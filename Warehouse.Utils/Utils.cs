using Warehouse.Core.DTO;

namespace Warehouse.Utils;

public static class Utils
{
    public static string GetAreaName(this List<Picket> pickets)
    {
        if (pickets.Count == 1)
        {
            return pickets[0].Name.ToString();
        }

        return pickets.First().Name + "-" + pickets.Last().Name;
    }

    private static readonly Random Random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}