using APPLICATION.DOMAIN.CONTRACTS.API.OPENAI;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using Swashbuckle.AspNetCore.Annotations;

namespace TOOLS.UTIL.API.CONTROLLER.OPENAI;

[Route("api/[controller]")]
[ApiController]
public class OpenAiController : ControllerBase
{
    private readonly IOpenAiExternal _openApiExternal;

    public OpenAiController(IOpenAiExternal openApiExternal)
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
    public async Task<ActionResult> ChatGPTCompletions([FromBody] string prompt)
    {
        try
        {
            var apikey = "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";
            string answer = string.Empty;
            var openai = new OpenAIAPI(apikey);

            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = prompt;

            completion.Model = "text-davinci-003";
            completion.MaxTokens = 4000;
            var result = openai.Completions.CreateCompletionAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }

                return Ok(answer);
            }
            else
            {
                return BadRequest("Not found");
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
