using System;
using Newtonsoft.Json.Linq;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic d = JObject.Parse("{rules:{blackjack:2,dealer_hit:16, surrender:50},dealer:0, pot:0,player:{username:null, balance: 0}}");
            App app = new App();
            app.Config(true);
            Render display = new Render(d);
            Engine engine = new Engine(d);
            display.Show_Logo(d);
            display.Display();
            Console.WriteLine($"Type your Username");
            while (d.player.username == null)
            {
                d.player.username = Console.ReadLine().ToUpper();
                Console.Title = Console.Title + $" \\/ Welcome {d.player.username}";
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            }
            display.Show_Logo(d);
            display.Display();
            while (d.player.balance == 0)
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine($"\nHow much would you start with, [100-1000]");
                int amount = 0;
                try
                {
                    amount = Convert.ToInt32(Console.ReadLine());
                    if (amount < 100 || amount > 1000)
                    {
                        display.Show_Logo(d);
                        display.Display();
                        continue;
                    }

                }
                catch
                {
                    display.Show_Logo(d);
                    display.Display();
                    continue;
                }
                d.player.balance = amount;
            }
            engine.engineOn();
            Console.ReadKey();
        }
    }
}
