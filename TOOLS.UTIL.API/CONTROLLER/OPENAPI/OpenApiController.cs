using APPLICATION.DOMAIN.CONTRACTS.API.OPENAPI;
using APPLICATION.DOMAIN.DTOS.REQUEST.OPENAPI;
using APPLICATION.DOMAIN.DTOS.RESPONSE.OPENAPI;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TOOLS.UTIL.API.CONTROLLER.OPENAPI;

[Route("api/[controller]")]
[ApiController]
public class OpenApiController : ControllerBase
{
    private readonly IOpenApiExternal _openApiExternal;

    public OpenApiController(IOpenApiExternal openApiExternal)
    {
        _openApiExternal = openApiExternal;
    }

    /// <summary>
    /// Método responsavel por fazer requisição ao OpenApi Completions.
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    [HttpPost("completions")]
    [EnableCors("CorsPolicy")]
    [SwaggerOperation(Summary = "Requisição a api de completions.", Description = "Método responsavel por fazer requisição a api de completions.")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<OpenApiCompletionsResponse> ChatGPTCompletions(OpenApiCompletionsRequest openApiCompletionsRequest)
    {
        try
        {
            var apikey = "Bearer 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";

            var response = await _openApiExternal.SendQuestion(apikey, openApiCompletionsRequest);

            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
