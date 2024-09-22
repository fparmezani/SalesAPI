using Sales.Application.Interface;
using Sales.Data.Interfaces;
using Sales.Domain.Entities;
using Serilog;

namespace Sales.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger _logger;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
            _logger = Log.ForContext<SaleService>();
        }

        public async Task CreateSaleAsync(Sale sale)
        {
             _logger.Information("Criando uma nova venda {@Sale}", sale);
            await _saleRepository.AddAsync(sale);
            _logger.Information("Venda {SaleNumber} criada com sucesso", sale.SaleNumber);
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            _logger.Information("Atualizando a venda {SaleNumber}", sale.SaleNumber);
            await _saleRepository.UpdateAsync(sale);
            _logger.Information("Venda {SaleNumber} atualizada com sucesso", sale.SaleNumber);

        }

        public async Task<IEnumerable<Sale>> GetSalesAsync()
        {
             _logger.Information("Buscando todas as vendas");
            var sales = await _saleRepository.GetAllAsync();
            _logger.Debug("Vendas encontradas: {@Sales}", sales);
            return sales;
        }

        public async Task<Sale> GetSaleByIdAsync(Guid id)
        {
            _logger.Information("Buscando venda pelo ID {SaleId}", id);
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale != null)
            {
                _logger.Information("Venda {SaleNumber} encontrada", sale.SaleNumber);
            }
            else
            {
                _logger.Warning("Venda com ID {SaleId} não encontrada", id);
            }
            return sale;
        }

        public async Task DeleteSaleAsync(Guid id)
        {
            _logger.Information("Cancelando a venda com ID {SaleId}", id);
            await _saleRepository.DeleteAsync(id);
            _logger.Information("Venda com ID {SaleId} cancelada com sucesso", id);
        }
    }
}
