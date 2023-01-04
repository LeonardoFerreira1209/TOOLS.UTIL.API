using APPLICATION.DOMAIN.ENTITY.TEMPLATES;
using APPLICATION.DOMAIN.ENTITY.TWILLIO;
using APPLICATION.INFRAESTRUTURE.CONTEXTO.CONFIGUREDATATYPES.TEMPLATES;
using APPLICATION.INFRAESTRUTURE.CONTEXTO.CONFIGUREDATATYPES.TWILLIO;
using Microsoft.EntityFrameworkCore;

namespace APPLICATION.INFRAESTRUTURE.CONTEXTO;

/// <summary>
/// Classe de configuração do banco de dados.
/// </summary>
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
        Database.EnsureCreated();
    }

    /// <summary>
    /// Configrações fos datatypes.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configutrations
        modelBuilder
            // Template
            .ApplyConfiguration(new TemplateTypesConfiguration())
            // Message Twillio
            .ApplyConfiguration(new MessageTwillioTypesConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Sets de tabelas no banco.
    /// </summary>
    #region M
    public DbSet<MessageTwillio> MessagesTwillio { get; set; }
    #endregion

    #region T
    public DbSet<Template> Templates { get; set; }
    #endregion
}
