using Microsoft.AspNetCore.Mvc;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
using Warehouse.WebApi.Map;
using Warehouse.Model.Model;

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
            ICollection<WarehouseResponse> warehousesResponse;

            try
            {
                var repository = _unitOfWork.GetRepository<IWarehouseRepository>();

                ICollection<Core.DTO.Warehouse> warehouses = await repository.GetAllAsync();

                warehouses = warehouses.Select(w =>
                {
                    w.Areas = w.Areas
                        .Where(a => a.DeleteTime == null || a.DeleteTime == DateTime.MinValue)
                        .Select(a =>
                        {
                            a.Cargoes = a.Cargoes.Where(c => c.UnloadTime == null || c.UnloadTime == DateTime.MinValue)
                                .ToList();

                            return a;
                        })
                        .ToList();

                    return w;
                }).ToList();

                warehousesResponse = warehouses.Select(w => w.ToModel()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }

            return Ok(warehousesResponse);
        }

        [HttpGet("{dateTime:DateTime}")]
        public async Task<ActionResult<ICollection<WarehouseResponse>>> GetAllWarehousesAsync(DateTime dateTime)
        {
            ICollection<WarehouseResponse> warehousesResponse;

            try
            {
                var repository = _unitOfWork.GetRepository<IWarehouseRepository>();

                ICollection<Core.DTO.Warehouse> warehouses = await repository.GetAllAsync();

                warehouses = warehouses
                    .Select(w =>
                    {
                        w.Areas = w.Areas
                            .Where(a => a.CreateTime <= dateTime && (a.DeleteTime >= dateTime || a.DeleteTime == null))
                            .Select(a =>
                            {
                                a.Cargoes = a.Cargoes
                                    .Where(c => c.LoadTime <= dateTime &&
                                                (c.UnloadTime >= dateTime || c.UnloadTime == null))
                                    .ToList();

                                return a;
                            })
                            .ToList();

                        return w;
                    })
                .ToList();

                warehousesResponse = warehouses.Select(w => w.ToModel()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }

            return Ok(warehousesResponse);
        }

        [HttpGet("{id:int}")]
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

        [HttpPost]
        public async Task<ActionResult<WarehouseResponse>> CreateWarehouseAsync(WarehouseResponse warehouseResponse)
        {
            try
            {
                var warehouse = warehouseResponse.ToDto();

                var repository = _unitOfWork.GetRepository<IWarehouseRepository>();
                repository.Create(warehouse);
                await repository.SaveAsync();

                var response = warehouse.ToModel();

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }
    }
}
