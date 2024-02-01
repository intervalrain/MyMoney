namespace Domain
{
	public class Bank : Account
	{
        public Bank(string accountName, int initMoney)
			: base(accountName, initMoney)
		{
		}

        public override int Sum()
		{
            return InitMoney + Items.Sum(x => x.Amount);
        }
    }
}

