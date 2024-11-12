using System.Collections.Specialized;
using System.Net.Http.Json;
using Warehouse.Ui.Model;

namespace Warehouse.Ui.Services;

public class HttpService
{
    private readonly HttpClient _client;

    public HttpService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IList<WarehouseResponse>?> GetWarehouses()
    {
      return await _client.GetFromJsonAsync<List<WarehouseResponse>>("Warehouse");
    } 
    
    public async Task<IList<WarehouseResponse>?> GetWarehousesByTime(DateTime time)
    {
        // https://www.jammer.biz/using-httpclient-to-send-dates-in-url-using-attributerouting/


        // var queryStringParams = new NameValueCollection
        // {
        //     {"startDate", time.ToQueryFormat()},
        // };
        //
        // return await _client.GetFromJsonAsync<List<WarehouseResponse>>("Warehouse/ByTime" + queryStringParams.ToQueryString(true));


        var httpTime = time.ToQueryFormat();
        return await _client.GetFromJsonAsync<List<WarehouseResponse>>($"Warehouse/{httpTime}" );
    }
}