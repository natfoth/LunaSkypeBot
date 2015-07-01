using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaSkypeBot.Utillities
{
    public static class CommandHandler
    {
        public static string GetCommand(string command)
        {
            return command.Trim().SubStringTillChar(" ");
        }

        public static string GetParameter(string command, string body)
        {
            return command.Trim().SubStringTillChar(" ");
        }
    }
}
