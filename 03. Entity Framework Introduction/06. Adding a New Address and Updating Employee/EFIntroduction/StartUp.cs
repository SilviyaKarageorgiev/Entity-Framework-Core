using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();

        string result = AddNewAddressToEmployee(dbContext);
        Console.WriteLine(result);
    }

    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        Address newAddress = new Address()
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };

        Employee? employee = context.Employees
            .FirstOrDefault(e => e.LastName == "Nakov");

        employee!.Address = newAddress;

        context.SaveChanges();

        StringBuilder sb = new StringBuilder();

        var employeesAddresses = context.Employees
            .OrderByDescending(e => e.AddressId)
            .Take(10)
            .Select(e => e.Address!.AddressText)
            .ToArray();

        //foreach (var e in employeesAddresses)
        //{
        //    sb.AppendLine(e);
        //}

        //return sb.ToString().TrimEnd();

        return String.Join(Environment.NewLine, employeesAddresses);
    }
}