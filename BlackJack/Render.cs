using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Figgle;
namespace BlackJack
{
    class Render
    {

        private dynamic d;

        public Render(dynamic d)
        {
            this.d = d;
        }

        public Render()
        {
        }

        public void Show_Logo(dynamic d)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            String text = "";
            Console.SetCursorPosition(0, 0);
            text = $@"
              __                                               
        _..-''--'----_.                                        
      ,''.-''| .---/ _`-._                                     
    ,' \ \  ;| | ,/ / `-._`-.                           
  ,' ,',\ \( | |// /,-._  / /                                  
  ;.`. `,\ \`| |/ / |   )/ /                                   
 / /`_`.\_\ \| /_.-.'-''/ /              ____     __       ______  ____     __  __   _____  ______  ____     __  __      ____     __    __      ______   __  __  ______  __  __
/ /_|_:.`. \ |;'`..')  / /              /\  _`\  /\ \     /\  _  \/\  _`\  /\ \/\ \ /\___ \/\  _  \/\  _`\  /\ \/\ \    /\  _`\  /\ \  /\ \    /\__  _\ /\ \/\ \/\  _  \/\ \/\ \
`-._`-._`.`.;`.\  ,'  / /               \ \ \L\ \\ \ \    \ \ \L\ \ \ \/\_\\ \ \/'/'\/__/\ \ \ \L\ \ \ \/\_\\ \ \/'/'   \ \ \L\ \\ `\`\\/'/    \/_/\ \/ \ \ \ \ \ \ \L\ \ \ `\\ \
    `-._`.`/    ,'-._/ /                 \ \  _ <'\ \ \  __\ \  __ \ \ \/_/_\ \ , <    _\ \ \ \  __ \ \ \/_/_\ \ , <     \ \  _ <'`\ `\ /'        \ \ \  \ \ \ \ \ \  __ \ \ , ` \
      : `-/     \`-.._/                   \ \ \L\ \\ \ \L\ \\ \ \/\ \ \ \L\ \\ \ \\`\ /\ \_\ \ \ \/\ \ \ \L\ \\ \ \\`\    \ \ \L\ \ `\ \ \         \_\ \__\ \ \_/ \ \ \/\ \ \ \`\ \
      |  :      ;._ (                      \ \____/ \ \____/ \ \_\ \_\ \____/ \ \_\ \_\ \____/\ \_\ \_\ \____/ \ \_\ \_\   \ \____/   \ \_\        /\_____\\ `\___/\ \_\ \_\ \_\ \_\
      :  |      \  ` \                      \/___/   \/___/   \/_/\/_/\/___/   \/_/\/_/\/___/  \/_/\/_/\/___/   \/_/\/_/    \/___/     \/_/        \/_____/ `\/__/  \/_/\/_/\/_/\/_/                         
       \         \   |                       Rules:                                                                                                                   
        :        :   ;                          - Dealer hit bellow < {d.rules.dealer_hit} & stand >= {d.rules.dealer_hit}               
        |           /                           - BlackJack pays x{d.rules.blackjack}                
        ;         ,'                            - Surrender gives %{d.rules.surrender} back              
       /         /                           SourceCode:     
      /         /                               - github.com/PlusIvan/blackjack
";
            Console.Write(text);
            for (var x = 0; x < Console.WindowWidth; x++)
            {
                Console.Write("=");
            }

            // Console.ResetColor();
        }

        public void Display()
        {
            String text = @"
";
            Console.WriteLine(text);
            if (d.player.username == null)
                Console.Write(FiggleFonts.Straight.Render($@"CASH: {d.player.balance} POT: {d.pot}"));
            else
                Console.Write(FiggleFonts.Straight.Render($@"{d.player.username}: CASH: {d.player.balance} POT: {d.pot}"));
            for (var x = 0; x < Console.WindowWidth; x++)
            {
                Console.Write("=");
            }
        }
        //dealer.First().Key, dealer.First().Value, player_cards, player_pts
        public void Table_Cards(String dealer_1st_card, int dealer_1st_value, String player_cards, int player_pts)
        {
            Console.Write($"(Dealer): {dealer_1st_card} XX, Value [{dealer_1st_value}]\n");
            Console.Write($"({d.player.username}): {player_cards}, Value [{player_pts}]\n");
            Console.Write($"" +
                $"> stand\n" +
                $"> hit\n" +
                $"> double\n" +
                $"> surrender\n" +
                $"\n");
            for (var x = 0; x < Console.WindowWidth; x++)
            {
                Console.Write("#");
            }
        }
    }
}
