using Discord;
using Discord.Commands;
using Discord.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot.Modules.Search
{
    internal class SearchModule : IModule
    {
        private ModuleManager _manager;
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;
            _client = manager.Client;

            manager.CreateCommands("", group =>
            {
                group.CreateCommand("imdb")
                .Description("Searches IMDB, returns the first movie it finds with the title.")
                .Parameter("title", ParameterType.Required)
                .Do(async e =>
                {
                    await e.Channel.SendIsTyping();
                    string result;
                    var movie = ImdbScraper.ImdbScrape(e.Args[0], true);
                    if (movie.Status) result = movie.ToString();
                    else result = e.User.Mention + " Couldn't find that movie.";
                    await e.Channel.SendMessage(e.User.Mention + " " + result);
                });
            });
        }
    }
}
