using APPLICATION.DOMAIN.CONTRACTS.REPOSITORIES.TWILLIO;
using APPLICATION.DOMAIN.DTOS.TWILLIO;
using APPLICATION.DOMAIN.ENTITY.TWILLIO;
using APPLICATION.INFRAESTRUTURE.CONTEXTO;
using Serilog;

namespace APPLICATION.INFRAESTRUTURE.REPOSITORY.TWILLIO;

/// <summary>
/// Repositorio de twillio.
/// </summary>
public class TwillioRepository : ITwillioRepository
{
    private readonly Context _context;

    public TwillioRepository(Context context)
    {
        _context = context;
    }

    /// <summary>
    ///  Metodo responsavel por salvar mensagem da twillio.
    /// </summary>
    /// <param name="messageTwillioEntity"></param>
    /// <returns></returns>
    public async Task Save(StatusSmsRequest statusSmsRequest)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TwillioRepository)} - METHOD {nameof(Save)}\n");

        await _context.MessagesTwillio.AddAsync(new MessageTwillio
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
            ProfileName = statusSmsRequest.ProfileName,
            MediaUrl = statusSmsRequest.MediaUrl
        });

        await _context.SaveChangesAsync();
    }
}
