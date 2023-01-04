using FluentScheduler;

namespace APPLICATION.INFRAESTRUTURE.JOBS.INTERFACES.RECURRENT;

public interface IReceiveUserEmailToServiceBusJob : IJob
{
    /// <summary>
    /// Processa o job de recebimento de mensagens da queue de email do service bus.
    /// </summary>
    /// <returns></returns>
    Task ProccessReceiveUserEmailToServiceBusJob();
}
