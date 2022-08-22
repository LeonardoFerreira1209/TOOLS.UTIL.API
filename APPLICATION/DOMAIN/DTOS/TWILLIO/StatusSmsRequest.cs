namespace APPLICATION.DOMAIN.DTOS.TWILLIO;

/// <summary>
/// Classe de recebimento de parametos para o status de sms.
/// </summary>
public class StatusSmsRequest
{
    /// <summary>
    /// Id do sms.
    /// </summary>
    public string SmsSid { get; set; }

    /// <summary>
    /// Status do sms.
    /// </summary>
    public string SmsStatus { get; set; }

    /// <summary>
    /// Status da mensagem.
    /// </summary>
    public string MessageStatus { get; set; }

    /// <summary>
    /// Número da pessoa que enviou a mensagem.
    /// </summary>
    public string To { get; set; }

    /// <summary>
    /// Id da mensagem.
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    /// Sid da conta.
    /// </summary>
    public string AccountSid { get; set; }

    /// <summary>
    /// Número da pessoa que recebeu a mensagem.
    /// </summary>
    public string From { get; set; }

    /// <summary>
    /// Versão da API da twillio.
    /// </summary>
    public string ApiVersion { get; set; }

    /// <summary>
    /// Corpo da mensagem.
    /// </summary>
    public string Body { get; set; }
}