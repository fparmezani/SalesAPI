using Dapper;
using Sales.Data.Context;
using Sales.Data.Interfaces;
using Sales.Domain.Entities;

namespace Sales.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DbContext _dbContext;

        public SaleRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "SELECT * FROM Sales";
                return await connection.QueryAsync<Sale>(query);
            }
        }

        public async Task<Sale> GetByIdAsync(Guid id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "SELECT * FROM Sales WHERE Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<Sale>(query, new { Id = id });
            }
        }

        public async Task AddAsync(Sale sale)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "INSERT INTO Sales (Id, SaleNumber, SaleDate, CustomerName, TotalAmount, Branch, IsCancelled) " +
                            "VALUES (@Id, @SaleNumber, @SaleDate, @CustomerName, @TotalAmount, @Branch, @IsCancelled)";
                await connection.ExecuteAsync(query, sale);
            }
        }

        public async Task UpdateAsync(Sale sale)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "UPDATE Sales SET SaleNumber = @SaleNumber, SaleDate = @SaleDate, CustomerName = @CustomerName, " +
                            "TotalAmount = @TotalAmount, Branch = @Branch, IsCancelled = @IsCancelled WHERE Id = @Id";
                await connection.ExecuteAsync(query, sale);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = "DELETE FROM Sales WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        
    }
}
