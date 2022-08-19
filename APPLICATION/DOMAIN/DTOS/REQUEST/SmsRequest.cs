﻿namespace APPLICATION.DOMAIN.DTOS.REQUEST;

/// <summary>
/// Classe de recebimento de parametos para o envio do sms.
/// </summary>
public class SmsRequest
{
    /// <summary>
    /// Número do celular.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Conteudo do sms (texto).
    /// </summary>
    public string Content { get; set; }

}
