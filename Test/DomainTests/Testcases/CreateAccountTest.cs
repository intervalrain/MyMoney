using Domain;
using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;

namespace Test.DomainTests.Testcases
{
	[TestClass]
	public class CreateAccountTest
	{
		[TestMethod]
		public void CreateAccount()
		{
			// Arrange
			var rain = new User("Rain Hu");
			var eva = new User("Eva Hsu");

            // Act
            rain.CreateAccount(AccountType.Cash, "Cash", 10000);
            rain.CreateAccount(AccountType.Bank, "YuanDa", 100000);
            rain.CreateAccount(AccountType.Bank, "ChungHsin", 200000);

            eva.CreateAccount(AccountType.Cash, "Cash", 2000);
            eva.CreateAccount(AccountType.Bank, "GuoTai", 200000);

			// Assert
			Assert.AreEqual(3, rain.Accounts.Count);
            Assert.AreEqual(310000, rain.GetTotalAssets());
            Assert.AreEqual(2, eva.Accounts.Count);
            Assert.AreEqual(202000, eva.GetTotalAssets());
			rain.DomainEvents.MoveNext(new CreateAccountEvent(AccountType.Cash, "Cash", 10000))
							 .MoveNext(new CreateAccountEvent(AccountType.Bank, "YuanDa", 100000))
							 .MoveNext(new CreateAccountEvent(AccountType.Bank, "ChungHsin", 200000))
							 .NoMore();
			eva.DomainEvents.MoveNext(new CreateAccountEvent(AccountType.Cash, "Cash", 2000))
							.MoveNext(new CreateAccountEvent(AccountType.Bank, "GuoTai", 200000))
							.NoMore();
        }

        [TestMethod]
        public void CreateDuplicatedAccount()
        {
            // Arrange
            var user = new User("Rain Hu");
            var accountName = "YuanDa";

            // Act
            user.CreateAccount(AccountType.Bank, accountName, 100000);
            Assert.ThrowsException<DuplicatedAccountNameException>(() => user.CreateAccount(AccountType.Bank, accountName, 200000));

            // Assert
            Assert.AreEqual(1, user.Accounts.Count);
            Assert.AreEqual(100000, user.GetAccountBalance(accountName));
            Assert.AreEqual(100000, user.GetTotalAssets());
            user.DomainEvents.MoveNext(new CreateAccountEvent(AccountType.Bank, "YuanDa", 100000))
                             .NoMore();
            
        }
    }
}

