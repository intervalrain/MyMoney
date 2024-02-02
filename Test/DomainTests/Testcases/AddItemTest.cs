using Domain;
using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;

namespace Test.DomainTests.Testcases
{
	[TestClass]
	public class AddItemTest
	{
		[TestMethod]
		public void Income_Test()
		{
			// Arrange
			var user = new User("Rain Hu");
            var accountName = "Bank";
            var itemName = "salary";
            var initMoney = 10000;
            var amount = 75000;
            var time = DateTime.Now;
            var bank = user.CreateAccount(AccountType.Bank, accountName, initMoney);

            // Act
            user.Income(time, bank, itemName, amount);

            // Assert
            Assert.AreEqual(1, user.Accounts.Count);
            Assert.AreEqual(85000, user.GetAccountBalance(accountName));
            Assert.AreEqual(85000, user.GetTotalAssets());
            user.DomainEvents.MoveNext(new CreateAccountEvent(AccountType.Bank, accountName, initMoney))
                             .MoveNext(new IncomeEvent(time, accountName, itemName, amount))
                             .NoMore();
        }

		[TestMethod]
		public void Pay_Test()
		{
            // Arrange
            var user = new User("Rain Hu");
            var accountName = "Cash";
            var itemName = "breakfast";
            var initMoney = 1000;
            var amount = 32;
            var time = DateTime.Now;
            var cash = user.CreateAccount(AccountType.Cash, accountName, initMoney);

            // Act
            user.Pay(time, cash, itemName, amount);

            // Assert
            Assert.AreEqual(1, user.Accounts.Count);
            Assert.AreEqual(968, user.GetAccountBalance(accountName));
            Assert.AreEqual(968, user.GetTotalAssets());
            user.DomainEvents.MoveNext(new CreateAccountEvent(AccountType.Cash, accountName, initMoney))
                             .MoveNext(new PaymentEvent(time, accountName, itemName, amount))
                             .NoMore();
        }

        [TestMethod]
        public void PayNotEnough_Test()
        {
            // Arrange
            var user = new User("Rain Hu");
            var accountName = "Cash";
            var itemName = "dinner";
            var initMoney = 1000;
            var amount = 2000;
            var time = DateTime.Now;
            var cash = user.CreateAccount(AccountType.Cash, accountName, initMoney);

            // Act
            Assert.ThrowsException<CashNotEnoughException>(() => user.Pay(time, cash, itemName, amount));

            // Assert
            Assert.AreEqual(1, user.Accounts.Count);
            Assert.AreEqual(1000, user.GetAccountBalance(accountName));
            Assert.AreEqual(1000, user.GetTotalAssets());
            user.DomainEvents.MoveNext(new CreateAccountEvent(AccountType.Cash, accountName, initMoney))
                             .NoMore();
        }

        [TestMethod]
		public void Transaction_Test()
		{
            // Arrange
            var user = new User("Rain Hu");
            var fromName = "Cash";
            var toName = "Bank";
            var cash = user.CreateAccount(AccountType.Cash, fromName, 1000);
            var bank = user.CreateAccount(AccountType.Bank, toName, 0);

            // Act
            DateTime time = DateTime.Now;
            user.Transfer(time, cash, bank, 500);

            Assert.AreEqual(2, user.Accounts.Count);
            Assert.AreEqual(500, user.GetAccountBalance(fromName));
            Assert.AreEqual(500, user.GetAccountBalance(toName));
            Assert.AreEqual(1000, user.GetTotalAssets());
            user.DomainEvents.MoveNext(new CreateAccountEvent(AccountType.Cash, fromName, 1000))
                             .MoveNext(new CreateAccountEvent(AccountType.Bank, toName, 0))
                             .MoveNext(new TransferEvent(time, fromName, toName, 500))
                             .NoMore();
        }
	}
}

