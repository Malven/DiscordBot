using Discord;
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

        private bool _isUpdated = false;
                
        static void Main(string[] args) => new Program().Start(args);
        
        public void Start(string[] args)
        {
            //GlobalSettings.Save();
            //GlobalSettings.Load();
            EconomyFactory.Load();
            LevelSystemFactory.Load();

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


            _client.ExecuteAndWait(async () => {

                _client.MessageReceived += ( sender, e ) => {
                    if ( !e.Message.IsAuthor ) {
                        SotnBot.Classes.User temp = LevelSystemFactory.FindUser( e.User );
                        if ( temp == null ) {
                            LevelSystemFactory.AddUserToLevelSystem( e.User );
                        }
                        else {
                            int exp = e.Message.Text.Length;
                            if ( e.Message.Text.Contains( "http" ) ) {
                                exp += 10;
                            }
                            LevelSystemFactory.AddExpTouser( temp, exp );
                        }
                    }
                };

                //https://discordapp.com/oauth2/authorize?&client_id=168225708793921537&scope=bot&permissions=46245
                await _client.Connect("MTY4MjI5MTYxMDAzOTA5MTIw.CeojTg.EVdKvGK5iImHy22BgifSECpwlPI");
                _client.SetGame("'!help'");
                //server = _client.Servers.ToList();
                //if (_isUpdated)
                //{
                //    foreach (var serv in server)
                //    {
                //        await _client.GetChannel(serv.Id).SendMessage("@here **I've been updated, type !help to see all commands.**");
                //    }
                //}
                //await _client.GetChannel(server[1].Id).SendMessage("**I've been updated, '!help' to see all commands.**");

            });
        }
    }
}
