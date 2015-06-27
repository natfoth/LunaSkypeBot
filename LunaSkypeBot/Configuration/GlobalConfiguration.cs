using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LunaSkypeBot.Configuration
{
    public class GlobalConfiguration
    {
        private string _path;

        // ReSharper disable once UnusedMember.Local
        private GlobalConfiguration() { }

        private GlobalConfiguration(string path)
        {
            _path = path;
        }


        public Dictionary<string, int> Permissions { get; set; } = new Dictionary<string, int>();
        public List<string> GlobalAdmins { get; set; } = new List<string>() { "natfoth", "oliver.cbaker" };


        #region Load & Save
        public static GlobalConfiguration LoadConfig(string path = "Configs\\GlobalConfig.config")
        {
            if (!File.Exists(path))
            {
                var config = CreateDefaultConfig(path);
                return config;
            }
            else
            {
                var config = JsonConvert.DeserializeObject<GlobalConfiguration>(File.ReadAllText(path));
                config._path = path;
                return config;
            }
        }

        private static GlobalConfiguration CreateDefaultConfig(string path)
        {
            var config = new GlobalConfiguration(path);

            config.Save();

            return config;
        }

        public void Save()
        {
            if (!Directory.Exists("Configs"))
                Directory.CreateDirectory("Configs");

            File.WriteAllText(_path, JsonConvert.SerializeObject(this));
        }

        #endregion
    }
}
