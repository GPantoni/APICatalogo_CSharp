using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoxUnitTests.UnitTests;

public class PutProdutoUnitTests : IClassFixture<ProdutoUnitTestController>
{
    private readonly ProdutosController _controller;

    public PutProdutoUnitTests(ProdutoUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }
    
    [Fact]
    public async Task PutProduto_Return_OkResult()
    {
        //Arrange  
        var prodId = 11;

        var updatedProdutoDto = new ProdutoDTO
        {
            ProdutoId = prodId,
            Nome = "Produto Atualizado - Testes",
            Descricao = "Minha Descricao",
            ImagemUrl = "imagem1.jpg",
            CategoriaId = 2
        };

        // Act
        var result = await _controller.Put(prodId, updatedProdutoDto);

        // Assert  
        result.Should().NotBeNull(); // Verifica se o resultado não é nulo
        result.Result.Should().BeOfType<OkObjectResult>(); // Verifica se o resultado é OkObjectResult
    }
    
    [Fact]
    public async Task PutProduto_Return_BadRequestResult()
    {
        //Arrange
        var prodId = 1000;
        var meuProduto = new ProdutoDTO
        {
            ProdutoId = 14,
            Nome = "Produto Atualizado - Testes",
            Descricao = "Minha Descricao",
            ImagemUrl = "imagem1.jpg",
            CategoriaId = 2
        };

        //Act
        var data = await _controller.Put(prodId, meuProduto);

        //Assert
        data.Result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
    }
}