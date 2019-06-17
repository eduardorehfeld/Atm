using Atm.Repository.Repositories;
using Atm.Service.Services.AtmService;
using static System.Threading.Thread;

namespace Atm.Service.Services.Services
{
    public class Deposit : Transaction
    {
        private decimal _amount;
        private Keypad _keypad;
        private DepositSlot _depositSlot;
        private const int CANCELED = 0;

        public Deposit(BankDatabase atmBankDatabase, Keypad atmKeypad, DepositSlot atmDepositSlot)
            : base(atmBankDatabase)
        {
            _keypad = atmKeypad;
            _depositSlot = atmDepositSlot;
        }

        public override void Execute(int accountNumber, Screen atmScreen)
        {
            _amount = PromptForDepositAmount(atmScreen);

            if (_amount != CANCELED) 
            {
                atmScreen.DisplayMessage("Please insert a deposit envelope containing ");
                atmScreen.DisplayDollarAmount(_amount);
                atmScreen.DisplayMessageLine(" in the deposit slot.");

                bool envelopeReceived = _depositSlot.IsEnvelopeReceived;

                if (envelopeReceived)
                {
                    atmScreen.DisplayMessageLine(
                        "Your envelope has been received.\n" +
                        "The money just deposited will not be available " +
                        "until we \nverify the amount of any " +
                        "enclosed cash, and any enclosed checks clear.");

                    BankDatabase.Credit(accountNumber, _amount);
                }
                else
                    atmScreen.DisplayMessageLine("You did not insert an envelope, so the ATM has canceled your transaction.");
            }
            else
                atmScreen.DisplayMessageLine("Canceling transaction.");

            Sleep(3000);
        }

        private decimal PromptForDepositAmount(Screen atmScreen)
        {
            atmScreen.DisplayMessageLine("Please input a deposit amount in CENTS (or 0 to cancel): ");
            int input = _keypad.GetInput();
            return input == CANCELED ? CANCELED : input / 100M;
        }
    }
}

