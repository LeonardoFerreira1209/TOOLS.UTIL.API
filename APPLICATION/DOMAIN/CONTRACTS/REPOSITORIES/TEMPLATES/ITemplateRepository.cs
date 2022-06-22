using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.DOMAIN.CONTRACTS.REPOSITORIES.TEMPLATES;

/// <summary>
/// Repositorio de templates
/// </summary>
public interface ITemplateRepository
{
    /// <summary>
    /// Salvar um template no banco.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="fileContent"></param>
    /// <returns></returns>
    Task Save(string name, string description, string fileContent);

    /// <summary>
    /// Recuperar template pelo nome.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<string> GetContentTemplateWithName(string name);
}
