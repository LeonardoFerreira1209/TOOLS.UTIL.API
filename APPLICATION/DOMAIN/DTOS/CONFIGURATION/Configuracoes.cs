﻿namespace APPLICATION.DOMAIN.DTOS.CONFIGURATION;

/// <summary>
/// Classe responsavel por receber os dados do Appsettings.
/// </summary>
public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public SwaggerInfo SwaggerInfo { get; set; }
    public Configuracoes Configuracoes { get; set; }
    public RetryPolicy RetryPolicy { get; set; }
    public Email Email { get; set; }
    public Twillio Twillio { get; set; }
}

public class RetryPolicy
{
    public string RetryOn { get; set; }
    public int RetryCount { get; set; }
    public int RetryEachSecond { get; set; }
}

public class ConnectionStrings
{
    public string BaseDados { get; set; }
}

public class SwaggerInfo
{
    public string ApiDescription { get; set; }
    public string ApiVersion { get; set; }
    public string UriMyGit { get; set; }
}

public class Configuracoes
{
    public int TimeOutDefault { get; set; }
    public int NumeroThreadsConsumer { get; set; }
    public string TopicoExemploName { get; set; }
    public string SubscriptionExemploName { get; set; }
    public int TempoReagendamentoMinutos { get; set; }
    public int QuantidadeMaximaDeRetentativas { get; set; }
}

public class Email
{
    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}

public class Twillio
{
    public string TwillioPhoneNumber { get; set; }
    public string TwillioAccountSID { get; set; }
    public string TwillioAuthToken { get; set; }
}
