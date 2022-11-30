using APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using APPLICATION.DOMAIN.ENUM;
using APPLICATION.INFRAESTRUTURE.FACADES.EMAIL;
using Microsoft.Extensions.Options;
using Serilog;

namespace APPLICATION.APPLICATION.SERVICES.EMAIL;

/// <summary>
/// Serviço de email.
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
    /// Método responsável por fazer aenvio de email.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<object>> Invite(MailRequest request)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(EmailService)} - METHOD {nameof(Invite)}\n");

        try
        {
            await _emailFacade.Invite(request.Receivers, request.TemplateName, request.Subject, request.Content, request.Link, request.ButtonText);

            Log.Information($"[LOG INFORMATION] - E-mail enviado com sucesso.\n");

            return new ApiResponse<object>(true, StatusCodes.SuccessOK ,new List<DadosNotificacao> { new DadosNotificacao("Email enviado com sucesso.\n") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]\n", exception, exception.Message);

            return new ApiResponse<object>(false, StatusCodes.ServerErrorInternalServerError ,new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }
}
