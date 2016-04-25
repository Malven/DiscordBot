using Discord;
using Discord.Modules;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltarAPIDiabloIII;

namespace SotnBot.Modules.Diablo
{
    internal class DiabloModule : IModule
    {
        private ModuleManager _manager;
        private DiscordClient _client;
        D3Cache cache;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;
            _client = manager.Client;
            cache = new D3Cache();
            cache.ApiKey = "3xq99zhmrdeeweb2zq3sybynbrszymkq";
            cache.DefaultRegion = AltarAPIDiabloIII.Region.EU;

            manager.CreateCommands("search", group =>
            {
                group.CreateCommand("profile")
                .Description("Return all heroes from a profile. Tag = username#1234 on B.Net.")
                .Parameter("tag", ParameterType.Required)
                .Do(async e =>
                {
                    Career awaitedCareer = await cache.GetCareerAsync(e.GetArg("tag").ToString());
                    string heroNames = "";
                    foreach (var hero in awaitedCareer.Heroes)
                    {
                        heroNames += hero.Name + "\n";
                    }
                    await e.Channel.SendMessage(heroNames);
                });

                group.CreateCommand("item")
                .Description("Searches for an item")
                .Parameter("item", ParameterType.Unparsed)
                .Do(async e =>
                {
                    string temp = e.GetArg("item").Replace(" ", "-");
                    Item item = await cache.GetFullItemAsync("item/" + temp.ToString());
                    await e.Channel.SendMessage("http://eu.battle.net/d3/en/"+item.TooltipParams);
                });
            });

            manager.CreateCommands("build", group =>
            {
                group.CreateCommand("barbarian")
                .Description("Returns a link to a Barbarian solo build unless 'barbarian group' is specified.")
                .Alias("barb")
                .Parameter("group", ParameterType.Optional)
                .Do(async e =>
                {
                    await e.Channel.SendIsTyping();
                    if(e.GetArg("group").ToString() == "group")
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69882-2-4-gr100-zdps-globe-barb-group");
                    } else
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69865-2-4-gr90-raekor-furious-charge");
                    }
                });

                group.CreateCommand("demonhunter")
                .Description("Returns a link to a Demon Hunter solo build unless 'demonhunter group' is specified.")
                .Alias("dh")
                .Parameter("group", ParameterType.Optional)
                .Do(async e =>
                {
                    await e.Channel.SendIsTyping();
                    if (e.GetArg("group").ToString() == "group")
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69873-2-4-gr92-dh-shadow-impale");
                    }
                    else
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69836-2-4-gr-75-86-nats-fok-fulminator");
                    }
                });

                group.CreateCommand("wizard")
                .Description("Returns a link to a Wizard solo build unless 'wizard group' is specified.")
                .Alias("wiz")
                .Parameter("group", ParameterType.Optional)
                .Do(async e =>
                {
                    await e.Channel.SendIsTyping();
                    if (e.GetArg("group").ToString() == "group")
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69867-2-4-gr100-energy-twister");
                    }
                    else
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69867-2-4-gr100-energy-twister");
                    }
                });

                group.CreateCommand("monk")
                .Description("Returns a link to a Monk solo build unless 'monk group' is specified.")
                .Alias("mo")
                .Parameter("group", ParameterType.Optional)
                .Do(async e =>
                {
                    await e.Channel.SendIsTyping();
                    if (e.GetArg("group").ToString() == "group")
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69908-2-4-gr100-loh-heal-monk-group");
                    }
                    else
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69914-2-4-gr85-lon-flurry-ep");
                    }
                });

                group.CreateCommand("crusader")
                .Description("Returns a link to a Crusader solo build unless 'crusader group' is specified.")
                .Alias("cru", "sader")
                .Parameter("group", ParameterType.Optional)
                .Do(async e =>
                {
                    await e.Channel.SendIsTyping();
                    if (e.GetArg("group").ToString() == "group")
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69853-2-4-gr90-lon-bombardment");
                    }
                    else
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69853-2-4-gr90-lon-bombardment");
                    }
                });

                group.CreateCommand("witchdoctor")
                .Description("Returns a link to a Witch Doctor build unless 'witchdoctor group' is specified.")
                .Alias("wd")
                .Parameter("group", ParameterType.Optional)
                .Do(async e =>
                {
                    await e.Channel.SendIsTyping();
                    if (e.GetArg("group").ToString() == "group")
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69876-2-4-gr100-wd-support-group");
                    }
                    else
                    {
                        await e.Channel.SendMessage(e.User.Mention + " http://www.diablofans.com/builds/69927-2-4-gr90-lon-dart-wd-group");
                    }
                });

            });
        }
    }
}
