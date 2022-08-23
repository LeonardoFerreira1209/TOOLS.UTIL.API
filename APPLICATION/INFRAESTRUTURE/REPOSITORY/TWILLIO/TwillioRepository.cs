using APPLICATION.DOMAIN.CONTRACTS.REPOSITORIES.TEMPLATES;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.TWILLIO;
using APPLICATION.INFRAESTRUTURE.CONTEXTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace APPLICATION.INFRAESTRUTURE.REPOSITORY.TWILLIO;

/// <summary>
/// Repositorio de twillio.
/// </summary>
public class TwillioRepository : ITwillioRepository
{
    private readonly Contexto _contexto;

    public TwillioRepository(Contexto contexto)
    {
        _contexto = contexto;
    }

    /// <summary>
    ///  Metodo responsavel por salvar mensagem da twillio.
    /// </summary>
    /// <param name="messageTwillioEntity"></param>
    /// <returns></returns>
    public async Task Save(StatusSmsRequest statusSmsRequest)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TwillioRepository)} - METHOD {nameof(Save)}\n");

        await _contexto.MessagesTwillio.AddAsync(new MessageTwillio
        {
           AccountSid = statusSmsRequest.AccountSid,
           ApiVersion = statusSmsRequest.ApiVersion,
           Body = statusSmsRequest.Body,
           From = statusSmsRequest.From,
           To = statusSmsRequest.To,
           MessageId = statusSmsRequest.MessageId,
           MessageStatus = statusSmsRequest.MessageStatus,
           SmsSid = statusSmsRequest.SmsSid,
           SmsStatus = statusSmsRequest.SmsStatus,
           DateCreated = statusSmsRequest.DateCreated,
           DateSent = statusSmsRequest.DateSent,
           DateUpdated = statusSmsRequest.DateUpdated,
           ErrorCode = statusSmsRequest.ErrorCode,
           ErrorMessage = statusSmsRequest.ErrorMessage,
           NumMedia = statusSmsRequest.NumMedia,
           NumSegments = statusSmsRequest.NumSegments,
           Price = statusSmsRequest.Price,
           PriceUnit = statusSmsRequest.PriceUnit,
           Sid = statusSmsRequest.Sid,
           ProfileName = statusSmsRequest.ProfileName
        });

        await _contexto.SaveChangesAsync();
    }
}
