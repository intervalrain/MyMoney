using Domain.Common;

namespace Domain.Events
{
	public record IncomeEvent(DateTime time, string AccountName, string what, int amount) : DomainEvent;
}

