namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";



        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            ImportPlayDto[] playDtos = Deserialize<ImportPlayDto[]>(xmlString, "Plays");
            StringBuilder sb = new StringBuilder();
            List<Play> plays = new List<Play>();

            foreach (var dto in playDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDurationValid = TimeSpan.TryParseExact(dto.Duration, "c", CultureInfo.InvariantCulture, out TimeSpan duration);
                bool isGenreValid = Enum.TryParse<Genre>(dto.Genre, out Genre genre);

                if (!isDurationValid
                    || !isGenreValid
                    || duration < TimeSpan.FromHours(1))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Play play = new Play()
                {
                    Description = dto.Description,
                    Duration = duration,
                    Rating = dto.Rating,
                    Screenwriter = dto.Screenwriter,
                    Genre = genre,
                    Title = dto.Title
                };
                plays.Add(play);
                sb.AppendLine(String.Format(SuccessfulImportPlay, play.Title, genre.ToString(), play.Rating));
            }
            context.Plays.AddRange(plays);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            ImportCastDto[] castDtos = Deserialize<ImportCastDto[]>(xmlString, "Casts");
            StringBuilder sb = new StringBuilder();
            List<Cast> casts = new List<Cast>();

            foreach (var dto in castDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Cast cast = new Cast()
                {
                    FullName = dto.FullName,
                    IsMainCharacter = dto.IsMainCharacter,
                    PhoneNumber = dto.PhoneNumber,
                    PlayId = dto.PlayId
                };

                casts.Add(cast);
                string characterString = cast.IsMainCharacter ? "main" : "lesser";
                sb.AppendLine(String.Format(SuccessfulImportActor, cast.FullName, characterString));
            }
            context.Casts.AddRange(casts);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            ImportTheaterDto[] theaterDtos = JsonConvert.DeserializeObject<ImportTheaterDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            List<Theatre> theatres = new List<Theatre>();

            foreach (var theatreDto in theaterDtos)
            {
                if (!IsValid(theatreDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                List<Ticket> tickets = new List<Ticket>();
                foreach (var ticketDto in theatreDto.Tickets)
                {
                    if (!IsValid(ticketDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Ticket ticket = new Ticket()
                    {
                        PlayId = ticketDto.PlayId,
                        Price = ticketDto.Price,
                        RowNumber = ticketDto.RowNumber
                    };
                    tickets.Add(ticket);
                }

                Theatre theatre = new Theatre()
                {
                    Director = theatreDto.Director,
                    Name = theatreDto.Name,
                    NumberOfHalls = theatreDto.NumberOfHalls,
                    Tickets = tickets
                };
                theatres.Add(theatre);
                sb.AppendLine(String.Format(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count));
            }
            context.Theatres.AddRange(theatres);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static T Deserialize<T>(string inputXml, string rootName)
        {
            XmlRootAttribute root = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T), root);

            using StringReader reader = new StringReader(inputXml);

            T dtos = (T)serializer.Deserialize(reader);
            return dtos;
        }

        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
