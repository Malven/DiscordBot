using Discord;
using Discord.Commands;
using Discord.Modules;
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
            _client = new DiscordClient(x =>
            {
                x.AppName = "S.O.T.N-BOT";
            })
            .UsingCommands(x => {
                x.HelpMode = HelpMode.Public;
            })
            .UsingModules();

            _client.AddModule<SearchModule>("Search", ModuleFilter.None);

            _client.ExecuteAndWait(async () =>
            {
                await _client.Connect("dahil82@gmail.com", "d6qkwa52");
                _client.SetGame("sotn.malven.se");
            });
        }
    }
}
