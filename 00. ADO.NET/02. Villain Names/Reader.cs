using System.IO;


namespace _02.VillainNames
{
    internal class Reader
    {
        private const string configFilePath = @"..\..\..\Config.txt";

        public string ReadConnectionString()
        {
            using StreamReader configReader = new StreamReader(configFilePath);
            return configReader.ReadLine();
        }
    }
}
