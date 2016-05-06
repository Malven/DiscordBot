using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotnBot.Classes {
    public class LevelSystem {
        public User[] Users { get; set; }

        [JsonIgnore]
        public const float expConstant = 0.03f;

        public LevelSystem() {
            Users = new User[ 0 ];
        }
    }
}
