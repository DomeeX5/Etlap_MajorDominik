using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
    class EtlapService
    {
		MySqlConnection connection;

		public EtlapService()
		{
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Server = "localhost";
			builder.Port = 3306;
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "etlapdb";
			connection = new MySqlConnection(builder.ConnectionString);
		}

		public bool Add(Etlap etlap)
		{
			OpenConnection();
			string sql = "INSERT INTO etlap (nev, leiras, ar, kategoria) VALUES (@name, @desc, @price, @category)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("@name", etlap.Name);
			cmd.Parameters.AddWithValue("@desc", etlap.Description);
			cmd.Parameters.AddWithValue("@price", etlap.Price);
			cmd.Parameters.AddWithValue("@category", etlap.Category);

			int affectedRows = cmd.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}

		public bool Delete(int id)
		{
			OpenConnection();
			string sql = "DELETE FROM etlap WHERE id = @id";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("@id", id);
			int affectedRows = cmd.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}

		public List<Etlap> GetEtlap()
		{
			List<Etlap> etlapList = new List<Etlap>();
			OpenConnection();
			string sql = "SELECT * FROM etlap";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			using(MySqlDataReader reader = cmd.ExecuteReader())
			{
				while(reader.Read())
				{
					Etlap etlap = new Etlap();
					etlap.Id = reader.GetInt32("id");
					etlap.Name = reader.GetString("nev");
					etlap.Description = reader.GetString("leiras");
					etlap.Price = reader.GetInt32("ar");
					etlap.Category = reader.GetString("kategoria");
					etlapList.Add(etlap);
				}
			}
			return etlapList;
		}


		public void OpenConnection()
		{
			if (connection.State != System.Data.ConnectionState.Open)
			{
				connection.Open();
			}
		}
		public void CloseConnection()
		{
			if (connection.State == System.Data.ConnectionState.Open)
			{
				connection.Close();
			}
		}
	}
}
