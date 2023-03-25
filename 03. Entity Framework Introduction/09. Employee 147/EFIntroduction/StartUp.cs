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

        string result = GetEmployee147(dbContext);
        Console.WriteLine(result);
    }

    public static string GetEmployee147(SoftUniContext context)
    {

        StringBuilder sb = new StringBuilder();

        var employee = context.Employees
            .Where(e => e.EmployeeId == 147)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                Projects = e.EmployeesProjects
                    .OrderBy(ep => ep.Project.Name)
                    .Select(ep => new
                    {
                        ProjectName = ep.Project.Name
                    })
                    .ToArray()
            })
            .First();

        sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

        foreach (var p in employee.Projects)
        {
            sb.AppendLine($"{p.ProjectName}");
        }

        return sb.ToString().TrimEnd();
    }
}