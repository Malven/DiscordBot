using Discord;
using Discord.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SotnBot.Classes;

namespace SotnBot.Modules.Economy
{
    class EconomyModule : IModule
    {
        private ModuleManager _manager;
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;
            _client = manager.Client;

            _manager.CreateCommands("bank", group =>
            {
                //Register command
                group.CreateCommand("register")
                .Description("Register at S.O.T.N-BANK.")
                .Alias("reg")
                .Do(async e =>
                {
                    await e.Channel.SendMessage(EconomyFactory.AddUser(e.User));
                    Console.WriteLine("Added user: " + e.User.Name + " to Bank");
                });

                //PayDay
                group.CreateCommand("payday")
                .Description("Receive money once a day,")
                .Alias("pd")
                .Do(async e =>
                {
                    await e.Channel.SendMessage(EconomyFactory.PayDay(e.User));
                });

                //CheckTotal
                group.CreateCommand("total")
                .Description("Shows amount of cash in the bank.")
                .Do(async e =>
                {
                    await e.Channel.SendMessage(EconomyFactory.CheckTotal(e.User));
                });

                //Bankinfo
                group.CreateCommand("info")
                .Description("Shows info about the bank and todo list.")
                .Do(async e =>
                {
                    await e.Channel.SendMessage(EconomyFactory.Instance.bank.bankName + "\n" +
                            "Registered users: " + EconomyFactory.Instance.users.Count() + ".\n" +
                            "Bank cash amount: $" + EconomyFactory.Instance.bank.cashAmount + ".\n" +
                            "**TODO**\n:one:transfer cash between users/bank.\n:two:more ways to earn cash(slot machines perhaps?)\n" +
                            ":three:ability to remove oneself from the bank."
                        );
                });
                
            });
        }
    }
}
