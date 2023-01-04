using APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;
using APPLICATION.DOMAIN.CONTRACTS.SERVICES.TEMPLATE;
using APPLICATION.DOMAIN.DTOS.RESPONSE;
using APPLICATION.DOMAIN.UTILS;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace TOOLS.UTIL.API.CONTROLLER.EMAIL
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ITemplateService _templateService;

        public EmailController(IEmailService emailService, ITemplateService templateService)
        {
            _emailService = emailService;
            _templateService = templateService;
        }

        /// <summary>
        /// Método responsavel por fazer upload de um arquvivo html.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("upload/template")][EnableCors("CorsPolicy")]
        [SwaggerOperation(Summary = "Upload de arquivo.", Description = "Método responsavel por fazer um upload.")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse<object>> UploadTemplate(string name, string description, IFormFile formFile)
        {
            using (var reader = new StreamReader(formFile.OpenReadStream(), Encoding.UTF8))
            {
                string fileContent = await reader.ReadToEndAsync();

                using (LogContext.PushProperty("Controller", "EmailController"))
                using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(new List<string> { name, description, fileContent })))
                using (LogContext.PushProperty("Metodo", "UploadTemplate"))
                {
                    return await Tracker.Time(() => _templateService.Save(name, description, fileContent), "Salvar template");
                }
            }
        }
    }
}
