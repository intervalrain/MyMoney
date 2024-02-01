using System;
namespace Domain
{
	public abstract class Account : IAccount
	{
        private string _accountName;
        private int _initMoney;
        private List<Item> _items = new();

        public string AccountName => _accountName;
        public int InitMoney => _initMoney;
        public IReadOnlyList<Item> Items => _items;

        public Account(string accountName, int initMoney)
		{
            _accountName = accountName;
            _initMoney = initMoney;
		}

        public abstract int Sum();
	}
}

