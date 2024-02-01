using Domain.Common;
using Domain.Enums;

namespace Domain.Events
{
	public record CreateAccountEvent(AccountType accountType, string AccountName, int initMoney) : DomainEvent;
}

