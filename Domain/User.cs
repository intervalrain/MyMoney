using Domain.Common;
using Domain.Enums;
using Domain.Events;

namespace Domain
{
	public class User : AbstractAggregateRoot
    {
		public string UserName { get; set; }
		public List<IAccount> Accounts { get; set; } = new();

		public User(string userName)
		{
			UserName = userName;
		}

		public Account CreateAccount(AccountType type, string accountName, int money)
		{
			Account account = null;
			switch (type)
			{
				case AccountType.Cash:
					account = new Cash(accountName, money);
					break;
				case AccountType.Bank:
                    account = new Bank(accountName, money);
                    break;
				default:
					throw new NotImplementedException(nameof(type));
			}
			Accounts.Add(account);
			AddDomainEvent(new CreateAccountEvent(type, accountName, money));
			return account;
		}

		public int GetTotalAssets()
		{
			return Accounts.Sum(x => x.Sum());
		}
	}
}

