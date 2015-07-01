using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Utillities;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class RandomCommand : CommandProcessor
    {
        public RandomCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "Random" };



        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var files = Directory.GetFiles("G:\\Dropbox\\Public\\Pics\\pony", "*", SearchOption.AllDirectories).ToList();
            if (files.Count == 0)
                return true;

            //files = Directory.GetFiles("G:\\Dropbox\\Public\\Pics\\pony\\Luna\\NSFW", "*", SearchOption.AllDirectories).ToList();

            var random = new LunaRandom();

            var file = files[random.Next(files.Count)];

            var dropboxLink = await Utils.GetDropboxLinkForFilePath(file);

            bool nsfw = false;

            if (ConfigManager.Config.SFWOnly)
            {
                while (dropboxLink.Contains(@"/nsfw/"))
                    dropboxLink = await Utils.GetDropboxLinkForFilePath(file);
            }

            if (dropboxLink.Contains(@"/nsfw/", StringComparison.OrdinalIgnoreCase))
                nsfw = true;


            if(nsfw)
                msg.SendBotMessage("NSFW : " + dropboxLink);
            else
                msg.SendBotMessage(dropboxLink);

            return true;
        }

        
    }
}
