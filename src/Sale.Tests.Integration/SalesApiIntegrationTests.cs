using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using System.Text.Json;

namespace Sale.Tests.Integration
{
    public class SalesApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SalesApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // Cria um cliente HTTP para simular chamadas à API
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateSale_Should_Return_Created_Sale()
        {
            // Arrange
            var sale = new Sales.Domain.Entities.Sale
            {
                SaleNumber = "V123456",
                SaleDate = DateTime.Now,
                CustomerName = "João Silva",
                TotalAmount = 500.00m,
                Branch = "Filial Teste"
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(sale), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/sales", jsonContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            var returnedSale = JsonSerializer.Deserialize<Sales.Domain.Entities.Sale>(await response.Content.ReadAsStringAsync());
            returnedSale.Should().NotBeNull();
            returnedSale.SaleNumber.Should().Be("V123456");
        }
    }
}
