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
        private bool _isRunning;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;
            _client = manager.Client;

            manager.CreateCommands("search", group =>
            {
                group.CreateCommand("imdb")
                .Description("Searches IMDB.")
                .Parameter("value", ParameterType.Required)
                .Do(async e =>
                {
                    e.Channel.SendIsTyping();
                    string result;
                    var movie = ImdbScraper.ImdbScrape(e.Args[0], true);
                    if (movie.Status) result = movie.ToString();
                    else result = "Couldn't find that movie.";
                    await e.Channel.SendMessage(result);
                });
            });
        }
    }
}
