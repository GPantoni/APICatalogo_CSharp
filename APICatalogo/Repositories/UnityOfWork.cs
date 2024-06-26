using APICatalogo.Context;

namespace APICatalogo.Repositories;

public class UnityOfWork : IUnityOfWork
{
    private ICategoriaRepository? _categoriaRepo;

    public AppDbContext _context;
    private IProdutoRepository? _produtoRepo;

    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            // A linha acima, equivale à implementação abaixo:
            /*if (_produtoRepo == null)
            {
                _produtoRepo = new ProdutoRepository(_context);
            }

            return _produtoRepo;*/
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get { return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context); }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}