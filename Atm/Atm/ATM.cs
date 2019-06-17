using Atm.Domain.Domains.Enums;
using Atm.Service.Services.AtmService;
using Atm.Service.Services.Factories;
using Atm.Service.Services.Services;
using static System.Console;
using static System.Threading.Thread;

namespace Atm.ATM
{
    public class ATM
    {
        /// <summary>
        /// ATM's AuthenticateService
        /// </summary>
        private AuthenticateService _authenticateService { get; set; }
        /// <summary>
        /// ATM's TransactionFactory
        /// </summary>
        private TransactionFactory _transactionFactory { get; set; }
        /// <summary>
        /// ATM's cash dispenser
        /// </summary>
        private CashDispenser _cashDispenser;
        /// <summary>
        /// ATM's deposit slot
        /// </summary>
        private DepositSlot _depositSlot;
        /// <summary>
        /// ATM's keypad
        /// </summary>
        private Keypad _keypad;
        /// <summary>
        /// ATM's screen
        /// </summary>
        private Screen _screen;

        /// <summary>
        /// no-argument ATM constructor initializes instance variables
        /// </summary>
        public ATM()
        {
            _authenticateService = new AuthenticateService();
            _transactionFactory = new TransactionFactory();
            _cashDispenser = new CashDispenser();
            _depositSlot = new DepositSlot();
            _keypad = new Keypad();
            _screen = new Screen();
        }

        private void DisplayAuthentication()
        {
            Clear();
            _screen.DisplayMessageLine("Please enter your account number: ");
            int accountNumber = _keypad.GetInput();
            _screen.DisplayMessageLine("Enter your PIN: ");
            int pinCode = _keypad.GetInput();

            var authenticated = _authenticateService.AuthenticateUser(accountNumber, pinCode);
            if (!authenticated)
                _screen.DisplayMessageLine("Invalid account number or PIN. Please try again. "); // Try again if the authentication is incorrect.

            Sleep(3000);
        }

        private void PerformTransactions()
        {
            // local variable to store transaction currently being processed
            Transaction currentTransaction;

            bool isUserExited = false; // user has not chosen to exit

            // loop while user has not chosen option to exit system
            while (!isUserExited)
            {
                Clear();

                MenuOption menuSelect = DisplayMainMenu();

                switch (menuSelect)
                {
                    case MenuOption.BalanceInquiry:
                    case MenuOption.Withdrawal:
                    case MenuOption.Deposit:
                        currentTransaction = _transactionFactory.CreateTransaction(menuSelect, _keypad, _cashDispenser, _depositSlot);
                        currentTransaction.Execute(_authenticateService.CurrentAccountNumber, _screen);
                        break;
                    case MenuOption.Exit:
                        _screen.DisplayMessageLine("Exiting the system...");
                        isUserExited = true;
                        Sleep(3000);
                        Clear();
                        break;
                    // Try again if you enter a value other than the enum values, regardless of the GetInput method.
                    default:
                        _screen.DisplayMessageLine("You did not enter a valid selection. Try again.");
                        break;
                }
            }
        }

        private MenuOption DisplayMainMenu()
        {
            _screen.DisplayMessage("MAIN MENU: " +
                                    "\n\n1 - View my balance" +
                                    "\n2 - Withdraw cash" +
                                    "\n3 - Deposit funds" +
                                    "\n4 - Exit" +
                                    "\nPlease enter a choise: ");

            MenuOption menuSelect = (MenuOption)_keypad.GetInput();
            return menuSelect;
        }

        public void Run()
        {
            // welcome and authenticate user; perform transactions
            while (true)
            {
                while (!_authenticateService.UserAuthenticated) // loop while user is not yet authenticated
                {
                    _screen.DisplayMessageLine("Welcome!");
                    DisplayAuthentication();
                }

                PerformTransactions(); // user is now authenticated
                _authenticateService.Logout();
                _authenticateService.CleanAccountNumber();
                _screen.DisplayMessageLine("Thank you! Goodbye!");
                Sleep(2000);
            }
        }
    }
}
