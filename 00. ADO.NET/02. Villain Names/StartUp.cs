using Microsoft.Data.SqlClient;
using System;
using System.Text;

namespace _02.VillainNames
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();
            string connectionString = reader.ReadConnectionString();
            SqlConnection dbCon = new SqlConnection(connectionString);
            dbCon.Open();
            using (dbCon)
            {
                string queryString = @"
                                SELECT v.Name,
	                                   COUNT(*) AS NumberOfMinions
                                  FROM Villains AS v
                                  JOIN MinionsVillains AS mv ON v.Id = mv.VillainId
                                  JOIN Minions AS m ON mv.MinionId = m.Id
                              GROUP BY v.Name
                                HAVING COUNT(*) > 3
                              ORDER BY NumberOfMinions DESC";
                string queryResult = GetQueryResult(queryString, dbCon);
                Console.WriteLine(queryResult);
            }
            dbCon.Close();

        }

        static string GetQueryResult(string queryString, SqlConnection dbCon)
        {
            SqlCommand cmd = new SqlCommand(queryString, dbCon);
            using SqlDataReader sqlReader = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            while (sqlReader.Read())
            {
                sb.AppendLine(sqlReader["Name"] + " - " + sqlReader["NumberOfMinions"]);
            }

            return sb.ToString().TrimEnd();
        }
    }
}