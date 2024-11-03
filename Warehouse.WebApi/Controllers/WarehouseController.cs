using Microsoft.AspNetCore.Mvc;
using Warehouse.DataAccess;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
using Warehouse.WebApi.Map;
using Warehouse.WebApi.Model;

namespace Warehouse.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public WarehouseController(
            ILogger<WarehouseController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<WarehouseResponse>>> GetAllWarehousesAsync()
        {
            ICollection<Core.DTO.Warehouse> warehouses = new List<Core.DTO.Warehouse>();
            ICollection<WarehouseResponse> warehousesResponse = new List<WarehouseResponse>();

            try
            {
                var repository = _unitOfWork.GetRepository<IWarehouseRepository>();

                warehouses = await repository.GetAllAsync();

                warehousesResponse = warehouses.Select(w => w.ToModel()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }

            return Ok(warehousesResponse);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<WarehouseResponse>> GetWarehouseByIdAsync(int id)
        { 
            WarehouseResponse? warehouse;
            try
            {
                warehouse = (await _unitOfWork.GetRepository<IWarehouseRepository>().GetByIdAsync(id))?.ToModel();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }

            if (warehouse == null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }
    }
}
