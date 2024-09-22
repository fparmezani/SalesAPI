using Microsoft.AspNetCore.Mvc;
using Sales.Application.Interface;
using Sales.Application.Services;
using Sales.Domain.Entities;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ILogger _logger;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
            _logger = Log.ForContext<SalesController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            _logger.Information("Recebida requisição para listar todas as vendas");
            var sales = await _saleService.GetSalesAsync();
            _logger.Information("Retornando {Count} vendas", sales.ToList().Count);
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            _logger.Information("Recebida requisição para obter a venda com ID {SaleId}", id);
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                _logger.Warning("Venda com ID {SaleId} não encontrada", id);
                return NotFound();
            }
            _logger.Information("Venda com ID {SaleId} retornada com sucesso", id);
            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
             _logger.Information("Recebida requisição para criar uma nova venda");
            await _saleService.CreateSaleAsync(sale);
            _logger.Information("Venda criada com ID {SaleId}", sale.Id);
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] Sale sale)
        {
            _logger.Information("Recebida requisição para atualizar a venda com ID {SaleId}", id);
            var existingSale = await _saleService.GetSaleByIdAsync(id);
            if (existingSale == null)
            {
                _logger.Warning("Venda com ID {SaleId} não encontrada para atualização", id);
                return NotFound();
            }

            sale.Id = id; // Assegura que o ID está correto
            await _saleService.UpdateSaleAsync(sale);
            _logger.Information("Venda com ID {SaleId} atualizada com sucesso", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            _logger.Information("Recebida requisição para cancelar a venda com ID {SaleId}", id);
            await _saleService.DeleteSaleAsync(id);
            _logger.Information("Venda com ID {SaleId} cancelada com sucesso", id);
            return NoContent();
        }
    }
}
