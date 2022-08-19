using APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using APPLICATION.DOMAIN.ENUM;
using Microsoft.Extensions.Options;
using Serilog;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace APPLICATION.APPLICATION.SERVICES.EMAIL;

/// <summary>
/// Serviço de sms.
/// </summary>
public class SmsService : ISmsService
{
    private readonly IOptions<AppSettings> _appsettings;

    public SmsService(IOptions<AppSettings> appsettings)
    {
        _appsettings = appsettings;
    }

    /// <summary>
    /// Método responsável por fazer o envio de sms.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<object>> Invite(SmsRequest request)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(SmsService)} - METHOD {nameof(Invite)}\n");

        try
        {
            TwilioClient.Init(_appsettings.Value.Sms.TwillioAccountSID, _appsettings.Value.Sms.TwillioAuthToken);

            var message = MessageResource.Create(body: request.Content, from: new Twilio.Types.PhoneNumber(_appsettings.Value.Sms.TwillioPhoneNumber),  to: new Twilio.Types.PhoneNumber(request.PhoneNumber));

            Log.Information($"[LOG INFORMATION] - Sms enviado com sucesso.\n");

            return new ApiResponse<object>(true, StatusCodes.SuccessOK, await Task.FromResult(message), new List<DadosNotificacao> { new DadosNotificacao("Sms enviado com sucesso.") });
        }
        catch (Exception exception)
        {
            Log.Error("[LOG ERROR]", exception, exception.Message);

            return new ApiResponse<object>(false, StatusCodes.ServerErrorInternalServerError ,new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }
}
