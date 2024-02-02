using System.Security.Principal;
using Domain.Common;
using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;

namespace Domain
{
	public class User : AbstractAggregateRoot
    {
		public string UserName { get; set; }
		public Dictionary<string, IAccount> Accounts { get; set; } = new();

		public User(string userName)
		{
			UserName = userName;
		}

		public Account CreateAccount(AccountType type, string accountName, int money)
		{
			if (Accounts.ContainsKey(accountName)) throw new DuplicatedAccountNameException();

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
			Accounts.Add(accountName, account);
			AddDomainEvent(new CreateAccountEvent(type, accountName, money));
			return account;
		}

		public int GetTotalAssets()
		{
			return Accounts.Values.Sum(x => x.Sum());
		}

		public int GetAccountBalance(string accountName)
		{
			if (!Accounts.ContainsKey(accountName)) throw new AccountNotExistException();
			return Accounts[accountName].Sum();
        }

		public void Pay(DateTime time, IAccount account, string what, int amount)
		{
			account.AddItem(time, what, -amount);
			AddDomainEvent(new PaymentEvent(time, account.AccountName, what, amount));
		}

        public void Income(DateTime time, IAccount account, string what, int amount)
        {
            account.AddItem(time, what, amount);
			AddDomainEvent(new IncomeEvent(time, account.AccountName, what, amount));
        }

        public void Transfer(DateTime time, IAccount from, IAccount to, int amount)
        {
			string what = $"{from.AccountName} transfers to {to.AccountName}";
            from.AddItem(time, what, -amount);
            to.AddItem(time, what, amount);
			AddDomainEvent(new TransferEvent(time, from.AccountName, to.AccountName, amount));
        }
    }
}

