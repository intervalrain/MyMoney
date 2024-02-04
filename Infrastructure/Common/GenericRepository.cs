using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
        private IDbConnection _conn = null;
        private string _tableName = null;
        private string _primaryKey = null;

		public GenericRepository(DbContext dbContext)
		{
            _conn = dbContext.Database.GetDbConnection();
            _tableName = GetTableName();
            _primaryKey = GetPrimaryKey();
		}

        public IEnumerable<T> GetAll()
        {
            var sql = $"select * from {_tableName}";
            return _conn.Query<T>(sql);
        }

        public T GetById(object id)
        {
            var sql = $"select * from {_tableName} where {_primaryKey} = @Id";
            var param = new { Id = id };
            return _conn.Query<T>(sql, param).FirstOrDefault();
        }

        public IEnumerable<T> FindAll(params object[] param)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var sql = new StringBuilder($"select * from {_tableName} where 1=1");
            var parameters = new DynamicParameters();
            for (int i = 0; i < param.Length; i++)
            {
                var propertyName = properties[i].Name;
                var propertyValue = param[i];
                sql.Append($" and {propertyName} = @{propertyName}");
                parameters.Add(propertyName, propertyValue);
            }
            return _conn.Query<T>(sql.ToString(), parameters);
        }

        public T Find(params object[] param)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var sql = new StringBuilder($"select * from {_tableName} where 1=1");
            var parameters = new DynamicParameters();
            for (int i = 0; i < param.Length; i++)
            {
                var propertyName = properties[i].Name;
                var propertyValue = param[i];
                sql.Append($" and {propertyName} = @{propertyValue}");
                parameters.Add(propertyName, propertyValue);
            }
            return _conn.QueryFirstOrDefault<T>(sql.ToString(), parameters);
        }

        public void Insert(T obj)
        {
            var columns = GetColumns();
            var sql = new StringBuilder($"insert into {_tableName} (");
            sql.Append(string.Join(", ", columns));
            sql.Append(") values (");
            sql.Append(string.Join(", ", columns.Select(c => "@" + c)));
            sql.Append(")");
            
            _conn.Execute(sql.ToString(), obj);
        }

        public void Update(T obj)
        {
            var columns = GetColumns();
            
            var sql = new StringBuilder($"update {_tableName} set ");
            sql.Append(string.Join(", ", columns.Where(c => c != _primaryKey).Select(c => $"{c} = @{c}")));
            sql.Append($" where {_primaryKey} = @{_primaryKey}");
            
            _conn.Execute(sql.ToString(), obj);
        }

        public void Delete(object id)
        {
            var sql = $"delete from {_tableName} where {_primaryKey} = @Id";
            var param = new { Id = id };
            _conn.Execute(sql, param);
        }

        public void Save()
        {
        }

        private string GetTableName()
        {
            var type = typeof(T);
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();
            return tableAttribute?.Name ?? type.Name;
        }

        private string GetPrimaryKey()
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var keyProperty = properties.FirstOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null);
            return keyProperty?.Name ?? "Id";
        }

        private IEnumerable<string> GetColumns()
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            return properties.Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).Select(p => p.Name);
        }
    }
}

