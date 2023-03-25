using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Globalization;
using System.Net;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();

        string result = GetLatestProjects(dbContext);
        Console.WriteLine(result);
    }

    public static string GetLatestProjects(SoftUniContext context)
    {

        StringBuilder sb = new StringBuilder();

        var lastTenProjects = context.Projects
            .Where(p => p.EndDate == null)
            .OrderByDescending(p => p.StartDate)
            .Select(p => new
            {
                p.Name,
                p.Description,
                StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
            })
            .Take(10)
            .ToArray();

        foreach (var p in lastTenProjects.OrderBy(x => x.Name))
        {
            sb.AppendLine(p.Name)
              .AppendLine(p.Description)
              .AppendLine(p.StartDate);
        }

        return sb.ToString().TrimEnd();
    }
}
