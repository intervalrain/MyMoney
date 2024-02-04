using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
	public static class DbContextExtension
	{
		public static IEnumerable<T> ExecuteQuery<T>(this DbContext dbContext, string sql)
		{
			var db = dbContext.Database;
			using (var cmd = db.GetDbConnection().CreateCommand())
			{
				cmd.CommandText = sql;
				cmd.CommandType = CommandType.Text;
				db.OpenConnection();

				using (var reader = cmd.ExecuteReader())
				{
					T obj = default(T);
					while (reader.Read())
					{
						obj = Activator.CreateInstance<T>();
						foreach (PropertyInfo prop in obj.GetType().GetProperties())
						{
							if (!reader[prop.Name].Equals(DBNull.Value))
							{
								prop.SetValue(obj, reader[prop.Name], null);
							}
						}
						yield return obj;
					}
				}
				db.CloseConnection();
			}
		}
	}
}