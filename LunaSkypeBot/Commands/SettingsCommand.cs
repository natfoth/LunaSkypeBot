using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Utils;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class SettingsCommand : CommandProcessor
    {
        public  SettingsCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "setting" };

        public Dictionary<string, int> SettingsAllowedToChange
        {
            get
            {
                var newDict = new Dictionary<string, int>();

                newDict.Add("SFWOnly".ToLowerInvariant(), 100);
                newDict.Add("CelestiaRetaliation".ToLowerInvariant(), 100);

                return newDict;
            }
        }   

        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var props = ConfigManager.Config.GetType().GetProperties();

            var removedCommand = command.Replace("setting", "").Trim();

            var variable = removedCommand.SubStringTillChar(" ").Trim();

            var removedVariableAndCommand = removedCommand.Replace(variable, "").Trim();

            var parameter = removedVariableAndCommand.Trim();

            if (!SettingsAllowedToChange.ContainsKey(variable.ToLowerInvariant()))
            {
                if(props.Any(prop => string.Equals(prop.Name, variable, StringComparison.InvariantCultureIgnoreCase)))
                    msg.SendBotMessage(variable + " - Is Not Allowed to be changed");
                else
                    msg.SendBotMessage("Setting : (" + variable + ") - Not Found");

                return true;
            }

            var accessLevelRequired = SettingsAllowedToChange[variable.ToLowerInvariant()];

            if (GetSenderAccessLevel(msg) < accessLevelRequired)
            {
                msg.SendBotMessage("You do not have access to change the variable (" +variable + ")");
                return true;
            }
            

            foreach (var prop in props)
            {
                if (prop.Name.ToLower() == variable.ToLower())
                {
                   if(prop.Name.ToLower().Contains("nickname"))
                   {
                        msg.SendBotMessage("Setting (" + variable + ") Is not allowed to be changed");
                        return true;
                   }

                    prop.SetValue(ConfigManager.Config, Convert.ChangeType(parameter, prop.PropertyType), null);
                    ConfigManager.Config.Save();

                    msg.SendBotMessage(prop.Name + " - set to : " + prop.GetValue(ConfigManager.Config));
                    return true;
                }

                //Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(foo, null));
            }

            msg.SendBotMessage("Setting (" + variable + ") - Not Found");


            return true;
        }
    }
}
