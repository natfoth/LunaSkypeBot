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
    public class AddGlobalAdminCommand : CommandProcessor
    {
        public AddGlobalAdminCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "AddGlobalAdmin" };

        public override bool AdminCommand => true;


        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var parameter = GetParameter(command);

            if (ConfigManager.GlobalConfig.GlobalAdmins.Contains(parameter.ToLowerInvariant()))
            {
                msg.SendBotMessage("Global Admins already contains the user : " + parameter);
                return true;
            }
            else
            {
                ConfigManager.GlobalConfig.GlobalAdmins.Add(parameter.ToLowerInvariant());
                msg.SendBotMessage("Added the user : " + parameter + " - To the Global Admin List");

                ConfigManager.GlobalConfig.Save();
            }

           // msg.SendBotMessage("Adding Quote : \"" + quoteToAdd + "\" - To Pony (" + pony + ")");

            return true;
        }
    }
}
