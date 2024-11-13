using Microsoft.AspNetCore.Mvc;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
using Warehouse.WebApi.Map;
using Warehouse.Model.Model;

namespace Warehouse.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PicketController : ControllerBase
{
    private readonly ILogger<PicketController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PicketController(
        ILogger<PicketController> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<ActionResult<PicketResponse>> CreatePicketAsync(PicketResponse picketResponse)
    {
        try
        {
            var picket = picketResponse.ToDto();

            var picketRepository = _unitOfWork.GetRepository<IPicketRepository>();
            var warehouseRepository = _unitOfWork.GetRepository<IWarehouseRepository>();

            var warehouse = await warehouseRepository.GetByIdAsync(picketResponse.WarehouseId);
            picket.Warehouse = warehouse!;

            picketRepository.Create(picket);
            await picketRepository.SaveAsync();

            await warehouseRepository.SaveAsync();

            var response = picket.ToModel();

            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return BadRequest();
        }
    }
}