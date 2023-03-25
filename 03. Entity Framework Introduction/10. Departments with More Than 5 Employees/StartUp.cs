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

        string result = GetDepartmentsWithMoreThan5Employees(dbContext);
        Console.WriteLine(result);
    }

    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {

        StringBuilder sb = new StringBuilder();

        var departmentsWithMoreThan5Employees = context.Departments
            .Where(d => d.Employees.Count > 5)
            .OrderBy(d => d.Employees.Count)
            .ThenBy(d => d.Name)
            .Select(d => new
            {
                d.Name,
                ManagerFirstName = d.Manager.FirstName,
                ManagerLastName = d.Manager.LastName,
                Employees = d.Employees
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                    .ToArray()              
            })
            .ToArray();

        foreach (var d in departmentsWithMoreThan5Employees)
        {
            sb.AppendLine($"{d.Name} - {d.ManagerFirstName} {d.ManagerLastName}");

            foreach (var e in d.Employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
            }
        }

        return sb.ToString().TrimEnd();
    }
}