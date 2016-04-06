using Discord;
using Discord.Commands;
using Discord.Modules;
using SotnBot.Modules.Diablo;
using SotnBot.Modules.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot
{
    class Program
    {
        private DiscordClient _client;
        
        
                
        static void Main(string[] args) => new Program().Start(args);

        private void Start(string[] args)
        {
            //Enter login info here
            GlobalSettings.Discord.Email = "";
            GlobalSettings.Discord.Password = "";

            //GlobalSettings.Save();
            GlobalSettings.Load();

            _client = new DiscordClient(x =>
            {
                x.AppName = "S.O.T.N-BOT";
            })
            .UsingCommands(x => {
                x.HelpMode = HelpMode.Public;
            })
            .UsingModules();

            _client.AddModule<SearchModule>("Search", ModuleFilter.None);
            _client.AddModule<DiabloModule>("Diablo", ModuleFilter.None);

            _client.ExecuteAndWait(async () =>
            {
                await _client.Connect(GlobalSettings.Discord.Email, GlobalSettings.Discord.Password);
                _client.SetGame("sotn.malven.se");
            });
        }
    }
}
