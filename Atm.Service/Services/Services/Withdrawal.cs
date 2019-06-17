using Atm.Repository.Repositories;
using Atm.Service.Services.AtmService;
using static System.Console;
using static System.Threading.Thread;

namespace Atm.Service.Services.Services
{
    public class Withdrawal : Transaction
    {
        private decimal _amount;
        private Keypad _keypad;
        private CashDispenser _cashDispenser;
        private const int CANCELED = 6;

        public Withdrawal(BankDatabase atmBankDatabase, Keypad atmKeypad, CashDispenser atmCashDispenser)
            : base(atmBankDatabase)
        {
            _keypad = atmKeypad;
            _cashDispenser = atmCashDispenser;
        }

        public override void Execute(int accountNumber, Screen atmScreen)
        {
            bool isCashDispensed = false;
            decimal availableBalance;

            BankDatabase bankDatabase = BankDatabase;

            do
            {
                _amount = DisplayMenuOfAmounts(atmScreen);

                if (_amount != CANCELED)
                {
                    availableBalance = bankDatabase.GetAvailableBalance(accountNumber);
                    if (_amount <= availableBalance)
                    {
                        if (_cashDispenser.IsSufficiantCashAvailable(_amount))
                        {
                            bankDatabase.Debit(accountNumber, _amount);
                            _cashDispenser.DispenseCash(_amount);
                            isCashDispensed = true;

                            atmScreen.DisplayMessageLine("\nYour cash has been dispensed. Please take your cash now.");
                        }
                        else
                            atmScreen.DisplayMessageLine("\nInsufficient cash available in the ATM.\n\nPlease choose a smaller amount.");
                    }
                    else
                        atmScreen.DisplayMessage("\nInsufficient funds in your account.\n\nPlease choose a smaller amount.");
                    Sleep(3000);
                }
                else
                {
                    atmScreen.DisplayMessageLine("\nCancelling transaction...");
                    Sleep(3000);
                    return;
                }
            } while (!isCashDispensed);
        }

        private int DisplayMenuOfAmounts(Screen atmScreen)
        {
            int userChoice = 0;

            int[] amounts = { 0, 20, 40, 60, 100, 200 };

            while (userChoice == 0)
            {
                Clear();
                atmScreen.DisplayMessageLine("\nWITHDRAWAL MENU: ");
                atmScreen.DisplayMessageLine("1 - $20");
                atmScreen.DisplayMessageLine("2 - $40");
                atmScreen.DisplayMessageLine("3 - $60");
                atmScreen.DisplayMessageLine("4 - $100");
                atmScreen.DisplayMessageLine("5 - $200");
                atmScreen.DisplayMessageLine("6 - Cancel transaction");
                atmScreen.DisplayMessage("\nChoose a withdrawal amount: ");

                int input = _keypad.GetInput();

                switch (input)
                {
                    case 1: // if the user chose a withdrawal amount 
                    case 2: // (i.e., chose option 1, 2, 3, 4 or 5), return the
                    case 3: // corresponding amount from amounts array
                    case 4:
                    case 5:
                        userChoice = amounts[input]; // save user's choice
                        break;
                    case CANCELED: // the user chose to cancel
                        userChoice = CANCELED; // save user's choice
                        break;
                    default: // the user did not enter a value from 1-6
                        atmScreen.DisplayMessageLine("\nInvalid selection. Try again.");
                        Sleep(2000);
                        break;
                }
            }

            return userChoice;
        }

    }
}
