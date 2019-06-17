using Atm.Service.Services.AtmService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atm.Test
{
    [TestClass]
    public class CashDispenserTest
    {
        [TestMethod]
        public void TestCashAvailable()
        {
            decimal input = 499 * 20; // Initial Count = 500 
            CashDispenser cashDispenser = new CashDispenser();

            Assert.IsTrue(cashDispenser.IsSufficiantCashAvailable(input));
        }

        [TestMethod]
        public void TestCashUnavailable()
        {
            decimal input = 501 * 20; // Initial Count = 500 
            CashDispenser cashDispenser = new CashDispenser();

            Assert.IsFalse(cashDispenser.IsSufficiantCashAvailable(input));
        }

        [TestMethod]
        public void TestDispense()
        {
            decimal input = 1;
            decimal initialCount = 500;
            CashDispenser cashDispenser = new CashDispenser();
            cashDispenser.DispenseCash(input * 20);

            Assert.AreEqual(cashDispenser.GetBillCount(), initialCount - input);
        }
    }
}
