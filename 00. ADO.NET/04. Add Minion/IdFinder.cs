using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddMinion
{
    public class IdFinder
    {
        private readonly SqlConnection dbCon;

        public IdFinder(SqlConnection con)
        {
            this.dbCon = con;
        }

        public int GetTownId(string townName)
        {
            string townQuery = $"SELECT Id FROM Towns WHERE Name = '{townName}'";
            SqlCommand cmdFindId = new SqlCommand(townQuery, dbCon);

            object result = cmdFindId.ExecuteScalar();
            if (result != null)
            {
                return (int)result;
            }

            string insertTownQuery = $"INSERT INTO Towns VALUES ('{townName}', NULL)";
            SqlCommand cmdInsertTown = new SqlCommand(insertTownQuery, dbCon);
            cmdInsertTown.ExecuteNonQuery();
            Console.WriteLine($"Town {townName} was added to the database.");

            result = cmdFindId.ExecuteScalar();
            return (int)result;
        }

        public int GetVillainId(string villainName)
        {
            string villainQuery = $"SELECT Id FROM Villains WHERE Name = '{villainName}'";
            SqlCommand cmdFindId = new SqlCommand(villainQuery, dbCon);

            object result = cmdFindId.ExecuteScalar();
            if (result != null)
            {
                return (int)result;
            }

            string insertVillainQuery = $"INSERT INTO Villains VALUES ('{villainName}', 4)";
            SqlCommand cmdInserVillain = new SqlCommand(insertVillainQuery, dbCon);
            cmdInserVillain.ExecuteNonQuery();
            Console.WriteLine($"Villain {villainName} was added to the database.");

            result = cmdFindId.ExecuteScalar();
            return (int)result;
        }

        public int GetMinionId(string minionName)
        {
            string minionIdQuery = $"SELECT Id FROM Minions WHERE Name = '{minionName}'";
            SqlCommand cmdFindId = new SqlCommand(minionIdQuery, dbCon);
            return (int)cmdFindId.ExecuteScalar();
        }
    }
}