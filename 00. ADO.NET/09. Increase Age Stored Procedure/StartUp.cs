using System;
using System.Data.SqlClient;

namespace PrintAllMinionNames
{
    internal class StartUp
    {
        private static SqlConnection dbCon;

        static void Main()
        {
            Reader reader = new Reader();
            string connectionString = reader.ReadConnectionString();
            int minionId = int.Parse(reader.ReadUserInput());

            using (dbCon = new SqlConnection(connectionString))
            {
                dbCon.Open();
                CreateProcedure(reader.ReadQueryForProcedureCreation());
                IncreaseAgeByOne(minionId);
                string result = GetResultString(minionId);
                dbCon.Close();
                Console.WriteLine(result);
            }
        }

        static void CreateProcedure(string query)
        {
            SqlCommand cmd = new SqlCommand(query, dbCon);
            cmd.ExecuteNonQuery();
        }

        static void IncreaseAgeByOne(int id)
        {
            SqlCommand cmd = new SqlCommand(@"EXEC usp_GetOlder @Id", dbCon);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        static string GetResultString(int minionId)
        {
            string queryText = @"SELECT Name, Age FROM Minions WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(queryText, dbCon);
            cmd.Parameters.AddWithValue("@Id", minionId);

            using SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string name = (string)reader["Name"];
            int age = (int)reader["Age"];
            reader.Close();

            return $"{name} - {age} years old";
        }
    }
}
