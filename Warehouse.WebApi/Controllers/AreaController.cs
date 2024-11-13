using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.DTO;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
using Warehouse.Utils;
using Warehouse.WebApi.Map;
using Warehouse.WebApi.Model;

namespace Warehouse.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AreaController : ControllerBase
{
    private readonly ILogger<WarehouseController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AreaController(
        ILogger<WarehouseController> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<ActionResult> CreateArea(AreaResponse areaResponse)
    {
        try
        {
            var areaRepository = _unitOfWork.GetRepository<IAreaRepository>();
            var warehouseRepository = _unitOfWork.GetRepository<IWarehouseRepository>();
            var picketRepository = _unitOfWork.GetRepository<IPicketRepository>();

            var warehouse = await warehouseRepository.GetByIdAsync(areaResponse.WarehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }
            var area = areaResponse.ToDto();
            area.Name = area.Pickets.GetAreaName();
            area.Warehouse = warehouse;

            var picketsIds = area.Pickets
                .Select(p => p.Id)
                .ToList();

            var pickets = new List<Picket>();

            await Parallel.ForEachAsync(picketsIds, async (i, token) =>
            {
                var picket = await picketRepository.GetByIdAsync(i);

                if (picket != null)
                {
                    pickets.Add(picket);
                }
            });

            area.Pickets = pickets;
            area.DeleteTime = null;

            areaRepository.Create(area);
            await areaRepository.SaveAsync();

            warehouse.Areas.Add(area);
            await warehouseRepository.SaveAsync();
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<ActionResult> DeleteAreaWithCargo(AreaResponse areaResponse)
    {
        try
        {
            var areaRepository = _unitOfWork.GetRepository<IAreaRepository>();
            var cargoRepository = _unitOfWork.GetRepository<ICargoRepository>();

            var area = await areaRepository.GetByIdAsync(areaResponse.Id);

            if (area == null)
            {
                return NotFound();
            }

            area.DeleteTime = areaResponse.DeleteTime;
            areaRepository.Update(area);
            await areaRepository.SaveAsync();

            var cargoId = areaResponse.Cargo.FirstOrDefault()?.Id;

            if (cargoId != null)
            {
                var cargo = await cargoRepository.GetByIdAsync(cargoId.Value);

                if (cargo == null)
                {
                    NotFound();
                }

                cargo!.UnloadTime = areaResponse.DeleteTime;
                cargoRepository.Update(cargo);
                await cargoRepository.SaveAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return BadRequest();
        }

        return Ok();
    }
}