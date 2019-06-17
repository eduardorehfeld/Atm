using Atm.Domain.Domains.Enums;
using Atm.Repository.Repositories;
using Atm.Service.Services.AtmService;
using Atm.Service.Services.Services;

namespace Atm.Service.Services.Factories
{
    public class TransactionFactory
    {
        public BankDatabase BankDatabase { get; set; }

        public TransactionFactory()
        {
            BankDatabase = new BankDatabase();
        }

        public Transaction CreateTransaction(MenuOption type, 
            Keypad keypad, CashDispenser cashDispenser, DepositSlot depositSlot)
        {
            Transaction temp = null;

            switch (type)
            {
                case MenuOption.BalanceInquiry:
                    temp = new BalanceInquiry(BankDatabase);
                    break;
                case MenuOption.Withdrawal:
                    temp = new Withdrawal(BankDatabase, keypad, cashDispenser);
                    break;
                case MenuOption.Deposit:
                    temp = new Deposit(BankDatabase, keypad, depositSlot);
                    break;
            }

            return temp;
        }
    }
}
