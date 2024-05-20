using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoxUnitTests.UnitTests;

public class GetProdutoUnitTests : IClassFixture<ProdutoUnitTestController>
{
    private readonly ProdutosController _controller;

    public GetProdutoUnitTests(ProdutoUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task GetProdutoById_Return_OkResult()
    {
        //Arrange
        var produtoId = 2;

        //Act
        var data = await _controller.Get(produtoId);

        //Assert (xUnit)
        //var okResult = Assert.IsType<OkObjectResult>(data.Result);
        //Assert.Equal(200, okResult.StatusCode);

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetProdutoById_Return_NotFoundResult()
    {
        //Arrange
        var produtoId = 9999;

        //Act
        var data = await _controller.Get(produtoId);

        //Assert (xUnit)
        //var notFoundResult = Assert.IsType<NotFoundResult>(data.Result);
        //Assert.Equal(404, notFoundResult.StatusCode);

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetProdutoById_Return_BadRequestResult()
    {
        //Arrange
        var produtoId = -1;

        //Act
        var data = await _controller.Get(produtoId);

        //Assert (xUnit)
        //var badRequestResult = Assert.IsType<BadRequestResult>(data.Result);
        //Assert.Equal(400, badRequestResult.StatusCode);

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetProdutos_Return_ListOfProdutoDTO()
    {
        //Act
        var data = await _controller.Get();

        //Assert (xUnit)
        //var okResult = Assert.IsType<OkObjectResult>(data.Result);
        //var returnValue = Assert.IsType<List<ProdutoDTO>>(okResult.Value);

        //Assert (FluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeAssignableTo<List<ProdutoDTO>>().And
            .NotBeNull();
    }
    
    // [Fact]
    // public async Task GetProdutos_Return_BadRequestResult()
    // {
    //     //Arrange
    //     _controller.ModelState.AddModelError("erro", "Houve um erro");
    //
    //     //Act
    //     var data = await _controller.Get();
    //
    //     //Assert (xUnit)
    //     //var badRequestResult = Assert.IsType<BadRequestObjectResult>(data.Result);
    //     //Assert.Equal(400, badRequestResult.StatusCode);
    //
    //     //Assert (FluentAssertions)
    //     data.Result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
    // }
}