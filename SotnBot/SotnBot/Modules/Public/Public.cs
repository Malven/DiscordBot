using Discord;
using Discord.Modules;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot.Modules.Public
{
    public class PublicModule : IModule
    {
        private ModuleManager _manager;
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;
            _client = manager.Client;

            _manager.CreateCommands("", group =>
            {
                group.CreateCommand("suggestion")
                .Description("Send a suggestion on what to implement to the bot, encapsulate the suggestion with ', ex: 'add more lewt'.")
                .Parameter("text")
                .Do(async e =>
                {
                    await e.Channel.SendIsTyping();
                    Console.WriteLine(e.Args[0].ToString() + " by user: " + e.User.Name);
                    await e.Channel.SendMessage("Thank you for your suggestion!");
                });
            });
        }
    }
}
