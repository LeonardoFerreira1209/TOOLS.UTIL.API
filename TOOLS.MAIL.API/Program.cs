using APPLICATION.APPLICATION.CONFIGURATIONS;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using Microsoft.AspNetCore.Mvc;
using Serilog;

try
{
    // Preparando builder.
    var builder = WebApplication.CreateBuilder(args);

    // Pegando configura��es do appsettings.json.
    var configurations = builder.Configuration;

    // Pega o appsettings baseado no ambiente em execu��o.
    configurations
         .SetBasePath(builder.Environment.ContentRootPath).AddJsonFile("appsettings.json", true, true).AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true).AddEnvironmentVariables();

    /// <summary>
    /// Chamada das configura��es do projeto.
    /// </summary>
    builder.Services
        .AddHttpContextAccessor()
        .Configure<AppSettings>(configurations).AddSingleton<AppSettings>()
        .AddEndpointsApiExplorer()
        .AddOptions()
        .ConfigureLanguage()
        .ConfigureContexto(configurations)
        .ConfigureSwagger(configurations)
        .ConfigureDependencies(configurations, builder.Environment);

    // Se for em produ��o executa.
    if (builder.Environment.IsProduction())
    {
        builder.Services
             .ConfigureTelemetry(configurations)
             .ConfigureApplicationInsights(configurations);
    }

    // Continua��o do pipeline...
    builder.Services
        //.ConfigureSerilog()
        .ConfigureHealthChecks(configurations)
        .ConfigureCors()
        .AddControllers(options =>
        {
            options.EnableEndpointRouting = false;

            options.Filters.Add(new ProducesAttribute("application/json"));

        });

    // Preparando WebApplication Build.
    var applicationbuilder = builder.Build();

    // Chamada das connfigura��es do WebApplication Build.
    applicationbuilder
        .UseHttpsRedirection()
        .UseDefaultFiles()
        .UseStaticFiles()
        .UseRouting()
        .UseCors("CorsPolicy")
        .ConfigureHealthChecks()
        .UseSwaggerConfigurations(configurations);

    // Chamando as configura��es de Minimal APIS.
    applicationbuilder.UseMinimalAPI();

    Log.Information($"[LOG INFORMATION] - Inicializando aplica��o [TOOLS.MAIL.API]\n");

    // Iniciando a aplica��o com todas as configura��es j� carregadas.
    applicationbuilder.Run();
}
catch (Exception exception)
{
    Log.Error("[LOG ERROR] - Ocorreu um erro ao inicializar a aplicacao [TOOLS.MAIL.API]\n", exception.Message);
}