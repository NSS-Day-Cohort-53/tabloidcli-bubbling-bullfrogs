using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI;

namespace TabloidCLI.UserInterfaceManagers
{
    public class ConsoleManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;

        public ConsoleManager (IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }
       
        
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Change console color");
            Console.WriteLine(" 1) Blue");
            Console.WriteLine(" 2) Cyan");
            Console.WriteLine(" 3) Dark Blue");
            Console.WriteLine(" 4) Dark Cyan");
            Console.WriteLine(" 5) Dark Green");
            Console.WriteLine(" 6) Dark Red");
            Console.WriteLine(" 7) Black");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "7":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    return this;
                 
            }
        }
        

    }
}
