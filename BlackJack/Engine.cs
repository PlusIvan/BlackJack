using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BlackJack
{
    class Engine
    {
        private dynamic d;
        Random rand = new Random();
        public Engine(dynamic d)
        {
            this.d = d;
        }
        public void On()
        {
            Render display = new Render(d);
            string[] allow = { "5", "10", "25", "50", "100" };
            while (d.player.balance > 0)
            {
                display.Show_Logo(d);
                display.Display();
                // Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine($"\nAdd to pot? [5, 10, 25, 50, 100] & when done, type done");
                string number = Console.ReadLine();
                if (!allow.Contains(number) && number != "done")
                {
                    // Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    continue;
                }
                else if (number == "done")
                {
                    break;
                }
                if (Convert.ToInt32(number) > Convert.ToInt32(d.player.balance))
                {
                    //Console.WriteLine($"\nNot enough cash to waste");
                    continue;
                }
                else
                {
                    d.player.balance -= Convert.ToInt32(number);
                    d.pot += Convert.ToInt32(number);
                    continue;
                }


                /*  

                    if (b > Convert.ToInt32(d.balance))
                    {
                        Console.WriteLine($"\nNot enough cash to waste");
                        continue;
                    }
                    else
                    {
                        d.player.balance -= b;
                        d.pot += b;
                        display.Display();
                        continue;
                    }


                }else if (!allow.Contains(a) && a == "done")
                    {
                    if (d.pot == 0)
                        continue;

                        display.Display();
                        break;
                    }
                    }*/
            }
            display.Show_Logo(d);
            display.Display();
            // Console.WriteLine($"\nGame begin?");
            string[] type = { "A", "K", "Q", "J", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
            string[] symbols = { "s", "c", "d", "h" };
            var deck = new Dictionary<string, int>();
            while (true)
            {
                for (var ty = 0; ty < type.Length; ty++)
                {
                    for (var sym = 0; sym < symbols.Length; sym++)
                    {
                        if (type[ty] == "A")
                            deck.Add(type[ty] + symbols[sym], 11);
                        else if (type[ty] == "K" || type[ty] == "Q" || type[ty] == "J")
                            deck.Add(type[ty] + symbols[sym], 10);
                        else
                            deck.Add(type[ty] + symbols[sym], Convert.ToInt32(type[ty]));
                    }
                }
                break;
            }

            //Console.WriteLine($"Deck Length: {deck.Count}"); 52

            //Suffle
            List<string> keyList = new List<string>(deck.Keys);
            var shuffled_deck = new Dictionary<string, int>();
            while (deck.Count != 0)
            {
                string randomKey = keyList[rand.Next(keyList.Count)];
                //Console.WriteLine($"Sniff Key: {randomKey} Value: {deck[randomKey]}");
                shuffled_deck.Add(randomKey, deck[randomKey]);
                deck.Remove(randomKey);
                keyList.Remove(randomKey);
            }
            //Shuffle done

             foreach (KeyValuePair<string, int> entry in shuffled_deck)
             {
                 // do something with entry.Value or entry.Key
                 Console.WriteLine($"Key: {entry.Key} Value: {entry.Value}");

             }
            

        }
    }
}
