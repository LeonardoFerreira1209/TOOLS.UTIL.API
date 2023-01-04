namespace APPLICATION.DOMAIN.ENTITY.TEMPLATES;

/// <summary>
/// Entidade de templates
/// </summary>
public class Template
{
    /// <summary>
    /// Identificador
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descrição do template
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Conteudo html do template
    /// </summary>
    public string Content { get; set; }
}
