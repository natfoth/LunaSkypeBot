using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Utillities;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class RemoveGlobalAdminCommand : CommandProcessor
    {
        public RemoveGlobalAdminCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "RemoveGlobalAdmin" };

        public override bool AdminCommand => true;


        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var parameter = GetParameter(command);

            if (!ConfigManager.GlobalConfig.GlobalAdmins.Contains(parameter.ToLowerInvariant()))
            {
                msg.SendBotMessage("Global Admins doesnt contain the user : " + parameter);
                return true;
            }
            else
            {
                ConfigManager.GlobalConfig.GlobalAdmins.Remove(parameter.ToLowerInvariant());
                msg.SendBotMessage("Removed the user : " + parameter + " - From the Global Admin List");

                ConfigManager.GlobalConfig.Save();
            }

            // msg.SendBotMessage("Adding Quote : \"" + quoteToAdd + "\" - To Pony (" + pony + ")");

            return true;
        }
    }
}
