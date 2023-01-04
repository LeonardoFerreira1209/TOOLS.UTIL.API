using Azure.Messaging.ServiceBus;

namespace APPLICATION.DOMAIN.ENTITY.MESSAGE;

public class MessageEntity<T>
{
    public MessageEntity(T _mappedMessage, ServiceBusReceivedMessage _originalMessage)
    {
        MappedMessage = _mappedMessage; OriginalMessage = _originalMessage;
    }

    public MessageEntity() { }

    public T MappedMessage { get; set; }
    public ServiceBusReceivedMessage OriginalMessage { get; set; }
}
