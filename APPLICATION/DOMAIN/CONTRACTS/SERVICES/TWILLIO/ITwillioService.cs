using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using APPLICATION.DOMAIN.DTOS.TWILLIO;
using Microsoft.AspNetCore.Http;

namespace APPLICATION.DOMAIN.CONTRACTS.SERVICES.TWILLIO;

public interface ITwillioService
{
    /// <summary>
    /// Método responsavel por enviar sms.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResponse<object>> SmsInvite(MessageRequest request);

    /// <summary>
    /// Método responsavel por receber um statusdo sms.
    /// </summary>
    /// <param name="formCollection"></param>
    /// <returns></returns>
    Task<ApiResponse<object>> SmsStatus(IFormCollection formCollection);

    /// <summary>
    /// Método responsavel por receber um status de sms.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponse<object>> WhatsappStatus(IFormCollection formCollection);

    /// <summary>
    /// Envio de mensagem para o Whatsapp.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResponse<object>> WhatsappInvite(MessageRequest request);
}
