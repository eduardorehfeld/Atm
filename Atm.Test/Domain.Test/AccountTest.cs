using Atm.Domain.Domains.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atm.Test
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestCreditNegativeValueInput()
        {
            decimal totalBalance = 50;
            decimal input = -30;
            Account account = new Account(123, 123, totalBalance, 0);

            account.Credit(input);

            Assert.AreEqual(account.TotalBalance, totalBalance + input);
        }

        [TestMethod]
        public void TestCreditPositiveValueInput()
        {
            decimal totalBalance = 50;
            decimal input = 30;
            Account account = new Account(123, 123, totalBalance, 0);

            account.Credit(input);

            Assert.AreEqual(account.TotalBalance, totalBalance + input);
        }

        [TestMethod]
        public void TestDebitPositiveValueInput()
        {
            decimal totalBalance = 50;
            decimal avalaibleBalance = 50;
            decimal input = 30;
            Account account = new Account(123, 123, totalBalance, avalaibleBalance);

            account.Debit(input);

            Assert.AreEqual(account.TotalBalance, totalBalance - input);
            Assert.AreEqual(account.AvailableBalance, avalaibleBalance - input);
        }

        [TestMethod]
        public void TestDebitNegativeValueInput()
        {
            decimal totalBalance = 50;
            decimal avalaibleBalance = 50;
            decimal input = -30;
            Account account = new Account(123, 123, totalBalance, avalaibleBalance);

            account.Debit(input);

            Assert.AreEqual(account.TotalBalance, totalBalance - input);
            Assert.AreEqual(account.AvailableBalance, avalaibleBalance - input);
        }

        [TestMethod]
        public void TestAccountValidPin()
        {
            int pin = 123;
            int input = 123;
            Account account = new Account(456, pin, 0, 0);

            Assert.IsTrue(account.ValidatePin(input));
        }

        [TestMethod]
        public void TestAccountInvalidPin()
        {
            int pin = 123;
            int input = 999;
            Account account = new Account(456, pin, 0, 0);

            Assert.IsFalse(account.ValidatePin(input));
        }
    }
}
