using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Modules.Status
{
    public class Settings
    {
        public DateTimeOffset LastUpdate { get; set; } = DateTimeOffset.UtcNow;
        public ulong? Channel { get; set; }
    }
}
