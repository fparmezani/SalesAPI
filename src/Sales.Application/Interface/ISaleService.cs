using Sales.Domain.Entities;

namespace Sales.Application.Interface
{
    public interface ISaleService
    {
        Task CreateSaleAsync(Sale sale);
        Task UpdateSaleAsync(Sale sale);
        Task<IEnumerable<Sale>> GetSalesAsync();
        Task<Sale> GetSaleByIdAsync(Guid id);
        Task DeleteSaleAsync(Guid id);
    }
}
