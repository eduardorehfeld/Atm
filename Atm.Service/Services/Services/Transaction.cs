using Atm.Repository.Repositories;
using Atm.Service.Services.AtmService;

namespace Atm.Service.Services.Services
{
    public abstract class Transaction
    {
        public BankDatabase BankDatabase { get; private set; }
        
        public Transaction(BankDatabase atmBankDatabase)
        {
            BankDatabase = atmBankDatabase;
        }

        public abstract void Execute(int accountNumber, Screen atmScreen);
    }
}
