using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Utillities;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class MisCommands : CommandProcessor
    {
        public MisCommands(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "storm", "kill" };


        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var whichCommand = GetCommand(command);
            var message = "";
            switch (whichCommand)
            {
                case "storm":
                    message = "Quick! Hide in the barn!";
                    break;
                case "kill":
                    message = "The Luna Natwork is not allowed to be disabled";
                    break;
            }

            if (message.Length == 0)
                return true;

            msg.SendBotMessage(message);

            return true;
        }
    }
}
