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

        string result = DeleteProjectById(dbContext);
        Console.WriteLine(result);
    }

    public static string DeleteProjectById(SoftUniContext context)
    {

        var epToDelete = context.EmployeesProjects
            .Where(ep => ep.ProjectId == 2);
        context.EmployeesProjects.RemoveRange(epToDelete);

        Project projectToDelete = context.Projects.Find(2)!;
        context.Projects.Remove(projectToDelete);
        context.SaveChanges();

        string[] projectNames = context.Projects
            .Take(10)
            .Select(p => p.Name)
            .ToArray();

        return String.Join(Environment.NewLine, projectNames);

    }
}