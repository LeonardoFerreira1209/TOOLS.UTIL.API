using System.Diagnostics.CodeAnalysis;

namespace APPLICATION.DOMAIN.DTOS.CONFIGURATION;

/// <summary>
/// Classe responsavel por receber os dados do Appsettings.
/// </summary>
public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public SwaggerInfo SwaggerInfo { get; set; }
    public Configuracoes Configuracoes { get; set; }
    public RetryPolicy RetryPolicy { get; set; }
    public ServiceBus ServiceBus { get; set; }
    public Email Email { get; set; }
    public Twillio Twillio { get; set; }
    public UrlBase UrlBase { get; set; }
}

/// <summary>
/// Classe responsável por receber dados de retry policy.
/// </summary>
public class RetryPolicy
{
    public string RetryOn { get; set; }
    public int RetryCount { get; set; }
    public int RetryEachSecond { get; set; }
}

/// <summary>
/// Classe de dados do serviceBus
/// </summary>
public class ServiceBus
{
    public int NumeroThreadsConsumer { get; set; }
    public string QueueUserEmail { get; set; }
    public string SubscriptionExemploName { get; set; }
    public int TempoReagendamentoMinutos { get; set; }
    public int QuantidadeMaximaDeRetentativas { get; set; }
}

/// <summary>
/// Classe de conexões.
/// </summary>
public class ConnectionStrings
{
    public string BaseDados { get; set; }
    public string ServiceBus { get; set; }
}

/// <summary>
/// Classe de config do swagger.
/// </summary>
public class SwaggerInfo
{
    public string ApiDescription { get; set; }
    public string ApiVersion { get; set; }
    public string UriMyGit { get; set; }
}

/// <summary>
/// Classe de config diversas.
/// </summary>
public class Configuracoes
{
    public int TimeOutDefault { get; set; }
    public int NumeroThreadsConsumer { get; set; }
    public string TopicoExemploName { get; set; }
    public string SubscriptionExemploName { get; set; }
    public int TempoReagendamentoMinutos { get; set; }
    public int QuantidadeMaximaDeRetentativas { get; set; }
}

/// <summary>
/// Classe de config de e-mail.
/// </summary>
public class Email
{
    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}

/// <summary>
/// Clase de confi do Twillio.
/// </summary>
public class Twillio
{
    public string TwillioWhatsappNumber { get; set; }
    public string TwillioPhoneNumber { get; set; }
    public string TwillioAccountSID { get; set; }
    public string TwillioAuthToken { get; set; }
}

public class UrlBase
{
    public string CHATGPT_BASE_URL { get; set; } 
}
