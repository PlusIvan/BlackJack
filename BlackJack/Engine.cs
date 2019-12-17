using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figgle;
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
        public void engineOn()
        {
            Render display = new Render(d);
            Random rand = new Random();
            string[] cards = { "A", "K", "Q", "J", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
            string[] suits = { "s", "c", "d", "h" };
            string[] option = { "surrender", "hit", "double", "stand" };
            //d.player.balance = 1000;
            //int bet = 0;
            while (true)
            {
                d.pot = 0;
                IDictionary<string, int> deck = new Dictionary<string, int>();
                // Console.Clear();
                display.Show_Logo(d);
                display.Display();
                if (d.player.balance == 0)
                {
                    Console.Write($"...You lost everything...as always...");
                    Console.ReadKey();
                    break;
                }
                Console.Write($"Bet Amount?\n");
                try
                {
                    d.pot = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.Write($"[X] Int number please\n");
                    Console.ReadKey();
                    continue;
                }

                if (d.pot > d.player.balance || d.pot <= 0)
                {
                    Console.Write($"[X] Can't bet that amount\n");
                    Console.ReadKey();
                    continue;
                }
                d.player.balance -= d.pot;
                //Form deck
                foreach (string card in cards)
                {
                    foreach (string suit in suits)
                    {
                        int value = 0;
                        if (card == "A")
                        {
                            value = 11;
                        }
                        else if (card == "K" || card == "Q" || card == "J")
                        {
                            value = 10;
                        }
                        else
                        {
                            value = Convert.ToInt32(card);
                        }
                        deck.Add(card + suit, value); //Deck filled
                    }
                }
                //End of Form Deck
                //Shuffle Deck
                List<string> keyList = new List<string>(deck.Keys);
                IDictionary<string, int> deck_aux = new Dictionary<string, int>();
                while (deck.Count > 0)
                {
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    deck_aux.Add(randomKey, deck[randomKey]);
                    deck.Remove(randomKey);
                    keyList.Remove(randomKey);
                }
                deck = deck_aux;
                keyList = new List<string>(deck.Keys);
                var player = new Dictionary<string, int>();
                var dealer = new Dictionary<string, int>();
                while (player.Count != 2)
                {
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    player.Add(randomKey, deck[randomKey]);
                    deck.Remove(randomKey);
                    keyList.Remove(randomKey);
                }
                while (dealer.Count != 2)
                {
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    dealer.Add(randomKey, deck[randomKey]);
                    deck.Remove(randomKey);
                    keyList.Remove(randomKey);
                }
                while (true)
                {
                    //Console.Clear();
                    display.Show_Logo(d);
                    display.Display();
                    int dealer_pts = 0;
                    int player_pts = 0;
                    string dealer_cards = "";
                    string player_cards = "";
                    foreach (KeyValuePair<string, int> entry in player)
                    {
                        player_pts += entry.Value;
                        player_cards += " " + entry.Key;
                    }
                    foreach (KeyValuePair<string, int> entry in dealer)
                    {
                        dealer_pts += entry.Value;
                        dealer_cards += " " + entry.Key;
                    }
                    display.Table_Cards(dealer.First().Key, dealer.First().Value, player_cards, player_pts);
                    String op = "";
                    if (player_pts == 21)
                    {
                        Console.Write($"[+] You hit a blackjack\n");
                        op = "stand";
                    }
                    else
                    {
                        op = Console.ReadLine();
                    }
                    if (!option.Contains(op))
                    {
                        continue;
                    }
                    if (op == "double" && d.pot * 2 > d.player.balance)
                    {
                        Console.Write($"[X] Can not double, insuficient d.player.balance\n");
                        Console.ReadKey();
                        continue;
                    }
                    if (op == "surrender")
                    {
                        d.player.balance += d.pot / 2;
                        Console.Write($"[!] Dealer shows {dealer_cards}\n[+] Won {d.pot / 2}\n");
                        Console.ReadKey();
                        break;
                    }
                    if (op == "hit")
                    {
                        string randomKey = keyList[rand.Next(keyList.Count)];
                        player.Add(randomKey, deck[randomKey]);
                        player_cards += " " + randomKey;
                        player_pts += deck[randomKey];
                        deck.Remove(randomKey);
                        keyList.Remove(randomKey);
                        Console.Write($"[+] You hit with a {randomKey} [{player_pts}]\n");
                        if (player_pts > 21)
                        {
                            Console.Write($"[X] Busted\n");
                            Console.Write($"[-] Lost {d.pot}\n");
                            Console.ReadKey();
                            break;
                        }
                    }
                    if (op == "stand" || op == "double")
                    {
                        //Console.Clear();

                        if (op == "double")
                        {
                            d.pot *= 2;
                            d.player.balance -= d.pot * 2;
                            string randomKey = keyList[rand.Next(keyList.Count)];
                            player.Add(randomKey, deck[randomKey]);
                            player_cards += " " + randomKey;
                            player_pts += deck[randomKey];
                            deck.Remove(randomKey);
                            keyList.Remove(randomKey);
                            Console.Write($"[+] You double down {d.pot * 2} with a {randomKey} [{player_pts}]\n");
                            if (player_pts > 21)
                            {
                                Console.Write($"[X] Busted\n");
                                Console.Write($"[-] Lost {d.pot}\n");
                                Console.ReadKey();
                                break;
                            }
                        }


                        Console.Write($"[!] Dealer shows {dealer_cards} [{dealer_pts}]\n");
                        Console.Write($"Player: {player_cards}  [{player_pts}]\n");
                        if (dealer_pts >= 16 && dealer_pts <= 21) // Dealer stands
                        {
                            if (dealer_pts == player_pts)
                            {
                                d.player.balance += d.pot;
                                Console.Write($"[!] Tie\n");
                                Console.Write($"[+] Won {d.pot}\n");
                                Console.ReadKey();
                                break;
                            }
                            else if (dealer_pts > player_pts)
                            {
                                Console.Write($"[-] Lost {d.pot}\n");
                                Console.ReadKey();
                                break;
                            }
                            else if (dealer_pts < player_pts)
                            {
                                d.player.balance += d.pot * 2;
                                Console.Write($"[+] Won {d.pot * 2}\n");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else if (dealer_pts < 16 || dealer_pts > 21)
                        {
                            while (dealer_pts < 16 || dealer_pts > 21 && dealer.ContainsValue(11))
                            {
                                string randomKey = keyList[rand.Next(keyList.Count)];
                                dealer.Add(randomKey, deck[randomKey]);
                                dealer_cards += " " + randomKey;
                                dealer_pts += deck[randomKey];
                                deck.Remove(randomKey);
                                keyList.Remove(randomKey);
                                Console.Write($"[!] Dealer hits a card {randomKey} [{dealer_pts}]\n");
                                if (dealer_pts > 21)
                                {
                                    //Busting 21, check for Aces with 11
                                    if (dealer.ContainsValue(11))
                                    {
                                        foreach (KeyValuePair<string, int> entry in dealer)
                                        {
                                            if (entry.Value == 11)
                                            {
                                                dealer[entry.Key] = 1; // Ace 11 count as 1
                                                dealer_pts -= 10; //11-1 = 10
                                                break;
                                            }
                                        }
                                        continue;
                                    }
                                }
                            }
                            if (dealer_pts > 21)
                            {
                                Console.Write($"[!] Dealer shows {dealer_cards} [{dealer_pts}]\n");
                                Console.Write($"Player: {player_cards}  [{player_pts}]\n");
                                d.player.balance += d.pot * 2;
                                Console.Write($"[+] Won {d.pot * 2}\n");
                                Console.ReadKey();
                                break;
                            }
                            if (dealer_pts == player_pts)
                            {
                                Console.Write($"[!] Dealer shows {dealer_cards} [{dealer_pts}]\n");
                                Console.Write($"Player: {player_cards}  [{player_pts}]\n");
                                d.player.balance += d.pot;
                                Console.Write($"[!] Tie\n");
                                Console.Write($"[+] Won {d.pot}\n");
                                Console.ReadKey();
                                break;
                            }
                            else if (dealer_pts > player_pts)
                            {
                                Console.Write($"[!] Dealer shows {dealer_cards} [{dealer_pts}]\n");
                                Console.Write($"Player: {player_cards}  [{player_pts}]\n");
                                Console.Write($"[-] Lost {d.pot}\n");
                                Console.ReadKey();
                                break;
                            }
                            else if (dealer_pts < player_pts)
                            {
                                Console.Write($"[!] Dealer shows {dealer_cards} [{dealer_pts}]\n");
                                Console.Write($"Player: {player_cards}  [{player_pts}]\n");
                                d.player.balance += d.pot * 2;
                                Console.Write($"[+] Won {d.pot * 2}\n");
                                Console.ReadKey();
                                break;
                            }
                        }
                    }
                    Console.ReadKey();
                }


            }
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

            /*  foreach (KeyValuePair<string, int> entry in shuffled_deck)
              {
                  // do something with entry.Value or entry.Key
                  Console.WriteLine($"Key: {entry.Key} Value: {entry.Value}");

              }*/
            keyList = new List<string>(shuffled_deck.Keys);
            var player = new Dictionary<string, int>();
            var dealer = new Dictionary<string, int>();
            while (player.Count != 2)
            {
                string randomKey = keyList[rand.Next(keyList.Count)];
                player.Add(randomKey, shuffled_deck[randomKey]);
                shuffled_deck.Remove(randomKey);
                keyList.Remove(randomKey);
            }
            while (dealer.Count != 2)
            {
                string randomKey = keyList[rand.Next(keyList.Count)];
                dealer.Add(randomKey, shuffled_deck[randomKey]);
                shuffled_deck.Remove(randomKey);
                keyList.Remove(randomKey);
            }
            
            while (d.pot > 0)
            {
                Console.Write($"DEALER: {dealer.First().Key} XX\n");
                Console.Write($"YOU: {player.First().Key} {player.Last().Key}\n");
                //Special case for blackjack %
                if (player.First().Value + player.Last().Value == 21 && dealer.First().Value + dealer.Last().Value < 21)
                {
                    Console.Write($"Wow, you win a black jack\n");
                }
                else if (player.First().Value + player.Last().Value == 21 && dealer.First().Value + dealer.Last().Value == 21)
                {
                    Console.Write($"Wow, you win a black jack but so does the dealer :(\n");
                }
                // End of special case

                Console.Write($"Moves [Hit, Surrender, Stand, Double]\n");
                String[] moves = { "hit", "surrender", "stand", "double"};
                String move = Console.ReadLine().ToLower();
                if (!moves.Contains(move))
                {
                    display.Show_Logo(d);
                    display.Display();
                    continue;
                }
                if (move == "surrender")
                {
                    //dynamic d = JObject.Parse("{rules:{blackjack:xx,dealer_hit:xx, surrender:xx},dealer:0, pot:0,player:{username:null, balance: 0}}");
                    d.player.balance += d.rules.surrender*d.pot/100; // += %*pot/100
                    Console.Write($"You win {d.rules.surrender * d.pot / 100}\n");
                    Console.ReadKey();
                    break;
                }
                if (move == "stand")
                {
                    int dealer_pts = dealer.First().Value + dealer.Last().Value;
                    if (dealer.First().Value + dealer.Last().Value < d.rules.dealer_hit)
                    {
                        //Dealer bellow, must hit
                        while (dealer_pts < d.rules.dealer_hit)
                        {
                            string randomKey = keyList[rand.Next(keyList.Count)];
                            dealer.Add(randomKey, shuffled_deck[randomKey]);
                            shuffled_deck.Remove(randomKey);
                            keyList.Remove(randomKey);
                            dealer_pts += shuffled_deck[randomKey];
                            if (dealer_pts > 21 && dealer.Values.Contains(11)) //Dealer hit over 21 yet to convert ACE to 1
                            {
                                while (dealer.Values.Contains(11))
                                {

                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
