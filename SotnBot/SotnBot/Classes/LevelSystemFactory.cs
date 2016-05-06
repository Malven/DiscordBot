using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using System.Diagnostics;

namespace SotnBot.Classes {
    public class LevelSystemFactory {

        private static LevelSystem _levelSystem = new LevelSystem();

        private const string path = "./saves/level-system/save.json";

        public static LevelSystem Instance
        {
            get { return _levelSystem; }
        }

        public string ShowLevel(Discord.User _user ) {
            User temp = FindUser( _user );
            if(temp == null ) {
                AddUserToLevelSystem( _user );
                return "You are now a part of the S.O.T.N Level System, type !level info for more info.";
            } else {
                string message = "Your level: " + temp.Level + ".\n" +
                                 "Current exp: " + temp.Exp + ".\n" +
                                 "Exp to next level: " + ( temp.MaxExp - temp.Exp ) + ".";
            }
            return "Something went wrong";
        }

        public static void AddExpTouser(User _user, int amount ) {
            _user.Exp += amount;
            if(_user.Exp >= _user.MaxExp ) {
                LevelUp(_user);
            }
        }

        private static void LevelUp(User _user) {
            _user.Level += 1;
            _user.MaxExp = GetNewMaxExp( _user );
        }

        public static void AddUserToLevelSystem( Discord.User _user ) {
            try {
                User user = new User();
                user.userID = _user.Id;
                user.name = _user.Name;
                user.Level = 1;
                user.MaxExp = GetNewMaxExp( user );

                List<User> tempList = Instance.Users.ToList();
                tempList.Add( user );
                Instance.Users = tempList.ToArray();
                Save();
            }
            catch ( Exception ex ) {
                throw new Exception( "Gick inte att lägga till " + _user.Name + " till levelsystemet.\n\n" + ex.Message );
            }
        }

        private static int GetNewMaxExp( User _user ) {
            return Convert.ToInt32( ( _user.Level / LevelSystem.expConstant ) * ( _user.Level * LevelSystem.expConstant ) );
        }

        /// <summary>
        /// Finds a user
        /// </summary>
        /// <param name="_user">Discord.User object</param>
        /// <returns>User object or null if it couldnt find any user</returns>
        public static User FindUser( Discord.User _user ) {
            for ( int i = 0; i < Instance.Users.Length; i++ ) {
                if ( Instance.Users[ i ].userID == _user.Id )
                    return Instance.Users[ i ];
            }
            return null;
        }
        public static void Save() {
            using ( var stream = new FileStream( path, FileMode.Create, FileAccess.Write, FileShare.None ) )
            using ( var writer = new StreamWriter( stream ) )
                writer.Write( JsonConvert.SerializeObject( Instance, Formatting.Indented ) );
        }

        public static void Load() {
            _levelSystem = JsonConvert.DeserializeObject<LevelSystem>( File.ReadAllText( path ) );
        }
    }
}
