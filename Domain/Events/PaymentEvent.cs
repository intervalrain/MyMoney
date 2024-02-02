using Domain.Common;

namespace Domain.Events
{
    public record PaymentEvent(DateTime time, string AccountName, string what, int amount) : DomainEvent;
}

