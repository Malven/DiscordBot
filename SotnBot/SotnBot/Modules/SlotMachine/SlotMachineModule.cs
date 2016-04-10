using Discord;
using Discord.Commands;
using Discord.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SotnBot.Classes;

namespace SotnBot.Modules.SlotMachine
{
    public class SlotMachineModule : IModule
    {
        ModuleManager _manager;
        DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;
            _client = manager.Client;

            _manager.CreateCommands("slot", group =>
            {
                group.CreateCommand("play")
                .Description("Play the slot machine.")
                .Parameter("bet", ParameterType.Required)
                .Do(async e =>
                {
                    int bet;
                    int.TryParse(e.GetArg("bet"), out bet);
                    if(bet > 0)
                    {
                        await e.User.CreatePMChannel();
                        await e.User.PrivateChannel.SendMessage(SlotMachineFactory.Play(e.User, bet));
                    }
                    else
                    {
                        await e.Channel.SendMessage("Invalid bet.");
                    }
                    
                });
            });
        }
    }
}
