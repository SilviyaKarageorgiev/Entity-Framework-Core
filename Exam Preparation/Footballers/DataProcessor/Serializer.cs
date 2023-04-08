namespace Footballers.DataProcessor
{
    using Data;
    using Footballers.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Globalization;
    using Trucks.Utilities;

    public class Serializer
    {
        private static XmlHelper xmlHelper;

        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            xmlHelper = new XmlHelper();

            ExportCoachDto[] coaches = context
                .Coaches
                .Where(c => c.Footballers.Any())
                .Select(c => new ExportCoachDto()
                {
                    CoachName = c.Name,
                    FootballersCount = c.Footballers.Count(),
                    Footballers = c.Footballers
                        .Select(f => new ExportFootballerDto()
                        {
                            Name = f.Name,
                            Position = f.PositionType.ToString()
                        })
                        .OrderBy(f => f.Name)
                        .ToArray()
                })
                .OrderByDescending(c => c.FootballersCount)
                .ThenBy(c => c.CoachName)
            .ToArray();

            return xmlHelper.Serialize(coaches, "Coaches");

        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                .Include(tf => tf.TeamsFootballers)
                .Where(t => t.TeamsFootballers.Any(f => f.Footballer.ContractStartDate >= date))
                .ToArray()
                .Select(t => new
                {
                    Name = t.Name,
                    Footballers = t.TeamsFootballers
                                  .Where(tf => tf.Footballer.ContractStartDate >= date)
                                  .OrderByDescending(tf => tf.Footballer.ContractEndDate)
                                  .ThenBy(tf => tf.Footballer.Name)
                                  .ToArray()
                                  .Select(tfb => new
                                  {
                                      FootballerName = tfb.Footballer.Name,
                                      ContractStartDate = tfb.Footballer.ContractStartDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                                      ContractEndDate = tfb.Footballer.ContractEndDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                                      BestSkillType = tfb.Footballer.BestSkillType.ToString(),
                                      PositionType = tfb.Footballer.PositionType.ToString()
                                  }).ToArray()
                })
                .OrderByDescending(m => m.Footballers.Count())
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }

    }
}

