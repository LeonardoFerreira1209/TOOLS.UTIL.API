using MimeKit;

namespace APPLICATION.DOMAIN.DTOS.EMAIL;

/// <summary>
/// Classe de mensagem para o e-mail.
/// </summary>
public class Message
{
    public Message(IEnumerable<string> receiver, string subject, string content)
    {
        Receiver = new List<MailboxAddress>();

        Receiver.AddRange(receiver.Select(r => new MailboxAddress("receiver", r)));

        Subject = subject;

        Content = content;
    }

    public List<MailboxAddress> Receiver { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
}
