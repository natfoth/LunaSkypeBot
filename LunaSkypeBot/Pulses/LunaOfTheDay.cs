using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Utillities;

namespace LunaSkypeBot.Pulses
{
    public class LunaOfTheDay
    {
        public LunaOfTheDay(Form1 form)
        {
            form.Pulsed += new PulseDelegate(Pulse);
        }

        public DateTime LastSent = DateTime.MinValue;

        public void Pulse()
        {
            if (DateTime.Now.Hour == 9 && DateTime.Now.Minute < 10 && LastSent.Date != DateTime.Now.Date)
            {
                LastSent = DateTime.Now;

                var messageToSend = "";


                messageToSend = GetRandomLunaPic();


                foreach (var item in ConfigManager.GlobalConfig.LunaOfTheDayList)
                {
                    var chat = Program.Form.skype.Chat[item];

                    chat.SendBotMessage("Luna of The Day : " + messageToSend);
                }

            }
        }

        private string GetRandomLunaPic()
        {

            var files = Directory.GetFiles("G:\\Dropbox\\Public\\Pics\\pony\\Luna\\5 Star", "*", SearchOption.TopDirectoryOnly).ToList();
            if (files.Count == 0)
                return "";

            var random = new LunaRandom();

            var file = files[random.Next(files.Count)];
            var fileName = Path.GetFileName(file);

            var url = "https://dl.dropboxusercontent.com/u/69140190/Pics/pony/Luna/5%20Star/" + fileName;


            return url;
        }

    }
}
