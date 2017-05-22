using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XenUpdaterAPI
{
    public class PackageConfig
    {
        public string package_version { get; set; }
        public string package_needrestart { get; set; }
        public string package_executable { get; set; }
        public string package_data { get; set; }
        public string package_patchnotes { get; set; }
        public KeyValuePair<string, string> package_files { get; set; }
    }

}
