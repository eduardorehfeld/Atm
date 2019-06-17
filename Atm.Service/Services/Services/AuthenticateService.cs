using Atm.Repository.Repositories;

namespace Atm.Service.Services.Services
{
    public class AuthenticateService
    {
        /// <summary>
        /// Current user's account number
        /// </summary>
        public int CurrentAccountNumber { get; private set; }

        /// <summary>
        /// Whether user is authenticated
        /// </summary>
        public bool UserAuthenticated { get; private set; }

        /// <summary>
        /// Account information database
        /// </summary>
        private BankDatabase BankDatabase;

        public AuthenticateService()
        {
            BankDatabase = new BankDatabase();
        }

        public bool AuthenticateUser(int accountNumber, int pinCode)
        {
            UserAuthenticated = BankDatabase.AuthenticateUser(accountNumber, pinCode);
            if (UserAuthenticated)
            {
                CurrentAccountNumber = accountNumber; // Provide access to account if authentication is correct.
                return true;
            }
            else
                return false;   
        }

        public void Logout()
        {
            UserAuthenticated = false;
        }

        public void CleanAccountNumber()
        {
            CurrentAccountNumber = 0;
        }
    }
}
