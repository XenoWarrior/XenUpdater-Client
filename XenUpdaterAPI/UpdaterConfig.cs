using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenUpdaterAPI
{
    public class UpdaterConfig
    {
        public string service_name { get; set; }
        public KeyValuePair<string, string> apps { get; set; }
    }
}
