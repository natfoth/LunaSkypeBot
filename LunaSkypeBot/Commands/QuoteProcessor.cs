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
    public class QuoteProcessor : CommandProcessor
    {
        public QuoteProcessor(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {
        }

        public override List<string> Commands => new List<string>() { "q", "Quote" };

        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            var parameter = "";

            if (command.StartsWith("quote"))
                parameter = command.ToLower().Replace("quote", "").Trim();
            else
                parameter = command.ToLower().Substring(1).Trim();

            var quote = GetRandomQuote(parameter);

            msg.SendBotMessage(quote);

            return true;
        }

        private string GetRandomQuote(string pony = "")
        {
            if (pony.Length == 0)
            {
                var listOfQuotes = new List<string>();
                listOfQuotes.Add("Sweet Celestia! - Spike");
                listOfQuotes.Add("Holy Guacamole! - Spike");
                listOfQuotes.Add(
                    "From all of us together. Together we are friends. With the marks of our destinies made one, there is magic without end! - Twilight (Unicorn)");
                listOfQuotes.Add("Will you accept my friendship? - Celestia");
                listOfQuotes.Add("I'm So Sorry! - Luna");
                listOfQuotes.Add("Yay Ponies! - Everypony");
                listOfQuotes.Add("Fun? What is this 'fun' thou speakest of? - Luna");
                listOfQuotes.Add("Ha ha! The fun has been doubled! - Luna");
                listOfQuotes.Add("Tis a lie! - Luna");
                listOfQuotes.Add("Tis a lie! Thy backside is whole and ungobbled, thou ungrateful whelp! - Luna");
                listOfQuotes.Add("Huzzah! How many points do I receive? - Luna");
                listOfQuotes.Add("Rest, my sister.As always, I will guard the night. - Luna");
                listOfQuotes.Add("Who goes there? Stay indoors, Twilight Sparkle. - Luna");
                listOfQuotes.Add(
                    "Sometimes we can worry about a thing so much, the fear can make us feel we're trapped in a nightmare. - Luna");
                listOfQuotes.Add("It is time to face your real fears. - Luna");

                return listOfQuotes[Random.Next(listOfQuotes.Count)];
            }

            var fileFound = File.Exists("Quotes\\" + pony + ".txt");
            if (!fileFound)
                return "Pony not found";

            var listOfSpecificQuotes = new List<string>();

            var strings = File.ReadAllLines("Quotes\\" + pony + ".txt");

            var convertedStrings = new List<string>();
            var nsfwConvertedStrings = new List<string>();
            foreach (var s in strings)
            {
                if (s.ToLowerInvariant().StartsWith("nsfw"))
                    nsfwConvertedStrings.Add(string.Format("\"{0}\"" + " - " + pony, s.Replace("nsfw", "").Trim()));
                else
                    convertedStrings.Add(string.Format("\"{0}\"" + " - " + pony, s));
            }

            listOfSpecificQuotes.AddRange(convertedStrings);

            if (ConfigManager.Config.SFWOnly == false)
            {
                if (listOfSpecificQuotes.Count == 1 && nsfwConvertedStrings.Count == 1)
                    listOfSpecificQuotes = nsfwConvertedStrings;
                else
                    listOfSpecificQuotes.AddRange(nsfwConvertedStrings);
            }

            listOfSpecificQuotes.Shuffle();


            return listOfSpecificQuotes[Random.Next(listOfSpecificQuotes.Count)];
        }
    }
}
