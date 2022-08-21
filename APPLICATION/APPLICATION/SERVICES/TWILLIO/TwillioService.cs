using APPLICATION.DOMAIN.CONTRACTS.SERVICES.TWILLIO;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
    public async Task<ApiResponse<object>> SmsInvite(MessageRequest request)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TwillioService)} - METHOD {nameof(SmsInvite)}\n");

        try
        {
            TwilioClient.Init(_appsettings.Value.Twillio.TwillioAccountSID, _appsettings.Value.Twillio.TwillioAuthToken);

            var message = MessageResource.Create(body: request.Content, from: new Twilio.Types.PhoneNumber(_appsettings.Value.Twillio.TwillioPhoneNumber), statusCallback: new Uri("https://toolsmailapi.azurewebsites.net/api/Twillio/sms/status"), to: new Twilio.Types.PhoneNumber(request.PhoneNumber));

            Log.Information($"[LOG INFORMATION] - Sms enviado com sucesso.\n");

            return new ApiResponse<object>(true, DOMAIN.ENUM.StatusCodes.SuccessOK, await Task.FromResult(message), new List<DadosNotificacao> { new DadosNotificacao("Sms enviado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}", exception, exception.Message);

            return new ApiResponse<object>(false, DOMAIN.ENUM.StatusCodes.ServerErrorInternalServerError ,new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }

    /// <summary>
    /// Método responsavel por receber um status de sms.
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResponse<object>> SmsStatus(IFormCollection formCollection)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TwillioService)} - METHOD {nameof(SmsStatus)}\n");

        try
        {
            var statusSms = new StatusSmsRequest
            {
                SmsSid = formCollection["MessageSid"],
                MessageStatus = formCollection["MessageStatus"],
                AccountSid = formCollection["AccountSid"],
                SmsStatus = formCollection["SmsStatus"],
                From = formCollection["From"],
                To = formCollection["To"],
                MessageId = formCollection["MessageId"],
                ApiVersion = formCollection["ApiVersion"]
            };

            Log.Information($"Status sms: {JsonConvert.SerializeObject(statusSms)}");

            return new ApiResponse<object>(true, DOMAIN.ENUM.StatusCodes.SuccessOK, await Task.FromResult(statusSms), new List<DadosNotificacao> { new DadosNotificacao("Status sms retornado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}", exception, exception.Message);

            return new ApiResponse<object>(false, DOMAIN.ENUM.StatusCodes.ServerErrorInternalServerError, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }

    /// <summary>
    /// Método responsavel por fazer envio de mensagem via whatsapp
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<object>> WhatsappInvite(MessageRequest request)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TwillioService)} - METHOD {nameof(WhatsappInvite)}\n");

        try
        {
            TwilioClient.Init(_appsettings.Value.Twillio.TwillioAccountSID, _appsettings.Value.Twillio.TwillioAuthToken);

            var message = MessageResource.Create(body: request.Content, from: new Twilio.Types.PhoneNumber($"whatsapp:{_appsettings.Value.Twillio.TwillioWhatsappNumber}"), statusCallback: new Uri("https://toolsmailapi.azurewebsites.net/api/Twillio/sms/status"), to: new Twilio.Types.PhoneNumber($"whatsapp:{request.PhoneNumber}"));

            Log.Information($"[LOG INFORMATION] - Whatsapp enviado com sucesso.\n");

            return new ApiResponse<object>(true, DOMAIN.ENUM.StatusCodes.SuccessOK, await Task.FromResult(message), new List<DadosNotificacao> { new DadosNotificacao("Whatsapp enviado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}", exception, exception.Message);

            return new ApiResponse<object>(false, DOMAIN.ENUM.StatusCodes.ServerErrorInternalServerError, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }
}
