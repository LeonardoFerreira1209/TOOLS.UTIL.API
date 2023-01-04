using APPLICATION.DOMAIN.DTOS.TWILLIO;

namespace APPLICATION.DOMAIN.CONTRACTS.REPOSITORIES.NOTIFICATIONS.TWILLIO;

/// <summary>
/// Repositorio onde salva os registros de notificações.
/// </summary>
public interface ITwillioRepository
{
    /// <summary>
    /// Método responsavel por salvar a mensagem no repositório. 
    /// </summary>
    /// <returns></returns>
    Task Save(StatusSmsRequest statusSmsRequest);
}
