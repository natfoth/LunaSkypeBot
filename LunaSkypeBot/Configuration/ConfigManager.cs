using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace LunaSkypeBot.Configuration
{
    public static class ConfigManager
    {
        public static GlobalConfiguration GlobalConfig = GlobalConfiguration.LoadConfig();
        public static Configuration Config = null;

        public static void LoadChatConfiguration(ChatMessage msg, string chatMessage)
        {
            string fileName = "";

            try
            {
                var chatTopic = msg.Chat.Topic;
                if (!string.IsNullOrWhiteSpace(chatTopic))
                {
                    fileName = System.IO.Path.GetInvalidFileNameChars().Aggregate(chatTopic, (current, c) => current.Replace(c, '_'));

                    Config = Configuration.LoadConfig("Configs\\" + fileName + ".config");
                    if (Config.ChatName.Length != 0) return;

                    Config.ChatName = chatTopic;
                    Config.Save();

                    return;
                }
            }
            catch (Exception)
            {
                
                
            }

            var chatName = msg.Chat.FriendlyName;
            if (string.IsNullOrWhiteSpace(chatName))
                return;

            fileName = System.IO.Path.GetInvalidFileNameChars().Aggregate(chatName, (current, c) => current.Replace(c, '_'));

            Config = Configuration.LoadConfig("Configs\\" + fileName + ".config");

            if (Config.ChatName.Length != 0) return;

            Config.ChatName = chatName;
            Config.Save();
        }

    }
}
