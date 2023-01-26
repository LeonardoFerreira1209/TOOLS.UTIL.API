using APPLICATION.DOMAIN.DTOS.CONFIGURATION;
using APPLICATION.INFRAESTRUTURE.SERVICEBUS.PROVIDER.BASE;
using Microsoft.Extensions.Options;

namespace APPLICATION.INFRAESTRUTURE.SERVICEBUS.PROVIDER.USER;

/// <summary>
/// Classe de envio de email do usuário para serivice bus.
/// </summary>
public class UserEmailServiceBusReceiverProvider : ServiceBusReceiverProviderBase, IUserEmailServiceBusReceiverProvider
{
    /// <summary>
    /// Construtor.
    /// </summary>
    /// <param name="configuracoes"></param>
    public UserEmailServiceBusReceiverProvider(IOptions<AppSettings> configuracoes) : base(configuracoes.Value.ConnectionStrings.ServiceBus, configuracoes.Value.ServiceBus.QueueEmail) { }
}
