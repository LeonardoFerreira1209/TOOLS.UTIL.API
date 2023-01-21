using APPLICATION.DOMAIN.CONTRACTS.REPOSITORIES.TEMPLATES;
using APPLICATION.DOMAIN.CONTRACTS.SERVICES.TEMPLATE;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using APPLICATION.ENUMS;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;

namespace APPLICATION.APPLICATION.SERVICES.TEMPLATE;

/// <summary>
/// Serviço de templates.
/// </summary>
public class TemplateService : ITemplateService
{
    private readonly IOptions<AppSettings> _appsettings;

    private readonly ITemplateRepository _templateRepository;

    public TemplateService() { }

    public TemplateService(IOptions<AppSettings> appsettings, ITemplateRepository templateRepository)
    {
        _appsettings = appsettings;

        _templateRepository = templateRepository;
    }

    /// <summary>
    /// Método responsável por salvar um template de e-mail.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<object>> Save(string name, string description, string fileContent)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TemplateService)} - METHOD {nameof(Save)}\n");

        try
        {
            Log.Information($"[LOG INFORMATION] - Salvando o template {name} - {description} - Content {fileContent}\n");

            // Save the template in database.
            await _templateRepository.Save(name, description, fileContent);

            Log.Information($"[LOG INFORMATION] - Templeta salvo com sucesso!\n");

            // Response success.
            return new ApiResponse<object>(true, StatusCodes.SuccessOK, new List<DadosNotificacao> { new DadosNotificacao("Template cadastrado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]", exception, exception.Message);

            // Response error.
            return new ApiResponse<object>(false, StatusCodes.ErrorBadRequest, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }

    /// <summary>
    /// Método responsável por recuperar um template de e-mail através do nome.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<string>> GetContentTemplateWithName(string name)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TemplateService)} - METHOD {nameof(Save)}\n");

        try
        {
            Log.Information($"[LOG INFORMATION] - Recuperando o template {name}\n");

            // Get template.
            var content = await _templateRepository.GetContentTemplateWithName(name);

            // Is not null -> Response success.
            if (content is not null) return new ApiResponse<string>(true, StatusCodes.SuccessOK, content, new List<DadosNotificacao> { new DadosNotificacao("Template recuperado0 com sucesso.") });

            Log.Information($"[LOG INFORMATION] - Template Recuperado com sucesso!\n");

            // Response error.
            return new ApiResponse<string>(false, StatusCodes.ErrorBadRequest, new List<DadosNotificacao> { new DadosNotificacao("Nenhum template encontrado.") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]", exception, exception.Message);

            // Response error.
            return new ApiResponse<string>(false, StatusCodes.ServerErrorInternalServerError, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }
}
