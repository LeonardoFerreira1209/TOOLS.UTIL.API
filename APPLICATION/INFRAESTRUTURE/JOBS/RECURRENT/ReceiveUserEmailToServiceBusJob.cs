using APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.DOMAIN.DTOS.CONFIGURATION.SERVICEBUS.MESSAGE;
using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.INFRAESTRUTURE.JOBS.INTERFACES.RECURRENT;
using APPLICATION.INFRAESTRUTURE.SERVICEBUS.PROVIDER.USER;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace APPLICATION.INFRAESTRUTURE.JOBS.RECURRENT;

[ExcludeFromCodeCoverage]
public class ReceiveUserEmailToServiceBusJob : IReceiveUserEmailToServiceBusJob
{
    private readonly IEmailService _emailService;

    private readonly IUserEmailServiceBusReceiverProvider _userEmailServiceBusReceiverProvider;

    private readonly IOptions<AppSettings> _configuracoes;

    public ReceiveUserEmailToServiceBusJob(IEmailService emailService, IUserEmailServiceBusReceiverProvider userEmailServiceBusReceiverProvider, IOptions<AppSettings> configuracoes)
    {
        _emailService = emailService;

        _userEmailServiceBusReceiverProvider = userEmailServiceBusReceiverProvider;

        _configuracoes = configuracoes;
    }

    /// <summary>
    /// Executa o Job.
    /// </summary>
    public void Execute()
    {
        ProccessReceiveUserEmailToServiceBusJob().Wait();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task ProccessReceiveUserEmailToServiceBusJob()
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(ReceiveUserEmailToServiceBusJob)} - METHOD {nameof(ProccessReceiveUserEmailToServiceBusJob)}\n");

        try
        {
            Log.Information($"[LOG INFORMATION] - Processando Job de Delete de usuários sem pessaos vinculadas.\n");

            // get messages from service bus.
            var messageEntities = await _userEmailServiceBusReceiverProvider.GetMessagesAsync<UserEmailMessageDto>(10);
            
            // run foreach messages and prepare to mailRequest.
            messageEntities.ForEach(async message =>
            {
                Log.Information($"[LOG INFORMATION] - Processando mensagem {JsonConvert.SerializeObject(message.OriginalMessage)} do service bus .\n");

                await _emailService.Invite(new MailRequest
                {
                    Receivers = message.MappedMessage.Receivers,
                    ButtonText = message.MappedMessage.ButtonText,
                    Content = message.MappedMessage.Content,
                    Link = message.MappedMessage.Link,
                    Subject = message.MappedMessage.Subject,
                    TemplateName = message.MappedMessage.TemplateName
                });

                Log.Information($"[LOG INFORMATION] - Limpando mensagem processada do service bus.\n");

                // Complete service bus message.
                await _userEmailServiceBusReceiverProvider.CompleteMessageAsync(message.OriginalMessage);
            });

            Log.Information("[LOG INFORMATION] - Finalizando Job de Delete de usuários sem pessaos vinculadas\n");
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERRO] - Erro ao processar Job - ProcessFailedPersonCreateJob - ({0}).\n", exception.Message);
        }
    }
}
