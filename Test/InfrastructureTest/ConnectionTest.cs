using Npgsql;

namespace Test.InfrastructureTest
{
	[TestClass]
	public class ConnectionTest
	{
		[TestMethod]
		public void Connection_Test()
		{
			var contacts = new List<string>();

			var connString = "Host=localhost;Port=5432;Username=postgres;Password=#rain1011;Database=mym";
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();
				using (var cmd = new NpgsqlCommand("Select * from contacts", conn))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							contacts.Add(Convert.ToString(reader[1])!);
						}
					}
				}
			}

			Assert.AreEqual(10, contacts.Count());
			Assert.AreEqual("rain", contacts[0]);
        }
	}
}

