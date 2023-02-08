using System;
using System.IO;
using System.Linq;

namespace PrintAllMinionNames
{
    internal class Reader
    {
        private const string CONFIG_FILE_PATH = @"..\..\..\Config.txt";
        private const string PROC_QUERY_FILE_PATH = @"..\..\..\TSQL_usp_GetOlder.sql";

        public string ReadConnectionString()
        {
            using StreamReader configReader = new StreamReader(CONFIG_FILE_PATH);
            return configReader.ReadLine();
        }

        public string ReadUserInput()
        => Console.ReadLine();

        public string ReadQueryForProcedureCreation()
        {
            using StreamReader procQueryReader = new StreamReader(PROC_QUERY_FILE_PATH);
            return procQueryReader.ReadToEnd();
        }
    }
}