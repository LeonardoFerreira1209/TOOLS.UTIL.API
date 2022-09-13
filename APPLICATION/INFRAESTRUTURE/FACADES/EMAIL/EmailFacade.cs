using APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.EMAIL;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace APPLICATION.INFRAESTRUTURE.FACADES.EMAIL;

/// <summary>
/// Classe responsável por envio de e-mails.
/// </summary>
public class EmailFacade
{
    private readonly IOptions<AppSettings> _appsettings;

    private readonly ITemplateService _templateService;

    public EmailFacade(IOptions<AppSettings> appsettings, ITemplateService templateService)
    {
        _appsettings = appsettings;

        _templateService = templateService;
    }

    /// <summary>
    /// Metodo que prepara o envio do e-mail.
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="subject"></param>
    /// <param name="templateName"></param>
    /// <param name="userId"></param>
    /// <param name="activateCode"></param>
    /// <exception cref="Exception"></exception>
    public async Task Invite(List<string> receiver, string templateName, string subject, string content, string link, string buttonText)
    {
        try
        {
            var message = new Message(receiver, subject, templateName, content, link, buttonText);

            var emailMessage = await CreateEmailBody(message);

            await Send(emailMessage);

        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    /// <summary>
    /// Método responsavel por criar o corpo do e-mail.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task<MimeMessage> CreateEmailBody(Message message)
    {
        try
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("receiver", _appsettings.Value.Email.From));

            emailMessage.To.AddRange(message.Receiver);

            emailMessage.Subject = message.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = await GetTemplate(message.TemplateName, message.Subject, message.Content, message.Link, message.ButtonText)
            };

            return await Task.FromResult(emailMessage);
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    /// <summary>
    /// Método responsavel por fazer acesso ao client e enviar o e-mail.
    /// </summary>
    /// <param name="emailMessage"></param>
    /// <exception cref="Exception"></exception>
    private async Task Send(MimeMessage emailMessage)
    {
        using var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(_appsettings.Value.Email.SmtpServer, _appsettings.Value.Email.Port, false);

            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync(_appsettings.Value.Email.From, _appsettings.Value.Email.Password);

            await client.SendAsync(emailMessage);
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
        finally
        {
            client.Disconnect(true);

            client.Dispose();
        }
    }

    /// <summary>
    /// Recupera o template do banco e aplica as alterações com base nos dados enviados.
    /// </summary>
    /// <param name="templateName"></param>
    /// <param name="titulo"></param>
    /// <param name="conteudoTexto"></param>
    /// <param name="linkBotao"></param>
    /// <param name="textoBotao"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private async Task<string> GetTemplate(string templateName, string titulo, string conteudoTexto, string linkBotao, string textoBotao)
    {
        var response = await _templateService.GetContentTemplateWithName(templateName);

        if (response.Sucesso) return await Task.FromResult(response.Dados.Replace("__titulo__", titulo).Replace("__content__", conteudoTexto).Replace("__link-botao__", linkBotao).Replace("__texto-botao__", textoBotao));

        throw new Exception("Nenhum template encontrado.");
    }
}