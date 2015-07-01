using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Utillities;
using Newtonsoft.Json;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class LunaOfTheDayCommand : CommandProcessor
    {
        public LunaOfTheDayCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "LunaOfTheDay" };

        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
           // var chatHandler = msg.

            ChatClass chat = (ChatClass) msg.Chat;

            var chatname = chat.Name;

            var list = ConfigManager.GlobalConfig.LunaOfTheDayList;

            if (list.Contains(chatname))
            {
                ConfigManager.GlobalConfig.LunaOfTheDayList.Remove(chatname);
                msg.SendBotMessage("Removed from the Luna Of The Day List");
            }
            else
            {
                ConfigManager.GlobalConfig.LunaOfTheDayList.Add(chatname);
                msg.SendBotMessage("Registered to the Luna Of The Day List");
            }

            ConfigManager.GlobalConfig.Save();

            return true;
        }
    }
}
