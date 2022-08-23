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

    /// <summary>
    /// Data em que a mensagem foi criada.
    /// </summary>
    public DateTime? DateCreated { get; set; }

    /// <summary>
    /// Data em que a mensagem foi enviada.
    /// </summary>
    public DateTime? DateSent { get; set; }

    /// <summary>
    /// Data em que a mensagem foi atualizada.
    /// </summary>
    public DateTime? DateUpdated { get; set; }

    /// <summary>
    /// Código de erro.
    /// </summary>
    public int? ErrorCode { get; set; }

    /// <summary>
    /// Mensagem de erro,
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Número de midias.
    /// </summary>
    public string NumMedia { get; set; }

    /// <summary>
    /// Número de segmentos.
    /// </summary>
    public string NumSegments { get; set; }

    /// <summary>
    /// Valor.
    /// </summary>
    public string Price { get; set; }

    /// <summary>
    /// Valor unitário.
    /// </summary>
    public string PriceUnit { get; set; }

    /// <summary>
    /// Id da mensagem.
    /// </summary>
    public string Sid { get; set; }

    /// <summary>
    /// Nome do Perfil.
    /// </summary>
    public string ProfileName { get; set; }

    /// <summary>
    /// Canal do endereço.
    /// </summary>
    public string ChannelToAddress { get; set; }

    /// <summary>
    /// Prefixo do canal.
    /// </summary>
    public string ChannelPrefix { get; set; }

    /// <summary>
    /// Canal de instalação.
    /// </summary>
    public string ChannelInstallSid { get; set; }

    /// <summary>
    /// Media URL.
    /// </summary>
    public string MediaUrl { get; set; }
}