namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumsInfo = context
                .Producers
                .First(p => p.Id == producerId)
                .Albums
                .Select(a => new
                {
                    AlbumName = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy"),
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs.Select(s => new
                    {
                        SongName = s.Name,
                        SongPrice = s.Price,
                        SongWriterName = s.Writer.Name
                    })
                        .OrderByDescending(s => s.SongName)
                        .ThenBy(s => s.SongWriterName),
                    AlbumPrice = a.Price
                })
                .OrderByDescending(a => a.AlbumPrice)
                .ToArray();
                        
            StringBuilder sb = new StringBuilder();

            foreach (var a in albumsInfo)
            {
                sb
                    .AppendLine($"-AlbumName: {a.AlbumName}")
                    .AppendLine($"-ReleaseDate: {a.ReleaseDate}")
                    .AppendLine($"-ProducerName: {a.ProducerName}")
                    .AppendLine($"-Songs:");

                int counter = 1;
                if (a.Songs.Any())
                {
                    foreach (var s in a.Songs)
                    {
                        sb
                            .AppendLine($"---#{counter++}")
                            .AppendLine($"---SongName: {s.SongName}")
                            .AppendLine($"---Price: {s.SongPrice:f2}")
                            .AppendLine($"---Writer: {s.SongWriterName}");
                    }
                }

                sb.AppendLine($"-AlbumPrice: {a.AlbumPrice:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            throw new NotImplementedException();
        }
    }
}
