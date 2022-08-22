using APPLICATION.DOMAIN.DTOS.TWILLIO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APPLICATION.INFRAESTRUTURE.CONTEXTO.CONFIGUREDATATYPES.TEMPLATES;

public class MessageTwillioTypesConfiguration : IEntityTypeConfiguration<MessageTwillio>
{
    public void Configure(EntityTypeBuilder<MessageTwillio> builder)
    {
        // Renomeando nome.
        builder.ToTable("MessagesTwillio").HasKey(message => message.Id);

        // Guid
        builder.Property(message => message.Id).IsRequired();

        // String
        builder.Property(message => message.From);
        builder.Property(message => message.To);
        builder.Property(message => message.MessageStatus);
        builder.Property(message => message.SmsStatus);
        builder.Property(message => message.Body);
        builder.Property(message => message.AccountSid);
        builder.Property(message => message.ApiVersion);
        builder.Property(message => message.MessageId);
        builder.Property(message => message.SmsSid);
    }
}
