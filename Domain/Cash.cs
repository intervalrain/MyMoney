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

    }
}

