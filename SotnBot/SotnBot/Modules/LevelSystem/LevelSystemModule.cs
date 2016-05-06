using Discord;
using Discord.Modules;
using SotnBot.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot.Modules.LevelSystem {
    class LevelSystemModule : IModule {
        private ModuleManager _manager;
        private DiscordClient _client;

        void IModule.Install( ModuleManager manager ) {
            _manager = manager;
            _client = manager.Client;

            manager.CreateCommands( "level", group => {
                group.CreateCommand( "info" )
                .Description( "Shows you some info and commands" )
                .Do( async e => {
                    //TODO show a users level
                    await e.Channel.SendMessage( "**Welcome to S.O.T.N Level System**\n" +
                                                 "!level stats: Shows your current stats or add you to the system if you are not apart of it.\n" +
                                                 "Current Users in the system: " + LevelSystemFactory.Instance.Users.Length + ".");
                } );
            } );
        }
    }
}
