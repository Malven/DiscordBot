using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot
{
    public class GlobalSettings
    {
        private const string path = "./config/global.json";
        private static GlobalSettings _instance = new GlobalSettings();

        public static void Load()
        {
            if (!File.Exists(path))
            {
                //throw new FileNotFoundException($"{path} is missing.");
                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var writer = new StreamWriter(stream))
                    writer.Write(JsonConvert.SerializeObject(_instance, Formatting.Indented));
            }

            _instance = JsonConvert.DeserializeObject<GlobalSettings>(File.ReadAllText(path));

        }
        public static void Save()
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)) 
            using (var writer = new StreamWriter(stream))
                writer.Write(JsonConvert.SerializeObject(_instance, Formatting.Indented));
        }

        //Discord
        public class DiscordSettings
        {
            [JsonProperty("username")]
            public string Email;
            [JsonProperty("password")]
            public string Password;
        }
        [JsonProperty("discord")]
        private DiscordSettings _discord = new DiscordSettings();
        public static DiscordSettings Discord => _instance._discord;
    }
}
