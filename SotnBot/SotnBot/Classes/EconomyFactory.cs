using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot.Classes
{
    public class EconomyFactory
    {
        private static Economy _economy = new Economy();        
        
        private const string path = "./saves/save.json";
        private const int paydayAmount = 1000;

        public static Economy Instance
        {
            get { return _economy; }
        }

        public static void Save()
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var writer = new StreamWriter(stream))
                writer.Write(JsonConvert.SerializeObject(_economy, Formatting.Indented));
        }

        public static void Load()
        {
            _economy = JsonConvert.DeserializeObject<Economy>(File.ReadAllText(path));
        }

        public static string AddUser(Discord.User _user)
        {
            try
            {
                User user = new User();

                user.userID = _user.Id;
                user.name = _user.Name;
                user.cashAmount = 0;
                user.lastPayday = DateTime.Now;

                List<User> tempUsers = _economy.users.ToList();
                tempUsers.Add(user);
                _economy.users = tempUsers.ToArray();
                Save();

                return "User: " + user.name + " was added to the " + _economy.bank.bankName;
            }
            catch (Exception ex)
            {
                return "Something went wrong with the registration, please try again later: " + ex.Message;
            }
        }

        /// <summary>
        /// Payday a user
        /// </summary>
        /// <param name="_user">Discord.User object</param>
        /// <returns>a string of what happened</returns>
        public static string PayDay(Discord.User _user)
        {
            try
            {
                DateTime dateNow = DateTime.Now;

                for (int i = 0; i < _economy.users.Length; i++)
                {
                    if(_economy.users[i].userID == _user.Id)
                    {
                        if(!(_economy.users[i].lastPayday.AddHours(24) > dateNow))
                        {
                            _economy.users[i].cashAmount += paydayAmount;
                            _economy.users[i].lastPayday = dateNow;
                            Save();
                            return "Added $" + paydayAmount + " to your account. Your total cash is: $" + _economy.users[i].cashAmount + ".";
                        }
                        TimeSpan ts = _economy.users[i].lastPayday.AddHours(24) - dateNow;
                        return "Not time for payday yet, time left: " + ts.Hours + ":" + ts.Minutes + ":" +ts.Seconds + ".";
                    }
                }
                return "You are not registered to the " + _economy.bank.bankName + ".";
            }
            catch (Exception ex)
            {
                return "Something went wrong: " + ex.Message;
            }
        }

        public static string CheckTotal(Discord.User _user)
        {
            User user = FindUser(_user);
            if (user != null)
                return "Your total is: $" + user.cashAmount + ".";
            else
                return "You are not registered to the " + _economy.bank.bankName + ".";
        }

        /// <summary>
        /// Finds a user
        /// </summary>
        /// <param name="_user">Discord.User object</param>
        /// <returns>User object or null if it couldnt find any user</returns>
        public static User FindUser(Discord.User _user)
        {
            for (int i = 0; i < _economy.users.Length; i++)
            {
                if (_economy.users[i].userID == _user.Id)
                    return _economy.users[i];
            }
            return null;
        }

    }
}
