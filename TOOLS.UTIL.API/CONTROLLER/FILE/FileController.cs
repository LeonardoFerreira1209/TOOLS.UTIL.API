using APPLICATION.DOMAIN.CONTRACTS.SERVICES.FILE;
using APPLICATION.DOMAIN.DTOS.RESPONSE.UTILS;
using APPLICATION.DOMAIN.UTILS;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace TOOLS.UTIL.API.CONTROLLER.OPENAPI;

[Route("api/[controller]")][ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// Método responsavel por receber um arquivo gravar no blob e retornar dados.
    /// </summary>
    /// <returns></returns>
    [HttpPost("send/blobstorage")][EnableCors("CorsPolicy")]
    [SwaggerOperation(Summary = "Gravar arquivos no blob.", Description = "Método responsavel por receber um arquivo gravar no blob e retornar dados.")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse<object>> SendFileToBlob(IFormFile file)
    {
        using (LogContext.PushProperty("Controller", "FileController"))
        using (LogContext.PushProperty("Metodo", "SendFileToBlob"))
        {
            return await Tracker.Time(() => _fileService.InviteFileToAzureBlobStorageAndReturnUri(Request.Form.Files[0]), "Gravar arquivos no blob");
        }
    }
}
