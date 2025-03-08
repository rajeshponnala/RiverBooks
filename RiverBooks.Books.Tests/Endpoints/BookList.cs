using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.BookEndPoints;
using Xunit.Abstractions;

namespace RiverBooks.Books.Tests.Endpoints;
public class BookList(Fixture fixture): TestBase<Fixture>() {

  [Fact]
  public async Task ReturnsBookAsync() {
     var testResult = await fixture.Client.GETAsync<List, ListBooksResponse>();
     testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Books.Count().Should().Be(3);
  }

  
}

