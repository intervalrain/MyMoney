using Domain.Exceptions;

namespace Domain
{
    public class Cash : Account
	{
		public Cash(string accountName, int initMoney)
			: base(accountName, initMoney)
		{
		}

		public override int Sum()
		{
			return InitMoney + Items.Sum(x => x.Amount);
		}

        public override void AddItem(DateTime time, string what, int amount)
        {
			if (amount < 0)
			{
                int current = Sum();
				if (current + amount < 0)
				{
					throw new CashNotEnoughException();
				}
            }
            base.AddItem(time, what, amount);
        }
    }
}

