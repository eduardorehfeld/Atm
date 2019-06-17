using static System.Console;

namespace Atm.Service.Services.AtmService
{
    public class Keypad
    {
        public int GetInput()
        {
            int.TryParse(ReadLine(), out int output);
            return output;
        }
    }
}
