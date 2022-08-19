using APPLICATION.APPLICATION.CONFIGURATIONS;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using Microsoft.AspNetCore.Mvc;
using Serilog;

try
{
    // Preparando builder.
    var builder = WebApplication.CreateBuilder(args);

    // Pegando configurações do appsettings.json.
    var configurations = builder.Configuration;

    /// <summary>
    /// Chamada das configurações do projeto.
    /// </summary>
    builder.Services
        .AddHttpContextAccessor()
        .Configure<AppSettings>(configurations).AddSingleton<AppSettings>()
        .AddEndpointsApiExplorer()
        .AddOptions()
        .ConfigureLanguage()
        .ConfigureContexto(configurations)
        .ConfigureSwagger(configurations)
        .ConfigureDependencies(configurations)
        .ConfigureTelemetry(configurations)
        .ConfigureApplicationInsights(configurations)
        .ConfigureSerilog()
        .ConfigureHealthChecks(configurations)
        .ConfigureCors()
        .AddControllers(options =>
        {
            options.EnableEndpointRouting = false;

            options.Filters.Add(new ProducesAttribute("application/json"));

        });

    // Preparando WebApplication Build.
    var applicationbuilder = builder.Build();

    // Chamada das connfigurações do WebApplication Build.
    applicationbuilder
        .UseHttpsRedirection()
        .UseDefaultFiles()
        .UseStaticFiles()
        .UseRouting()
        .UseCors("CorsPolicu")
        .ConfigureHealthChecks()
        .UseSwaggerConfigurations(configurations);

    // Chamando as configurações de Minimal APIS.
    applicationbuilder.UseMinimalAPI();

    Log.Information($"[LOG INFORMATION] - Inicializando aplicação [TOOLS.MAIL.API]\n");

    // Iniciando a aplicação com todas as configurações já carregadas.
    applicationbuilder.Run();
}
catch (Exception exception)
{
    Log.Error("[LOG ERROR] - Ocorreu um erro ao inicializar a aplicacao [TOOLS.MAIL.API]\n", exception.Message);
}