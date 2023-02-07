using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace _03.MinionNames
{
    internal class StartUp
    {
        private static SqlConnection dbCon;

        static void Main()
        {
            FileReader fileReader = new FileReader();
            string connectionString = fileReader.GetConnectionString();

            Console.Write("VillainId = ");
            int villainId = int.Parse(Console.ReadLine());

            using (dbCon = new SqlConnection(connectionString))
            {
                dbCon.Open();
                string resultString = GetMinionNamesByVillainId(villainId);
                Console.WriteLine(resultString);
                dbCon.Close();
            }
        }

        static string GetMinionNamesByVillainId(int villainId)
        {
            string villainName = GetVillainName(villainId);
            if (string.IsNullOrEmpty(villainName))
            {
                return $"No villain with ID <{villainId}> exists in the database.";
            }
            villainName = "Villain: " + villainName;

            string minionsInfo = GetMinionsNamesAndAges(villainId);
            if (string.IsNullOrEmpty(minionsInfo))
            {
                minionsInfo = "(no minions)";
            }
            return villainName + Environment.NewLine + minionsInfo;
        }

        static string GetVillainName(int villainId)
        {
            string villainQuery = "SELECT Name FROM Villains WHERE Id = @VillainId";
            SqlCommand cmd = new SqlCommand(villainQuery, dbCon);
            cmd.Parameters.AddWithValue("@VillainId", villainId);
            return (string)cmd.ExecuteScalar();
        }

        static string GetMinionsNamesAndAges(int villainId)
        {
            string minionsQuery = @"
                                SELECT m.Name,
	                                   m.Age
                                  FROM Villains AS v
                                  JOIN MinionsVillains AS mv ON v.Id = mv.VillainId
                                  JOIN Minions AS [m] ON mv.MinionId = m.Id
                                 WHERE v.Id = @VillainId
                              ORDER BY m.Name ASC";

            SqlCommand cmd = new SqlCommand(minionsQuery, dbCon);
            cmd.Parameters.AddWithValue("@VillainId", villainId);
            SqlDataReader reader = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            int minionCounter = 0;
            while (reader.Read())
            {
                sb.AppendLine($"{++minionCounter}. {reader["Name"]} {reader["Age"]}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
