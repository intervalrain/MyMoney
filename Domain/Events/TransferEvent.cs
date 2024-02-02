using Domain.Common;

namespace Domain.Events
{
    public record TransferEvent(DateTime time, string fromAccount, string toAccount, int amount) : DomainEvent;
}

