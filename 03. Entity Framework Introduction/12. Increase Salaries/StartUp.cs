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

        string result = IncreaseSalaries(dbContext);
        Console.WriteLine(result);
    }

    public static string IncreaseSalaries(SoftUniContext context)
    {

        StringBuilder sb = new StringBuilder();

        var employeesIncreseSalary = context.Employees
            .Where(e => e.Department.Name == "Engineering" ||
                        e.Department.Name == "Tool Design" ||
                        e.Department.Name == "Marketing" ||
                        e.Department.Name == "Information Services")
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToList();

        foreach (var e in employeesIncreseSalary)
        {
            e.Salary += e.Salary * 0.12m;
        }

        foreach (var employee in employeesIncreseSalary)
        {
            sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
        }

        return sb.ToString().TrimEnd();
    }
}