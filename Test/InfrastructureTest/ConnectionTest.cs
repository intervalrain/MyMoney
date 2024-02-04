using Npgsql;

namespace Test.InfrastructureTest
{
	[TestClass]
	public class ConnectionTest
	{
		[TestMethod]
		public void Connection_Test()
		{
			var users = new List<string>();

			var connString = "Host=localhost;Port=5432;Username=postgres;Password=#rain1011;Database=mym";
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();
				using (var cmd = new NpgsqlCommand("Select * from users", conn))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							users.Add(Convert.ToString(reader[1])!);
						}
					}
				}
			}

			Assert.AreEqual(2, users.Count());
			Assert.AreEqual("Rain Hu", users[0]);
            Assert.AreEqual("Eva Hsu", users[1]);
        }
	}
}

