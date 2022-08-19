﻿using MimeKit;

namespace APPLICATION.DOMAIN.DTOS.EMAIL;

/// <summary>
/// Classe de mensagem para o e-mail.
/// </summary>
public class Message
{
    public Message(IEnumerable<string> receiver, string subject, string templateName, string content, string link, string buttonText)
    {
        Receiver = new List<MailboxAddress>();

        Receiver.AddRange(receiver.Select(r => new MailboxAddress("Leo.Almeida", r)));

        Subject = subject;

        TemplateName = templateName;

        Content = content;

        Link = link;

        ButtonText = buttonText;
    }

    public List<MailboxAddress> Receiver { get; set; }
    public string Subject { get; set; }
    public string TemplateName { get; set; }
    public string Content { get; set; }
    public string Link { get; set; }
    public string ButtonText { get; set; }
}
