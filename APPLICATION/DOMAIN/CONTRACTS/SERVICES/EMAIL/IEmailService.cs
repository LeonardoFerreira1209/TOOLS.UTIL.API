using APPLICATION.DOMAIN.DTOS.REQUEST;
using APPLICATION.DOMAIN.DTOS.RESPONSE;

namespace APPLICATION.DOMAIN.CONTRACTS.SERVICES.EMAIL;

public interface IEmailService
{
    Task<ApiResponse<object>> Invite(MailRequest request);
}
