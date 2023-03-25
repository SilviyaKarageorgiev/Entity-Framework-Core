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

        string result = GetAddressesByTown(dbContext);
        Console.WriteLine(result);
    }

    public static string GetAddressesByTown(SoftUniContext context)
    {

        StringBuilder sb = new StringBuilder();

        var addresses = context.Addresses
            .OrderByDescending(a => a.Employees.Count)
            .ThenBy(a => a.Town!.Name)
            .ThenBy(a => a.AddressText)
            .Select(a => new
            {
                a.AddressText,
                a.Town!.Name,
                EmployeesOnTheAddress = a.Employees.Count
            })
            .Take(10)
            .ToArray();

        foreach (var a in addresses)
        {
            sb.AppendLine($"{a.AddressText}, {a.Name} - {a.EmployeesOnTheAddress} employees");
        }

        return sb.ToString().TrimEnd();
    }
}