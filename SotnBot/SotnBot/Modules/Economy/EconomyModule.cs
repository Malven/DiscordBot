using Discord;
using Discord.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SotnBot.Classes;
using Discord.Commands;

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
                    await e.Channel.SendMessage(e.User.Mention + " " + EconomyFactory.AddUser(e.User));
                    Console.WriteLine("Added user: " + e.User.Name + " to Bank.");
                });

                //PayDay
                group.CreateCommand("payday")
                .Description("Receive money once a day.")
                .Alias("pd")
                .Do(async e =>
                {
                    await e.Channel.SendMessage(e.User.Mention + " " + EconomyFactory.PayDay(e.User));
                });

                //CheckTotal
                group.CreateCommand("balance")
                .Description("Show amount of cash you have in the bank.")
                .Alias("bal")
                .Do(async e =>
                {
                    await e.Channel.SendMessage(e.User.Mention + " " + EconomyFactory.CheckTotal(e.User));
                });

                //transfer funds
                group.CreateCommand("transfer")
                .Description("Transfer X amount of cash to another user.")
                .Alias("give")
                .Parameter("receiver", ParameterType.Required)
                .Parameter("amount", ParameterType.Required)
                .Do(async e =>
                {
                    Discord.User _receiver = e.Channel.FindUsers(e.GetArg("receiver"), true).First();
                    if (_receiver.Id == e.User.Id)
                    {
                        await e.Channel.SendMessage(e.User.Mention + " You can't give money to yourself!");
                        return;
                    }
                    
                    if (_receiver != null)
                    {
                        await e.Channel.SendMessage(e.User.Mention);
                        int _amount = 0;
                        int.TryParse(e.GetArg("amount"), out _amount);
                        if (_amount > 0)
                            await e.Channel.SendMessage(e.User.Mention + " "+ EconomyFactory.TransferFunds(e.User, _receiver, _amount));
                        else
                            await e.Channel.SendMessage(e.User.Mention + " Invalid amount.");
                    }
                    else
                        await e.Channel.SendMessage(e.User.Mention + " Couldn't find user: " + e.GetArg("receiver")  + ".");

                });

                //Bankinfo
                group.CreateCommand("info")
                .Description("Shows info about the bank and todo list.")
                .Do(async e =>
                {
                    await e.Channel.SendMessage(e.User.Mention + " " + EconomyFactory.Instance.bank.bankName + "\n" +
                            "Registered users: " + EconomyFactory.Instance.users.Count() + ".\n" +
                            "Bank cash amount: $" + EconomyFactory.Instance.bank.cashAmount + ".\n" +
                            "**TODO**\n:one:transfer cash between users/bank.**DONE**\n:two:more ways to earn cash(slot machines perhaps?)**DONE**\n" +
                            ":three:ability to remove oneself from the bank."
                        );
                });
                
            });
        }
    }
}
