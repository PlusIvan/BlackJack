using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlackJack.essentials
{
    class Profile
    {
        public int user_balance = 0;


        public void Set_Balance(int balance)
        {
            this.user_balance = balance;
        }

        public int Get_Balance()
        {
            return this.user_balance;
        }

    }
}
