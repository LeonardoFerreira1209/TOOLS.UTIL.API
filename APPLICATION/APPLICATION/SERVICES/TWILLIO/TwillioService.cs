using APPLICATION.DOMAIN.CONTRACTS.SERVICES.TWILLIO;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using APPLICATION.DOMAIN.ENUM;
using Microsoft.Extensions.Options;
using Serilog;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace APPLICATION.APPLICATION.SERVICES.TWILLIO;

/// <summary>
/// Serviço de sms.
/// </summary>
public class TwillioService : ITwillioService
{
    private readonly IOptions<AppSettings> _appsettings;

    public TwillioService(IOptions<AppSettings> appsettings)
    {
        _appsettings = appsettings;
    }

    /// <summary>
    /// Método responsável por fazer o envio de sms.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<object>> Sms(MessageRequest request)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TwillioService)} - METHOD {nameof(Sms)}\n");

        try
        {
            TwilioClient.Init(_appsettings.Value.Twillio.TwillioAccountSID, _appsettings.Value.Twillio.TwillioAuthToken);

            var message = MessageResource.Create(body: request.Content, from: new Twilio.Types.PhoneNumber(_appsettings.Value.Twillio.TwillioPhoneNumber), statusCallback: new Uri("https://toolsmailapi.azurewebsites.net/twillio/sms/status"), to: new Twilio.Types.PhoneNumber(request.PhoneNumber));

            Log.Information($"[LOG INFORMATION] - Sms enviado com sucesso.\n");

            return new ApiResponse<object>(true, StatusCodes.SuccessOK, await Task.FromResult(message), new List<DadosNotificacao> { new DadosNotificacao("Sms enviado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]", exception, exception.Message);

            return new ApiResponse<object>(false, StatusCodes.ServerErrorInternalServerError ,new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }

    /// <summary>
    /// Método responsavel por fazer envio de mensagem via whatsapp
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<object>> Whatsapp(MessageRequest request)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TwillioService)} - METHOD {nameof(Whatsapp)}\n");

        try
        {
            TwilioClient.Init(_appsettings.Value.Twillio.TwillioAccountSID, _appsettings.Value.Twillio.TwillioAuthToken);

            var message = MessageResource.Create(body: request.Content, from: new Twilio.Types.PhoneNumber($"whatsapp:{_appsettings.Value.Twillio.TwillioWhatsappNumber}"), to: new Twilio.Types.PhoneNumber($"whatsapp:{request.PhoneNumber}"));

            Log.Information($"[LOG INFORMATION] - Whatsapp enviado com sucesso.\n");

            return new ApiResponse<object>(true, StatusCodes.SuccessOK, await Task.FromResult(message), new List<DadosNotificacao> { new DadosNotificacao("Whatsapp enviado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]", exception, exception.Message);

            return new ApiResponse<object>(false, StatusCodes.ServerErrorInternalServerError, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }
}
