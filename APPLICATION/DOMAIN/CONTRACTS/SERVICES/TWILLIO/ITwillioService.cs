using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;

namespace APPLICATION.DOMAIN.CONTRACTS.SERVICES.TWILLIO;

public interface ITwillioService
{
    /// <summary>
    /// Método responsavel por enviar sms.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResponse<object>> Sms(MessageRequest request);
    
    /// <summary>
    /// Envio de mensagem para o Whatsapp.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResponse<object>> Whatsapp(MessageRequest request);
}
