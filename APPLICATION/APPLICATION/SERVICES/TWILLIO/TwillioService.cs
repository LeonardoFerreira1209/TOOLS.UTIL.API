using APPLICATION.DOMAIN.CONTRACTS.REPOSITORIES.TWILLIO;
using APPLICATION.DOMAIN.CONTRACTS.SERVICES.TWILLIO;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.RESPONSE;
using APPLICATION.DOMAIN.DTOS.TWILLIO;
using APPLICATION.ENUMS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using StatusCodes = APPLICATION.ENUMS.StatusCodes;

namespace APPLICATION.APPLICATION.SERVICES.TWILLIO;

/// <summary>
/// Serviço de sms.
/// </summary>
public class TwillioService : ITwillioService
{
    private readonly IOptions<AppSettings> _appsettings;

    private readonly ITwillioRepository _twillioRepository;

    public TwillioService(IOptions<AppSettings> appsettings, ITwillioRepository twillioRepository)
    {
        _appsettings = appsettings;

        _twillioRepository = twillioRepository;
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
            // Iniciando acessp a Twillio.
            TwilioClient.Init(_appsettings.Value.Twillio.TwillioAccountSID, _appsettings.Value.Twillio.TwillioAuthToken);

            // Enviando mensagem.
            var message = MessageResource.Create(body: request.Content, from: new Twilio.Types.PhoneNumber(_appsettings.Value.Twillio.TwillioPhoneNumber), statusCallback: new Uri("https://toolsutilapi.azurewebsites.net/api/Twillio/sms/status"), to: new Twilio.Types.PhoneNumber(request.PhoneNumber));

            // Salvando mensagem no banco.
            await _twillioRepository.Save(new StatusSmsRequest
            {
                AccountSid = message.AccountSid,
                ApiVersion = message.ApiVersion,
                Body = message.Body,
                From = message.From.ToString(),
                MessageId = message.Sid,
                SmsSid = message.Sid,
                MessageStatus = message.Status.ToString(),
                SmsStatus = message.Status.ToString(),
                To = message.To.ToString(),
                DateCreated = message.DateCreated,
                DateUpdated = message.DateUpdated,
                DateSent = message.DateSent,
                ErrorCode = message.ErrorCode,
                ErrorMessage = message.ErrorMessage,
                NumMedia = message.NumMedia,
                NumSegments = message.NumSegments,
                Price = message.Price,
                PriceUnit = message.PriceUnit,
                Sid = message.Sid
            });

            Log.Information($"[LOG INFORMATION] - Sms enviado com sucesso.\n");

            // retorno success.
            return new ApiResponse<object>(true, StatusCodes.SuccessOK, await Task.FromResult(message), new List<DadosNotificacao> { new DadosNotificacao("Sms enviado com sucesso.\n") });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}", exception, exception.Message);

            // retorno error.
            return new ApiResponse<object>(false, StatusCodes.ServerErrorInternalServerError, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
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
            // Montando request.
            var statusSms = new StatusSmsRequest
            {
                SmsSid = formCollection["SmsSid"],
                MessageStatus = formCollection["MessageStatus"],
                AccountSid = formCollection["AccountSid"],
                SmsStatus = formCollection["SmsStatus"],
                From = formCollection["From"],
                To = formCollection["To"],
                MessageId = formCollection["MessageId"],
                ApiVersion = formCollection["ApiVersion"],
                Body = formCollection["Body"],
            };

            Log.Information($"Status sms: {JsonConvert.SerializeObject(statusSms)}");

            // Salvando request no banco.
            await _twillioRepository.Save(statusSms);

            Log.Information($"Status sms salvo no banco: {JsonConvert.SerializeObject(statusSms)}");

            // Retorno success.
            return new ApiResponse<object>(true, StatusCodes.SuccessOK, await Task.FromResult(statusSms), new List<DadosNotificacao> { new DadosNotificacao("Status sms retornado com sucesso.\n") });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}", exception, exception.Message);

            // Retorno error.
            return new ApiResponse<object>(false, StatusCodes.ServerErrorInternalServerError, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
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
            // Iniciando acessp a Twillio.
            TwilioClient.Init(_appsettings.Value.Twillio.TwillioAccountSID, _appsettings.Value.Twillio.TwillioAuthToken);

            // Enviando mensagem.
            var message = MessageResource.Create(body: request.Content, from: new Twilio.Types.PhoneNumber($"whatsapp:{_appsettings.Value.Twillio.TwillioWhatsappNumber}"), statusCallback: new Uri("https://toolsutilapi.azurewebsites.net/api/Twillio/whatsapp/status"), to: new Twilio.Types.PhoneNumber($"whatsapp:{request.PhoneNumber}"));

            // Salvando mensagem no banco.
            await _twillioRepository.Save(new StatusSmsRequest
            {
                AccountSid = message.AccountSid,
                ApiVersion = message.ApiVersion,
                Body = message.Body,
                From = message.From.ToString(),
                MessageId = message.Sid,
                SmsSid = message.Sid,
                MessageStatus = message.Status.ToString(),
                SmsStatus = message.Status.ToString(),
                To = message.To.ToString(),
                DateCreated = message.DateCreated,
                PriceUnit = message.PriceUnit,
                Price = message.Price,
                DateSent = message.DateSent,
                DateUpdated = message.DateUpdated,
                ErrorCode = message.ErrorCode,
                ErrorMessage = message.ErrorMessage,
                NumMedia = message.NumMedia,
                NumSegments = message.NumSegments,
                Sid = message.Sid
            });

            Log.Information($"[LOG INFORMATION] - Whatsapp enviado com sucesso.\n");

            // Retorno sucesso.
            return new ApiResponse<object>(true, StatusCodes.SuccessOK, await Task.FromResult(message), new List<DadosNotificacao> { new DadosNotificacao("Whatsapp enviado com sucesso.\n") });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}", exception, exception.Message);

            // Retorno error.
            return new ApiResponse<object>(false, StatusCodes.ServerErrorInternalServerError, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }

    /// <summary>
    /// Método responsavel por receber um status de sms.
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResponse<object>> WhatsappStatus(IFormCollection formCollection)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TwillioService)} - METHOD {nameof(WhatsappStatus)}\n");

        try
        {
            // Montando request.
            var statusSms = new StatusSmsRequest
            {
                SmsSid = formCollection["SmsSid"],
                MessageStatus = formCollection["MessageStatus"],
                AccountSid = formCollection["AccountSid"],
                SmsStatus = formCollection["SmsStatus"],
                From = formCollection["From"],
                To = formCollection["To"],
                MessageId = formCollection["MessageId"],
                ApiVersion = formCollection["ApiVersion"],
                Body = formCollection["Body"],
                ProfileName = formCollection["ProfileName"],
                MediaUrl = formCollection["MediaUrl0"]
            };

            Log.Information($"Status sms: {JsonConvert.SerializeObject(statusSms)}");

            // Salvando request no banco.
            await _twillioRepository.Save(statusSms);

            Log.Information($"Status sms salvo no banco: {JsonConvert.SerializeObject(statusSms)}");

            // Retorno success.
            return new ApiResponse<object>(true, StatusCodes.SuccessOK, await Task.FromResult(statusSms), new List<DadosNotificacao> { new DadosNotificacao("Status whatsapp retornado com sucesso.\n") });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}", exception, exception.Message);

            // Retorno error.
            return new ApiResponse<object>(false, StatusCodes.ServerErrorInternalServerError, new List<DadosNotificacao> { new DadosNotificacao(exception.Message) });
        }
    }
}
