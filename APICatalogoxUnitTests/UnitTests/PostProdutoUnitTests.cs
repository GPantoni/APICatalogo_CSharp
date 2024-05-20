using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoxUnitTests.UnitTests;

public class PostProdutoUnitTests : IClassFixture<ProdutoUnitTestController>
{
    private readonly ProdutosController _controller;

    public PostProdutoUnitTests(ProdutoUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }
    
    [Fact]
    public async Task PostProduto_Return_CreatedResult()
    {
        //Arrange
        var novoProdutoDto = new ProdutoDTO()
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do produto teste",
            Preco = 9.99M,
            ImagemUrl = "imagem1.jpg",
            CategoriaId = 2
        };

        //Act
        var data = await _controller.Post(novoProdutoDto);

        //Assert (xUnit)
        //var createdResult = Assert.IsType<CreatedAtActionResult>(data.Result);
        //Assert.Equal(201, createdResult.StatusCode);

        //Assert (FluentAssertions)
        var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
        createdResult.Subject.StatusCode.Should().Be(201);
    }
    
    [Fact]
    public async Task PostProduto_Return_BadRequestResult()
    {
        //Arrange
        ProdutoDTO novoProdutoDto = null;

        //Act
        var data = await _controller.Post(novoProdutoDto);

        //Assert (xUnit)
        //var badRequestResult = Assert.IsType<BadRequestObjectResult>(data.Result);
        //Assert.Equal(400, badRequestResult.StatusCode);

        //Assert (FluentAssertions)
        var badRequestResult = data.Result.Should().BeOfType<BadRequestObjectResult>();
        badRequestResult.Subject.StatusCode.Should().Be(400);
    }
}