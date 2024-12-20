﻿using System.Net;
using System.Net.Http.Json;
using Warehouse.Model.Model;

namespace Warehouse.Ui.Services;

public class HttpService
{
    private readonly HttpClient _client;

    public HttpService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IList<WarehouseResponse>?> GetWarehousesAsync()
    {
        return await _client.GetFromJsonAsync<List<WarehouseResponse>>("Warehouse");
    }

    public async Task<WarehouseResponse?> CreateWarehouseAsync(WarehouseResponse warehouse)
    {
        using var response = await _client.PostAsJsonAsync("Warehouse", warehouse);

        var res = await response.Content.ReadFromJsonAsync<WarehouseResponse>();

        return res;
    }

    public async Task<PicketResponse?> CreatePicketAsync(PicketResponse picket)
    {
        using var response = await _client.PostAsJsonAsync("Picket", picket);

        var res = await response.Content.ReadFromJsonAsync<PicketResponse>();

        return res;
    }

    public async Task<IList<WarehouseResponse>?> GetWarehousesByTimeAsync(DateTime? time)
    {
        var httpTime = time.ToQueryFormat();

        return await _client.GetFromJsonAsync<List<WarehouseResponse>>($"Warehouse/{httpTime}");
    }

    public async Task DeleteAreaAsync(AreaResponse selectedArea)
    {
        await _client.PutAsJsonAsync("Area", selectedArea);
    }

    public async Task DeleteCargoAsync(CargoResponse selectedCargo)
    {
        await _client.PutAsJsonAsync("Cargo", selectedCargo);
    }

    public async Task<bool> CreateCargoAsync(CargoResponse cargoResponse)
    {
      using var response =  await _client.PostAsJsonAsync("Cargo", cargoResponse);

      return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> CreateAreaAsync(AreaResponse areaResponse)
    {
        using var response = await _client.PostAsJsonAsync("Area", areaResponse);

        return response.StatusCode == HttpStatusCode.OK;
    }
}