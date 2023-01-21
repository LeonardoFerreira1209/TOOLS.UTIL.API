using APPLICATION.DOMAIN.CONTRACTS.SERVICES.TEMPLATE;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.EMAIL;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;

namespace APPLICATION.INFRAESTRUTURE.FACADES.EMAIL;

/// <summary>
/// Classe responsável por envio de e-mails.
/// </summary>
public class EmailFacade
{
    private readonly IOptions<AppSettings> _appsettings;

    private readonly ITemplateService _templateService;

    /// <summary>
    /// Construtor com parametros.
    /// </summary>
    /// <param name="appsettings"></param>
    /// <param name="templateService"></param>
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
            Log.Error($"[LOG ERROR] - {exception.Message}\n", exception, exception.Message);

            throw;
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
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(EmailFacade)} - METHOD {nameof(CreateEmailBody)}\n");

        try
        {
            // Instance of MimeMessage.
            var emailMessage = new MimeMessage();

            Log.Information($"[LOG INFORMATION] - Preparando e-mail de TOOLS.API para {_appsettings.Value.Email.From}.\n");

            // Prepare Mailbox address.
            emailMessage.From.Add(new MailboxAddress("TOOLS.API", _appsettings.Value.Email.From));

            // Set receivers in message.
            emailMessage.To.AddRange(message.Receiver);

            // Set subject in e-mail.
            emailMessage.Subject = message.Subject;

            // Set e-mail body in text.
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = await GetTemplate(message.TemplateName, message.Subject, message.Content, message.Link, message.ButtonText)
            };

            Log.Information($"[LOG INFORMATION] - E-mail preparado com sucesso.\n");

            return await Task.FromResult(emailMessage);
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}\n", exception, exception.Message);

            throw;
        }
    }

    /// <summary>
    /// Método responsavel por fazer acesso ao client e enviar o e-mail.
    /// </summary>
    /// <param name="emailMessage"></param>
    /// <exception cref="Exception"></exception>
    private async Task Send(MimeMessage emailMessage)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(EmailFacade)} - METHOD {nameof(Send)}\n");

        using var client = new SmtpClient();

        try
        {
            Log.Information($"[LOG INFORMATION] - Conectando ao cliente com o SmtpServer {_appsettings.Value.Email.SmtpServer} & Porta {_appsettings.Value.Email.Port}\n");

            await client.ConnectAsync(_appsettings.Value.Email.SmtpServer, _appsettings.Value.Email.Port, false);

            client.AuthenticationMechanisms.Remove("XOAUTH2");

            Log.Information($"[LOG INFORMATION] - Autenticando o cliente {_appsettings.Value.Email.From}\n");

            await client.AuthenticateAsync(_appsettings.Value.Email.From, _appsettings.Value.Email.Password);

            Log.Information($"[LOG INFORMATION] - Enviando e-mail {emailMessage.ToString()}.\n");

            await client.SendAsync(emailMessage);
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}\n", exception, exception.Message);

            throw;
        }
        finally
        {
            Log.Information($"[LOG INFORMATION] - Desconectando cliente.\n");

            client.Disconnect(true);
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
        try
        {
            var response = await _templateService.GetContentTemplateWithName(templateName);

            if (response.Sucesso) return await Task.FromResult(response.Dados.Replace("__titulo__", titulo).Replace("__content__", conteudoTexto).Replace("__link-botao__", linkBotao).Replace("__texto-botao__", textoBotao));

            return null;
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}\n", exception, exception.Message);

            throw;
        }
    }
}