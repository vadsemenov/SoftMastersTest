namespace Warehouse.Utils;

public class StringComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        var xIsInt = int.TryParse(x, out var xValue);

        var yIsInt = int.TryParse(y, out var yValue);

        if (xIsInt && yIsInt)
        {
            if (xValue < yValue)
            {
                return -1;
            }

            if (xValue > yValue)
            {
                return 1;
            }

            return 0;
        }

        if (xIsInt && !yIsInt)
        {
            return -1;
        }

        if (!xIsInt && yIsInt)
        {
            return 1;
        }

        return 0;
    }
}