﻿using APPLICATION.INFRAESTRUTURE.JOBS.INTERFACES.BASE;
using FluentScheduler;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace APPLICATION.INFRAESTRUTURE.JOBS.FACTORY.FLUENTSCHEDULER;

[ExcludeFromCodeCoverage]
public class FluentSchedulerJobs : Registry, IFluentSchedulerJobs
{
    public FluentSchedulerJobs()
    {
        try
        {
            Log.Information($"[LOG INFORMATION] - Inicializando os Job do Fluent Scheduler.\n");

            NonReentrantAsDefault(); /*Schedule<IProcessDeleteUserWithoutPersonJob>().ToRunEvery(24).Hours();*/
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERRO] - Falha na inicialização do Job do Fluent Scheduler. ({exception.Message})\n");
        }
    }
}
