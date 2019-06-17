namespace Atm.Domain.Domains.Entities
{
    public class Account
    {
        /// <summary>
        /// Account's Number
        /// </summary>
        public int AccountNumber { get; private set; }
        /// <summary>
        /// Account's AvailableBalance
        /// </summary>
        public decimal AvailableBalance { get; private set; }
        /// <summary>
        /// Account's TotalBalance
        /// </summary>
        public decimal TotalBalance { get; private set; }
        /// <summary>
        /// Account's Pin
        /// </summary>
        private int Pin { get; set; }

        public Account(int accountNumber, int pin, decimal totalBalance, decimal availableBalance)
        {
            AccountNumber = accountNumber;
            Pin = pin;
            TotalBalance = totalBalance;
            AvailableBalance = availableBalance;
        }

        public void Credit(decimal amount)
        {
            TotalBalance += amount;
        }

        public void Debit(decimal amount)
        {
            AvailableBalance -= amount;
            TotalBalance -= amount;
        }

        public bool ValidatePin(int userPin)
        {
           return userPin == Pin;
        }
    }
}