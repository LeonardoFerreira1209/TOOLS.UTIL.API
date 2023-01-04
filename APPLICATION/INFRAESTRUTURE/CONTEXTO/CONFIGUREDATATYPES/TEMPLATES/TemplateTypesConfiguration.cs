using APPLICATION.DOMAIN.ENTITY.TEMPLATES;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APPLICATION.INFRAESTRUTURE.CONTEXTO.CONFIGUREDATATYPES.TEMPLATES;

public class TemplateTypesConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        // Renomeando nome.
        builder.ToTable("Templates").HasKey(template => template.Id);

        // Guid
        builder.Property(template => template.Id).IsRequired();

        // String
        builder.Property(template => template.Name).HasMaxLength(50).IsRequired();
        builder.Property(template => template.Description).HasMaxLength(80).IsRequired();
        builder.Property(template => template.Content).IsRequired();
    }
}
