using Discord;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class SotnBot
    {
        private DiscordClient bot;

        public SotnBot()
        {
            bot = new DiscordClient();
            bot.ExecuteAndWait(async () =>
            {
                bot.MessageReceived += Bot_MessageReceived;

                await bot.Connect("dahil82@gmail.com", "d6qkwa52");

                bot.SetGame("I'm ONLINE");
            });
        }

        private void Bot_MessageReceived(object sender, MessageEventArgs e)
        {
            //If we sent the message, return out
            if (e.Message.IsAuthor) return;

            if (e.Message.Text == "!invite")
            {
                e.Channel.SendMessage(e.User.Mention + " http://sotn.malven.se");
            }

            if (e.Message.Text.ToLower().Contains("sotnbot"))
            {
                e.Channel.SendMessage(e.User.Mention + " I'm the bot! type !help and I will send you a PM with the commands.");
            }

            if (e.Message.Text == "!logo")
            {
                e.Channel.SendFile("images/logo.jpg");
            }
            if (e.Message.Text == "!help")
            {
                e.User.SendMessage("!invite : shows to invite link.\n" +
                                   "!logo : shows the server logo.\n" +
                                   "!imdb moviename : shows the first movie it finds.\n" +
                                   "!lmgtfy searchstring: google something...");
            }

            if (e.Message.Text.ToLower().Contains("!imdb"))
            {
                string result;
                string query = e.Message.Text.Substring(6);
                e.Channel.SendIsTyping();
                try
                {
                    var movie = ImdbScraper.ImdbScrape(query, true);
                    if (movie.Status) result = movie.ToString();
                    else result = "Failed to find that movie.";
                }
                catch
                {
                    e.Channel.SendMessage("Failed to find that movie.");
                    return;
                }

                e.Channel.SendMessage(result.ToString());
            }

            if (e.Message.Text.ToLower().Contains("!lmgtfy"))
            {
                string query = e.Message.Text.Substring(8);
                e.Channel.SendIsTyping();
                try
                {
                    if (query == null || query.Length < 1) return;
                    e.Channel.SendMessage("http://lmgtfy.com/?q="+ Uri.EscapeUriString(query));
                }
                catch
                {
                    e.Channel.SendMessage("Something went wrong.");
                }
            }
        }
    }
}
