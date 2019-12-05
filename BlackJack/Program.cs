using System;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Console = Colorful.Console;
namespace BlackJack
{
    class Program
    {
        public static object Color { get; private set; }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        static void Main(string[] args)
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);
            Console.Title = "BlackJack by Ivan Moroz";
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            String text = @"
                             ___  __   ___  _______ __   _____  _______ __
                            / _ )/ /  / _ |/ ___/ //_/_ / / _ |/ ___/ //_/
                           / _  / /__/ __ / /__/ ,< / // / __ / /__/ ,<   
                          /____/____/_/ |_\___/_/|_|\___/_/ |_\___/_/|_|  
                                               _____  __
                                              / _ ) \/ /
                                             / _  |\  / 
                                            /____/ /_/  
                                         _____   _____   _  __
                                        /  _/ | / / _ | / |/ /
                                       _/ / | |/ / __ |/    / 
                                      /___/ |___/_/ |_/_/|_/  
=====================================================================================================
";


            Console.ReadKey();

            dynamic d = JObject.Parse("{balance:0, dealer:0, player:0, pot:0}");
            render.Render display = new render.Render(d);
            display.Display();
            Console.WriteLine($"Press any key to begin the game");
            Console.ReadKey();
            Console.WriteLine($"\nDeposit between $100-$1000");
            while (true)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                //todo: allow only numbers
                if(n < 100 || n > 1000)
                {
                    Console.WriteLine($"\nDeposit between $100-$1000");
                }
                else
                {
                    d.balance = n;
                    break;
                }
            }
            display.Display();
            functions.Engine engine = new functions.Engine(d);
            engine.On(); 

            Console.ReadKey();
        }
    }
}
