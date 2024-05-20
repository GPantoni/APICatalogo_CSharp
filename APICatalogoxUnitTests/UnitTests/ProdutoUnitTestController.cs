using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoxUnitTests.UnitTests;

public class ProdutoUnitTestController
{
    private static string connectionString =
        "Server=localhost;DataBase=CatalogoDB;Uid=root;Pwd=Mysql-100";

    public IMapper mapper;
    public IUnityOfWork repository;

    static ProdutoUnitTestController()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
    }

    public ProdutoUnitTestController()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new ProdutoDTOMappingProfile()); });

        mapper = config.CreateMapper();

        var context = new AppDbContext(dbContextOptions);
        repository = new UnityOfWork(context);
    }

    private static DbContextOptions<AppDbContext> dbContextOptions { get; }
}