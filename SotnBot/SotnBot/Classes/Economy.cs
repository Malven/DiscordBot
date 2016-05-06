using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot.Classes
{
    public class Economy
    {
        public User[] users { get; set; }
        public Bank bank { get; set; }

        public Economy()
        {
            users = new User[0];
            bank = new Bank();
            bank.bankName = "S.O.T.N-BANK";
            bank.cashAmount = 100000;
        }
    }

    public class Bank
    {
        public int cashAmount { get; set; }
        public string bankName { get; set; }
    }

    public class User
    {
        public ulong userID { get; set; }
        public string name { get; set; }
        public int cashAmount { get; set; }
        public DateTime lastPayday { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }
        public int Level { get; set; }
    }
}
