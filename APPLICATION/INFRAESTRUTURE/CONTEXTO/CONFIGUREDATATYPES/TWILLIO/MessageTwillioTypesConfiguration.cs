using APPLICATION.DOMAIN.ENTITY.TWILLIO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APPLICATION.INFRAESTRUTURE.CONTEXTO.CONFIGUREDATATYPES.TWILLIO;

public class MessageTwillioTypesConfiguration : IEntityTypeConfiguration<MessageTwillio>
{
    public void Configure(EntityTypeBuilder<MessageTwillio> builder)
    {
        // Renomeando nome.
        builder.ToTable("MessagesTwillio").HasKey(message => message.Id);

        // Guid
        builder.Property(message => message.Id).IsRequired();

        // DateTime
        builder.Property(message => message.DateCreated);
        builder.Property(message => message.DateSent);
        builder.Property(message => message.DateUpdated);

        // String
        builder.Property(message => message.From);
        builder.Property(message => message.To);
        builder.Property(message => message.MessageStatus);
        builder.Property(message => message.SmsStatus);
        builder.Property(message => message.Body);
        builder.Property(message => message.AccountSid);
        builder.Property(message => message.ApiVersion);
        builder.Property(message => message.MessageId);
        builder.Property(message => message.ErrorMessage);
        builder.Property(message => message.SmsSid);
        builder.Property(message => message.Sid);
        builder.Property(message => message.Price);
        builder.Property(message => message.PriceUnit);
        builder.Property(message => message.ProfileName);
        builder.Property(message => message.MediaUrl);

        // Int
        builder.Property(message => message.NumMedia);
        builder.Property(message => message.NumSegments);
        builder.Property(message => message.ErrorCode);
    }
}
