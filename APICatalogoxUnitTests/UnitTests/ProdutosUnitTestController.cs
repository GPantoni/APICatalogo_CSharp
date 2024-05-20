using Microsoft.Extensions.Configuration;

namespace ApiCatalogoxUnitTests.UnitTests;

public class ProdutosUnitTestController
{
    private IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<ProdutosUnitTestController>();

    private IConfigurationRoot configuration = builder.Build();

    string connectionString = configuration.GetConnectionString("DbConnectionString");
    
}