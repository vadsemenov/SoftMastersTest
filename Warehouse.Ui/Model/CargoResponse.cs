﻿namespace Warehouse.Ui.Model;

public class CargoResponse
{
    public int Id { get; set; }

    public string Weight { get; set; } = null!;

    public DateTime LoadTime { get; set; }

    public DateTime UnloadTime { get; set; }
}