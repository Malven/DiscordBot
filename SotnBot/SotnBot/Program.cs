﻿using Discord;
using Discord.Commands;
using Discord.Modules;
using SotnBot.Modules.Diablo;
using SotnBot.Modules.Search;
using SotnBot.Modules.Public;
using SotnBot.Modules.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SotnBot.Classes;
using SotnBot.Modules.SlotMachine;

namespace SotnBot
{
    class Program
    {
        private DiscordClient _client;
        public List<Server> server;

        private bool _isUpdated = false;
                
        static void Main(string[] args) => new Program().Start(args);

        private void Start(string[] args)
        {
            //GlobalSettings.Save();
            //GlobalSettings.Load();
            EconomyFactory.Load();

            _client = new DiscordClient(x =>
            {
                x.AppName = "S.O.T.N-BOT";
            })
            .UsingCommands(x => {
                x.PrefixChar =  '!';
                x.HelpMode = HelpMode.Public;
            })
            .UsingModules();

            _client.AddModule<SearchModule>("Search", ModuleFilter.None);
            _client.AddModule<DiabloModule>("Diablo", ModuleFilter.None);
            _client.AddModule<PublicModule>("Suggestion", ModuleFilter.None);
            _client.AddModule<EconomyModule>("Bank", ModuleFilter.None);
            _client.AddModule<SlotMachineModule>("Slot Machine", ModuleFilter.None);

            _client.ExecuteAndWait(async () =>
            {
                //https://discordapp.com/oauth2/authorize?&client_id=&scope=bot&permissions=46245
                await _client.Connect("");
                _client.SetGame("'!help'");
                server = _client.Servers.ToList();
                if (_isUpdated)
                {
                    foreach (var serv in server)
                    {
                        await _client.GetChannel(serv.Id).SendMessage("**I've been updated, type !help to see all commands.**");
                    }
                }
                //await _client.GetChannel(server[1].Id).SendMessage("**I've been updated, '@" + _client.CurrentUser.Name + " help' to see all commands.**");

            });
        }
    }
}
