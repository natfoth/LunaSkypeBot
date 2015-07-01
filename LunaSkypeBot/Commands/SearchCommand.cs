using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Database;
using LunaSkypeBot.Utillities;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class SearchCommand : CommandProcessor
    {
        public SearchCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() {"s", "search"};


        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var searchMessage = msg.SendBotMessage("Searching....");

            var searchTerms = "";

            if (command.StartsWith("s "))
                searchTerms = command.Remove(0, 1).Trim();
            else
            {
                searchTerms = command.Remove(0, 6).Trim();
            }


            var result = DatabaseManager.GetRandomImageSearch(searchTerms);

            if(result.Length == 0)
                searchMessage.EditMessage("No Results Found");
            else
            {
                searchMessage.EditMessage(result);
            }

            return true;
        }
    }
}
