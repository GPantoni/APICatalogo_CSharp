using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _repository;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(IProdutoRepository repository, ILogger<ProdutosController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produtos = _repository.GetProdutosPorCategoria(id);
        
        if(produtos is null)
            return NotFound();

        return Ok(produtos);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetAll();
        
        if (produtos is null)
            return NotFound();
        
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.Get(p => p.ProdutoId == id);
        
        if (produto is null)
        {
            _logger.LogWarning($"Produto com id= {id} não encontrado...");
            return NotFound($"Produto com id= {id} não encontrado...");
        }
        
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos...");
        }

        var produtoCriado = _repository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoCriado.ProdutoId }, produtoCriado);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos...");
        }

        var produtoAtualizado = _repository.Update(produto);

        return Ok(produtoAtualizado);
    }

    [HttpDelete]
    public ActionResult Delete(int id)
    {
        var produto = _repository.Get(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound("Produto não encontrado...");

        var produtoDeletado = _repository.Delete(produto);
        return Ok(produtoDeletado);
    }
}