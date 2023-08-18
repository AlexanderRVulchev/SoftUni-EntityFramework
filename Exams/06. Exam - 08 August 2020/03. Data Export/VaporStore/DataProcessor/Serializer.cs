namespace VaporStore.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.ExportDto;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var exportGames = context
                .Genres
                .Where(g => genreNames.Contains(g.Name))
                .ToArray()
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games.Where(g => g.Purchases.Any()).Select(game => new
                    {
                        Id = game.Id,
                        Title = game.Name,
                        Developer = game.Developer.Name,
                        Tags = String.Join(", ", game.GameTags.Select(gt => gt.Tag.Name)),
                        Players = game.Purchases.Count
                    })
                        .OrderByDescending(g => g.Players)
                        .ThenBy(g => g.Id)
                        .ToArray(),
                    TotalPlayers = g.Games.Sum(g => g.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToArray();

            return JsonConvert.SerializeObject(exportGames, Formatting.Indented);
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string purchaseType)
        {
            ExportUserDto[] userDtos = context
                .Users
                .ToArray()
                .Where(u => u.Cards.Any(c => c.Purchases.Any(p => p.Type.ToString() == purchaseType)))                
                .Select(u => new ExportUserDto()
                {
                    Username = u.Username,
                    TotalSpent = u.Cards
                        .Sum(c => c.Purchases
                        .Where(p => p.Type.ToString() == purchaseType)
                        .Sum(p => p.Game.Price)),
                    Purchases = u.Cards
                        .SelectMany(c => c.Purchases
                        .Where(p => p.Type.ToString() == purchaseType)                        
                        .Select(p => new ExportPurchaseDto()
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new ExportGameDto()
                            {
                                Genre = p.Game.Genre.Name,
                                Title = p.Game.Name,
                                Price = p.Game.Price
                            }
                        }))
                        .OrderBy(p => p.Date)
                        .ToArray()                        
                })
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

            return Serialize<ExportUserDto[]>(userDtos, "Users");
        }

        private static string Serialize<T>(T dataTransferObjects, string xmlRootAttributeName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttributeName));

            StringBuilder sb = new StringBuilder();
            using var write = new StringWriter(sb);

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(write, dataTransferObjects, xmlNamespaces);

            return sb.ToString();
        }
    }
}