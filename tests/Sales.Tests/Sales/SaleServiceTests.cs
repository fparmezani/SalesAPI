using Bogus;
using FluentAssertions;
using NSubstitute;
using Sales.Application.Services;
using Sales.Data.Interfaces;
using Sales.Domain.Entities;

namespace Sales.Tests.Sales
{
    public class SaleServiceTests
    {
        private readonly SaleService _saleService;
        private readonly ISaleRepository _saleRepository;

        public SaleServiceTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _saleService = new SaleService(_saleRepository);
        }

        [Fact]
        public async Task CreateSaleAsync_Should_Call_Repository_AddAsync()
        {
            var sale = GenerateFakeSale();
            await _saleService.CreateSaleAsync(sale);
            await _saleRepository.Received(1).AddAsync(sale);
        }

        [Fact]
        public async Task GetSalesAsync_Should_Return_ListOfSales()
        {
            var salesList = new List<Sale> { GenerateFakeSale(), GenerateFakeSale() };
            _saleRepository.GetAllAsync().Returns(salesList);
            var result = await _saleService.GetSalesAsync();
            result.Should().BeEquivalentTo(salesList);
        }

        [Fact]
        public async Task GetSaleByIdAsync_Should_Return_Sale_When_Found()
        {
            var sale = GenerateFakeSale();
            _saleRepository.GetByIdAsync(sale.Id).Returns(sale);
            var result = await _saleService.GetSaleByIdAsync(sale.Id);
            result.Should().BeEquivalentTo(sale);
        }

        [Fact]
        public async Task GetSaleByIdAsync_Should_Return_Null_When_NotFound()
        {
            var saleId = Guid.NewGuid();
            _saleRepository.GetByIdAsync(saleId).Returns((Sale)null);
            var result = await _saleService.GetSaleByIdAsync(saleId);
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateSaleAsync_Should_Call_Repository_UpdateAsync()
        {
            var sale = GenerateFakeSale();
            await _saleService.UpdateSaleAsync(sale);
            await _saleRepository.Received(1).UpdateAsync(sale);
        }

        [Fact]
        public async Task DeleteSaleAsync_Should_Call_Repository_DeleteAsync()
        {
            var saleId = Guid.NewGuid();
            await _saleService.DeleteSaleAsync(saleId);
            await _saleRepository.Received(1).DeleteAsync(saleId);
        }

        private Sale GenerateFakeSale()
        {
            var saleFaker = new Faker<Sale>()
                .RuleFor(s => s.Id, f => Guid.NewGuid())
                .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(s => s.SaleDate, f => f.Date.Recent())
                .RuleFor(s => s.CustomerName, f => f.Person.FullName)
                .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(100, 1000))
                .RuleFor(s => s.Branch, f => f.Company.CompanyName())
                .RuleFor(s => s.IsCancelled, f => f.Random.Bool());

            var sale = saleFaker.Generate();
            sale.Items = GenerateFakeSaleItems();
            return sale;
        }

        private List<SaleItem> GenerateFakeSaleItems()
        {
            var itemFaker = new Faker<SaleItem>()
                .RuleFor(i => i.Id, f => Guid.NewGuid())
                .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(10, 100))
                .RuleFor(i => i.Discount, f => f.Finance.Amount(0, 20));

            return itemFaker.Generate(3);
        }
    }
}
