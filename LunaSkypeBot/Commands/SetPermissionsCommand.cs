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
    public class SetPermissionsCommand : CommandProcessor
    {
        public SetPermissionsCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "SetPermissions" };

        public override bool AdminCommand => true;


        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var parameter = GetParameter(command);
            var secondary = GetSecondaryParameter(command);

            int newAccessLevel;
            if (!int.TryParse(secondary, out newAccessLevel))
            {
                msg.SendBotMessage("Failed to parse new Access Level : ( " + secondary + " )" + " - For the user : ( " + parameter + " )");
                return true;
            }


            if (!ConfigManager.Config.AccessLevels.ContainsKey(parameter.ToLowerInvariant()))
            {
                ConfigManager.Config.AccessLevels.Add(parameter.ToLowerInvariant(), newAccessLevel);
            }
            else
            {
                ConfigManager.Config.AccessLevels[parameter.ToLowerInvariant()] = newAccessLevel;
            }

            msg.SendBotMessage("Set the Access Level for the User : ( " + parameter + " )" + " - To Level : ( " + newAccessLevel + " )");

            ConfigManager.Config.Save();

            // msg.SendBotMessage("Adding Quote : \"" + quoteToAdd + "\" - To Pony (" + pony + ")");

            return true;
        }
    }
}
