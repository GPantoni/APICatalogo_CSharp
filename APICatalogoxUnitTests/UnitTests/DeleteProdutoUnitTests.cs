using APICatalogo.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoxUnitTests.UnitTests;

public class DeleteProdutoUnitTests : IClassFixture<ProdutoUnitTestController>
{
    private readonly ProdutosController _controller;

    public DeleteProdutoUnitTests(ProdutoUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }
    
    [Fact]
    public async Task DeleteProdutoById_Return_OkResult()
    {
        //Arrange
        var prodId = 11;

        //Act
        var data = await _controller.Delete(prodId);

        //Assert
        data.Should().NotBeNull();
        data.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task DeleteProdutoById_Return_NotFoundResult()
    {
        //Arrange
        var prodId = 1000;

        //Act
        var data = await _controller.Delete(prodId);

        //Assert
        data.Result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);
    }
}