using APPLICATION.DOMAIN.DTOS.RESPONSE;
using Microsoft.AspNetCore.Mvc;

namespace TOOLS.UTIL.API.CONTROLLER.CONFIG
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [HttpOptions("options")]
        public async Task<ApiResponse<object>> Options()
        {
            return await Task.FromResult(new ApiResponse<object>(true, APPLICATION.ENUMS.StatusCodes.SuccessAccepted, new List<DadosNotificacao> { new DadosNotificacao("Headers suportados pela aplicação.") }));
        }
    }
}
