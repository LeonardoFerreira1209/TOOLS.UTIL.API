using APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE;
using APPLICATION.INFRAESTRUTURE.FACADES.EMAIL;
using Microsoft.Extensions.Options;
using Serilog;

namespace APPLICATION.APPLICATION.SERVICES.EMAIL;

/// <summary>
/// Serviço de usuários.
/// </summary>
public class EmailService : IEmailService
{
    private readonly IOptions<AppSettings> _appsettings;

    private readonly EmailFacade _emailFacade;
    public EmailService(IOptions<AppSettings> appsettings, EmailFacade emailFacade)
    {
        _appsettings = appsettings;

        _emailFacade = emailFacade;
    }

    /// <summary>
    /// Método responsável por fazer a authorização do usuário.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<object>> Invite(MailRequest request)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(EmailService)} - METHOD {nameof(Invite)}\n");

        try
        {
            await _emailFacade.Invite(request.Receivers, request.Subject, request.Content);

            return new ApiResponse<object>(true, new List<DadosNotificacao> { new DadosNotificacao(DOMAIN.ENUM.StatusCodes.SuccessOK, "Email enviado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]", exception, exception.Message);

            return new ApiResponse<object>(false, new List<DadosNotificacao> { new DadosNotificacao(DOMAIN.ENUM.StatusCodes.ErrorBadRequest, exception.Message) });
        }
    }
}
