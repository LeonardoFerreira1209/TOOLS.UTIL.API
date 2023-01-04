using APPLICATION.DOMAIN.DTOS.RESPONSE;

namespace APPLICATION.DOMAIN.CONTRACTS.SERVICES.TEMPLATE;

/// <summary>
/// Serviço de template
/// </summary>
public interface ITemplateService
{
    /// <summary>
    /// Método responsável por salvar um template de e-mail.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="fileContent"></param>
    /// <returns></returns>
    Task<ApiResponse<object>> Save(string name, string description, string fileContent);

    /// <summary>
    /// Método responsável por recuperar um template de e-mail através do nome.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<ApiResponse<string>> GetContentTemplateWithName(string name);
}
