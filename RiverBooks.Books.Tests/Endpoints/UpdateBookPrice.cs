using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.BookEndPoints;

namespace RiverBooks.Books.Tests.Endpoints;

public class UpdateBookPrice(Fixture fixture) : TestBase<Fixture>()
{
  [Fact]
  public async Task ReturnsInvalidCodeAndMessageForNegativePriceAsync()
  {
    var testResult = await fixture.Client.POSTAsync<UpdatePrice, UpdateBookPriceRequest>(new UpdateBookPriceRequest(Guid.Parse("c3d6babd-c2c8-4fe8-a979-03e5f74f8a06"), -10.00m));
    testResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    var errorResponse = JsonSerializer.Deserialize<BadValidationRequestResponse>(await testResult.Content.ReadAsStringAsync());
    errorResponse?.errors["newPrice"].Should().Contain("Book prices may not be negative.");
  }

  [Fact]
  public async Task ReturnsInvalidCodeAndMessageForEmptyIdAsync()
  {
    var testResult = await fixture.Client.POSTAsync<UpdatePrice, UpdateBookPriceRequest>(new UpdateBookPriceRequest(Guid.Empty, -10.00m));
    testResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    var errorResponse = JsonSerializer.Deserialize<BadValidationRequestResponse>(await testResult.Content.ReadAsStringAsync());
    errorResponse?.errors["id"].Should().Contain("A book id is required");
  }
}
