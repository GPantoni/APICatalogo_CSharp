namespace APICatalogo.Logging;

public class CustomerLogger : ILogger
{
    private readonly CustomLoggerProviderConfiguration loggerConfig;
    private readonly string loggerName;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public IDisposable? BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        var caminhoArquivoLog = @"C:\Users\glauco.pantoni\source\repos\Logs\LogAPICatalogo.txt";
        using (var streamWriter = new StreamWriter(caminhoArquivoLog, true))
        {
            streamWriter.WriteLine(mensagem);
            streamWriter.Close();
        }
    }
}