using Microsoft.AspNetCore.Mvc;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
using Warehouse.WebApi.Model;

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
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return BadRequest();
        }

        return Ok();
    }
}