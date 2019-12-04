using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Figgle;
namespace BlackJack.render
{
    class Render
    {
        private dynamic d;

        public Render(dynamic d)
        {
            this.d = d;
        }

        public void Display()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
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
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(FiggleFonts.Straight.Render(@"CASH: "+d.balance+@" "+@" POT: "+d.pot)+ @"
=====================================================================================================
");
            //tesr



        }
    }
}
