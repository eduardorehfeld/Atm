using static System.Console;

namespace Atm.Service.Services.AtmService
{
    public class Screen
    {
        public void DisplayMessage(string message)
        {
            Write(message);
        }

        public void DisplayMessageLine(string message)
        {
            WriteLine(message);
        }

        public void DisplayDollarAmount(decimal amount)
        {
            Write(amount.ToString("$#.00"));
        }
    }
}
