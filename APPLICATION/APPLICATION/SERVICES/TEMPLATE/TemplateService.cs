using APPLICATION.DOMAIN.CONTRACTS.REPOSITORIES.TEMPLATES;
using APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.RESPONSE;
using Microsoft.Extensions.Options;
using Serilog;

namespace APPLICATION.APPLICATION.SERVICES.TEMPLATE;

/// <summary>
/// Serviço de usuários.
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
            await _templateRepository.Save(name, description, fileContent);

            return new ApiResponse<object>(true, new List<DadosNotificacao> { new DadosNotificacao(DOMAIN.ENUM.StatusCodes.SuccessOK, "Template cadastrado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]", exception, exception.Message);

            return new ApiResponse<object>(false, new List<DadosNotificacao> { new DadosNotificacao(DOMAIN.ENUM.StatusCodes.ErrorBadRequest, exception.Message) });
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
            var contet = await _templateRepository.GetContentTemplateWithName(name);

            if (contet is not null) return new ApiResponse<string>(true, contet, new List<DadosNotificacao> { new DadosNotificacao(DOMAIN.ENUM.StatusCodes.SuccessOK, "Template cadastrado com sucesso.") });

            return new ApiResponse<string>(false, new List<DadosNotificacao> { new DadosNotificacao(DOMAIN.ENUM.StatusCodes.ErrorBadRequest, "Nenhum template encontrado.") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]", exception, exception.Message);

            return new ApiResponse<string>(false, new List<DadosNotificacao> { new DadosNotificacao(DOMAIN.ENUM.StatusCodes.ErrorBadRequest, exception.Message) });
        }
    }
}
