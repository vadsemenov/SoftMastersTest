using Microsoft.AspNetCore.Mvc;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
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

            var cargo = await cargoRepository.GetByIdAsync(areaResponse.Cargo.Id);

            if (cargo == null)
            {
                NotFound();
            }

            cargo!.UnloadTime = areaResponse.DeleteTime;
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