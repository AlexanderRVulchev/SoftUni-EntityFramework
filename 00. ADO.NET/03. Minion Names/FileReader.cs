using System.IO;

namespace _03.MinionNames
{
    internal class FileReader
    {
        private const string CONFIG_FILE_PATH = @"..\..\..\Config.txt";

        public string GetConnectionString()
        {
            using StreamReader configReader = new StreamReader(CONFIG_FILE_PATH);
            return configReader.ReadLine();            
        }

    }
}