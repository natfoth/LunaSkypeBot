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
    public class AddQuoteCommand : CommandProcessor
    {
        public AddQuoteCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {

        }

        public override List<string> Commands => new List<string>() { "AddQuote" };


        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var pony = GetParameter(command);
            var quoteToAdd = GetSecondaryParameter(command);


            if (!File.Exists("Quotes\\" + pony.ToLowerInvariant() + ".txt"))
            {
                //File.Create("Quotes\\" + pony + ".txt");

                File.WriteAllText("Quotes\\" + pony.ToLowerInvariant() + ".txt", quoteToAdd);
            }
            else
            {
                File.AppendAllText("Quotes\\" + pony.ToLowerInvariant() + ".txt", string.Format("{0}{1}", Environment.NewLine, quoteToAdd));
            }


            msg.SendBotMessage("Adding Quote : \"" + quoteToAdd + "\" - To Pony (" + pony + ")");

            return true;
        }
    }
}
