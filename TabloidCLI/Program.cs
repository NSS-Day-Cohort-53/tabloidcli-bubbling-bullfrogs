using TabloidCLI.UserInterfaceManagers;
using System;
using TabloidCLI.Models;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //public ConsoleBackground consoleColor = "Black";

            //Console.BackgroundColor = ConsoleColor.consoleColor;
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
