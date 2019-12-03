using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BlackJack.functions
{
    class Engine
    {
        private dynamic d;

        public Engine(dynamic d)
        {
            this.d = d;
        }
        public void On()
        {
            render.Render display = new render.Render(d);
            string[] allow = {"5","10","25","50","100"};
            JObject list = new JObject
        {
            { "K",  10 },
            { "Q",  10 },
            { "J",  10 },
            { "10",  10},
            { "9",  9},
            { "8",  8},
            { "7",  7},
            { "6",  6},
            { "5",  5},
            { "4",  4},
            { "3",  3},
            { "2",  2},
            { "A",  11}
        };
            while (d.balance > 0)
            {
                    Console.WriteLine($"\nAdd to pot? [5, 10, 25, 50, 100] & when done, type done");
                    string a = Console.ReadLine();
                    if (!allow.Contains(a) && a != "done")
                    {
                        continue;
                    }
                    else if (allow.Contains(a))
                    {
                        int b = Convert.ToInt32(a);
                    if (b > Convert.ToInt32(d.balance))
                    {
                        Console.WriteLine($"\nNot enough cash to waste");
                        continue;
                    }
                        d.balance -= b;
                        d.pot += b;
                    display.Display();
                    continue;
                    }
                    else if (!allow.Contains(a) && a == "done")
                    {
                    if (d.pot == 0)
                        continue;

                        display.Display();
                        break;
                    }
            }
            Console.WriteLine($"\nGame begin?");
        }
    }
}
