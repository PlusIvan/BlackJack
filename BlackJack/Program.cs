using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlackJack.essentials;
using Newtonsoft.Json.Linq;

namespace BlackJack
{
    class Program
    {
      

        static void Main(string[] args)
        {
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
