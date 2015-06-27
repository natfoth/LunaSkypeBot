using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Utils;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class CelestiaCommand : CommandProcessor
    {
        public CelestiaCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "Celestia" };

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

            msg.SendBotMessage(GetRandomCelestiaPic());

            return true;
        }
    }
}
