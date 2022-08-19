using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;

namespace APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;

public interface ISmsService
{
    /// <summary>
    /// Método responsavel por enviar sms.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResponse<object>> Invite(SmsRequest request);
}
