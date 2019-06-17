using Atm.Repository.Repositories;
using Atm.Service.Services.AtmService;
using static System.Threading.Thread;

namespace Atm.Service.Services.Services
{
    public class BalanceInquiry : Transaction
    {
        public BalanceInquiry(BankDatabase atmBankDatabase)
            : base(atmBankDatabase) { }

        public override void Execute(int accountNumber, Screen screen)
        {
            BankDatabase bankDatabase = BankDatabase;

            decimal availableBalance = bankDatabase.GetAvailableBalance(accountNumber);
            decimal totalBalance = bankDatabase.GetTotalBalance(accountNumber);

            screen.DisplayMessageLine("\nBalance Information:");
            screen.DisplayMessage(" - Available balance: ");
            screen.DisplayDollarAmount(availableBalance);
            screen.DisplayMessage("\n - Total balance:     ");
            screen.DisplayDollarAmount(totalBalance);
            screen.DisplayMessageLine(string.Empty);

            Sleep(5000);
        }
    }
}