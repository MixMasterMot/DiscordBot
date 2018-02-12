using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MotBot1.Modules
{
    class Config
    {
        private static Dictionary<string, string> alerts;
        
        static Config()
        {
            string json = File.ReadAllText("SystemLang/Config.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            alerts = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetValue(string key)
        {
            if (alerts.ContainsKey(key)) return alerts[key];
            return "";
        }
    }
}
