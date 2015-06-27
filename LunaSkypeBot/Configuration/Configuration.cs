using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LunaSkypeBot.Configuration
{
    public class Configuration
    {
        private string _path;

        // ReSharper disable once UnusedMember.Local
        private Configuration() { }

        private Configuration(string path)
        {
            _path = path;
        }

        public string ChatName { get; set; } = "";

        public bool SFWOnly { get; set; } = true;
        public bool CelestiaRetaliation { get; set; } = false;
        public Dictionary<string, int> AccessLevels { get; set; } = new Dictionary<string, int>(); 


        #region Load & Save
        public static Configuration LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                var config = CreateDefaultConfig(path);
                return config;
            }
            else
            {
                var config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(path));
                config._path = path;
                return config;
            }
        }

        private static Configuration CreateDefaultConfig(string path)
        {
            var config = new Configuration(path);

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

