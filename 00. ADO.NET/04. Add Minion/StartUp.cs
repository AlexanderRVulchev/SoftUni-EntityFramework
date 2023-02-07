using System;
using System.Data.SqlClient;

namespace AddMinion
{
    internal class StartUp
    {
        private static SqlConnection dbCon;

        static void Main()
        {
            Reader reader = new Reader();
            string connectionString = reader.ReadConnectionString();
            string[] inputTokens = reader.ReadUserInput();

            using (dbCon = new SqlConnection(connectionString))
            {
                dbCon.Open();
                AddMinionToVillain(inputTokens);
                dbCon.Close();
            }
        }

        static void AddMinionToVillain(string[] inputTokens)
        {
            string minionName = inputTokens[0];
            int minionAge = int.Parse(inputTokens[1]);
            string minionTown = inputTokens[2];
            string villainName = inputTokens[3];

            IdFinder sqlIdFinder = new IdFinder(dbCon);

            int townId = sqlIdFinder.GetTownId(minionTown);
            int villainId = sqlIdFinder.GetVillainId(villainName);

            string addMinionQuery = $"INSERT INTO Minions VALUES ('{minionName}', {minionAge}, {townId})";
            SqlCommand cmdAddMinion = new SqlCommand(addMinionQuery, dbCon);
            cmdAddMinion.ExecuteNonQuery();

            int minionId = sqlIdFinder.GetMinionId(minionName);

            string mapMinionToVillain = $"INSERT INTO MinionsVillains VALUES ({minionId}, {villainId})";
            SqlCommand cmdMapMinionToVillain = new SqlCommand(mapMinionToVillain, dbCon);
            cmdMapMinionToVillain.ExecuteNonQuery();
            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
        }
    }
}
