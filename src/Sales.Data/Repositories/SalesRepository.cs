using Sales.Data.Interfaces;
using Sales.Domain.Entities;

namespace Sales.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = new List<Sale>();

        public Task<Sale> GetByIdAsync(Guid id)
        {
            var sale = _sales.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(sale);
        }

        public Task<List<Sale>> GetAllAsync()
        {
            return Task.FromResult(_sales);
        }

        public Task AddAsync(Sale sale)
        {
            _sales.Add(sale);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Sale sale)
        {
            var existingSale = _sales.FirstOrDefault(s => s.Id == sale.Id);
            if (existingSale != null)
            {
                _sales.Remove(existingSale);
                _sales.Add(sale);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var sale = _sales.FirstOrDefault(s => s.Id == id);
            if (sale != null)
            {
                _sales.Remove(sale);
            }
            return Task.CompletedTask;
        }
    }
}
