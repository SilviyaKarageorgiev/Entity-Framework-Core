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

        string result = RemoveTown(dbContext);
        Console.WriteLine(result);
    }

    public static string RemoveTown(SoftUniContext context)
    {
        var townToDelete = context
                .Towns
                .Where(t => t.Name == "Seattle")
                .FirstOrDefault();

        var refferedAddresses = context
            .Addresses
            .Where(a => a.TownId == townToDelete!.TownId)
            .ToList();

        foreach (var e in context.Employees)
        {
            if (refferedAddresses.Any(x => x.AddressId == e.AddressId))
            {
                e.AddressId = null;
            }
        }

        var numberOfAddressesDeleted = refferedAddresses.Count;


        context.Addresses.RemoveRange(refferedAddresses);
        context.Towns.Remove(townToDelete!);

        context.SaveChanges();

        return $"{numberOfAddressesDeleted} addresses in Seattle were deleted";

    }
}