﻿using System.Collections.Specialized;
using System.Web;

namespace Warehouse.Ui.Services;

public static class QueryStringService
{
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

    public static string ToQueryFormat(this DateTime time)
    {
        return $"{time.Year}-{time.Month}-{time.Day}T{time.ToLongTimeString()}";
    }
}