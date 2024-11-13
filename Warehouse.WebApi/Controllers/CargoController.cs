using Microsoft.AspNetCore.Mvc;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
using Warehouse.WebApi.Map;
using Warehouse.Model.Model;

namespace Warehouse.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CargoController : ControllerBase
{
    private readonly ILogger<WarehouseController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CargoController(
        ILogger<WarehouseController> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCargo(CargoResponse cargoResponse)
    {
        try
        {
            var areaRepository = _unitOfWork.GetRepository<IAreaRepository>();
            var cargoRepository = _unitOfWork.GetRepository<ICargoRepository>();

            var area = await areaRepository.GetByIdAsync(cargoResponse.AreaId);

            if (area == null)
            {
                return NotFound();
            }

            var cargoes = area.Cargoes.ToList();

            var existCargoes = cargoes
                .Where(c => c.UnloadTime != null &&
                            (c.LoadTime < cargoResponse.LoadTime && c.UnloadTime > cargoResponse.LoadTime))
                .ToList();

            if (existCargoes.Any())
            {
                return NotFound();
            }

            var oldCargoes = cargoes
                .Where(c => c.UnloadTime == null)
                .ToList();

            oldCargoes.ForEach(c =>
            {
                c.UnloadTime = cargoResponse.LoadTime;

                cargoRepository.Update(c);
            });

            var newCargo = cargoResponse.ToDto();

            cargoRepository.Create(newCargo);
            await cargoRepository.SaveAsync();

            area.Cargoes.Add(newCargo);
            areaRepository.Update(area);
            await areaRepository.SaveAsync();

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<ActionResult> DeleteCargo(CargoResponse cargoResponse)
    {
        try
        {
            var cargoRepository = _unitOfWork.GetRepository<ICargoRepository>();

            var cargo = await cargoRepository.GetByIdAsync(cargoResponse.Id);

            if (cargo == null)
            {
                NotFound();
            }

            cargo!.UnloadTime = cargoResponse.UnloadTime;
            cargoRepository.Update(cargo);
            await cargoRepository.SaveAsync();

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return BadRequest();
        }
    }
}