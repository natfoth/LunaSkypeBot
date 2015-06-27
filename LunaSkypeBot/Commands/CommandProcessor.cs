using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Utils;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class CommandProcessor
    {
        protected CommandProcessor(ref List<CommandProcessor> listToAttachTo)
        {
            listToAttachTo.Add(this);
        }

        protected LunaRandom Random = new LunaRandom();

        public virtual string GetCommand(string command)
        {
            return command.Trim().SubStringTillChar(" ");
        }

        public virtual string GetParameter(string command)
        {
            return command.Replace(GetCommand(command), "").Trim().SubStringTillChar(" ");
        }

        public virtual string GetSecondaryParameter(string command)
        {
            var commandString = GetCommand(command);

            var trimmedString = command.Replace(commandString, "").Trim();

            var firstParameter = trimmedString.SubStringTillChar(" ");

            trimmedString = trimmedString.Replace(firstParameter, "").Trim();

            return trimmedString;
        }

        public virtual List<string> Commands => new List<string>();

        public virtual bool AdminCommand => false;
        public virtual int MinAccessLevel => 0;


        public bool IsAdmin(ChatMessage msg)
        {
            if (ConfigManager.GlobalConfig.GlobalAdmins.Contains(msg.Sender.Handle.ToLowerInvariant()))
                return true;

            return false;
        }

        public int GetSenderAccessLevel(ChatMessage msg)
        {
            if (IsAdmin(msg))
                return 255;

            if (!ConfigManager.Config.AccessLevels.ContainsKey(msg.Sender.Handle.ToLowerInvariant()))
                return 0;

            return ConfigManager.Config.AccessLevels[msg.Sender.Handle.ToLowerInvariant()];
        }


        public virtual bool AcceptCommand(ChatMessage msg, string input)
        {
            var inputCommand = GetCommand(input).ToLower();

            if (AdminCommand && !IsAdmin(msg))
                return false;

            if (!IsAdmin(msg) && MinAccessLevel > 0 && GetSenderAccessLevel(msg) < MinAccessLevel)
                return false;

            return Commands.Any(command => inputCommand == command.ToLower());
        }

        public virtual async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            return false;
        }

        
    }
}
