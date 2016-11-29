using Discord;
using Discord.API.Status;
using Discord.API.Status.Rest;
using Discord.Commands;
using Discord.Commands.Permissions;
using Discord.Modules;
using Discord.Net;
using ConsoleApplication1.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Modules.Status
{
    internal class StatusModule : IModule
    {
        private ModuleManager _manager;
        private DiscordClient _client;
        private bool _isRunning;
        private HttpService _http;
        private SettingsManager<Settings> _settings;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;
            _client = manager.Client;
            _http = _client.GetService<HttpService>();
            _settings = _client.GetService<SettingsService>().AddModule<StatusModule, Settings>(manager);

            manager.CreateCommands("status", group => {
                group.CreateCommand("enable")
                .Parameter("channel", ParameterType.Optional)
                .Do(async e => {
                    var settings = _settings.Load();
                });
            });
        }
    }
}
