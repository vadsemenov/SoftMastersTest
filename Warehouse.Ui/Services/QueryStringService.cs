using System.Collections.Specialized;
using System.Web;

namespace Warehouse.Ui.Services;

public static class QueryStringService
{
    // https://www.jammer.biz/using-httpclient-to-send-dates-in-url-using-attributerouting/
    // var queryStringParams = new NameValueCollection
    // {
    //     {"startDate", time.ToQueryFormat()},
    // };

    public static string ToQueryString(this NameValueCollection source, bool removeEmptyEntries)
    {
        return source != null ? "?" + String.Join("&", source.AllKeys
                .Where(key => !removeEmptyEntries || source.GetValues(key).Any(value => !String.IsNullOrEmpty(value)))
                .SelectMany(key => source.GetValues(key)
                    .Where(value => !removeEmptyEntries || !String.IsNullOrEmpty(value))
                    .Select(value => String.Format("{0}={1}", HttpUtility.UrlEncode(key), value != null ? HttpUtility.UrlEncode(value) : string.Empty)))
                .ToArray())
            : string.Empty;
    }

    public static string ToQueryFormat(this DateTime? time)
    {
        if (time == null)
        {
            return string.Empty;
        }

        return $"{time.Value.Year}-{time.Value.Month}-{time.Value.Day}T{time.Value.ToLongTimeString()}";
    }
}