using System;
using System.IO;
using System.Linq;

namespace AddMinion
{
    internal class Reader
    {
        private const string CONFIG_FILE_PATH = @"..\..\..\Config.txt";

        public string ReadConnectionString()
        {
            using StreamReader configReader = new StreamReader(CONFIG_FILE_PATH);
            return configReader.ReadLine();            
        }

        public string[] ReadUserInput()
        {
            string[] line1 = Console.ReadLine().Split();
            string[] line2 = Console.ReadLine().Split();
            string[] input = new string[] { line1[1], line1[2], line1[3], line2[1] };
            return input;
        }
    }
}