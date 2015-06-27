using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Utils;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class LunaCommand : CommandProcessor
    {
        public LunaCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "Luna" };

        private string GetRandomLunaPic()
        {

            var files = Directory.GetFiles("G:\\Dropbox\\Public\\Pics\\pony\\Luna\\5 Star").ToList();
            if (files.Count == 0)
                return "";

            var random = new LunaRandom();

            var file = files[random.Next(files.Count)];
            var fileName = Path.GetFileName(file);

            var url = "https://dl.dropboxusercontent.com/u/69140190/Pics/pony/Luna/5%20Star/" + fileName;


            return url;
        }

        private string GetRandomCelestiaPic()
        {

            var files = Directory.GetFiles("G:\\Dropbox\\Public\\Pics\\pony\\Celestia\\").ToList();
            if (files.Count == 0)
                return "";


            var file = files[Random.Next(files.Count)];
            var fileName = Path.GetFileName(file);

            var url = "https://dl.dropboxusercontent.com/u/69140190/Pics/pony/Celestia/" + fileName;


            return url;
        }

        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            if (ConfigManager.Config.CelestiaRetaliation)
            {
                msg.SendBotMessage("Celestial Retaliation!");
                msg.SendBotMessage(GetRandomCelestiaPic());
            }
            else
            {
                msg.SendBotMessage(GetRandomLunaPic());
            }


            return true;
        }
    }
}
