using APPLICATION.DOMAIN.DTOS.REQUEST.OPENAPI;
using APPLICATION.DOMAIN.DTOS.RESPONSE.OPENAPI;
using Refit;

namespace APPLICATION.DOMAIN.CONTRACTS.API.OPENAPI;

/// <summary>
/// Interface de chamada do ChatGPT com o Refit.
/// </summary>
public interface IOpenApiExternal
{
    /// <summary>
    /// Chama a API de Completions do ChatGpt
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="chatGptCompletionsRequest"></param>
    /// <returns></returns>
    [Post("/v1/completions")]
    Task<OpenApiCompletionsResponse> SendQuestion([Header("Authorization")] string apiKey, [Body] OpenApiCompletionsRequest chatGptCompletionsRequest);
}
