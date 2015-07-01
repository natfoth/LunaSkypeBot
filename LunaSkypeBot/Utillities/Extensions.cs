using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using SKYPE4COMLib;

namespace LunaSkypeBot.Utillities
{
    public static class Extensions
    {
        public static class ThreadSafeRandom
        {
            [ThreadStatic]
            private static Random Local;

            public static Random ThisThreadsRandom
            {
                get
                {
                    return Local ??
                           (Local =
                               new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));
                }
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static ChatMessage SendBotMessage(this ChatMessage msg, string messageToSend)
        {
            return msg.Chat.SendMessage(Program.Form.Nickname + " : " + messageToSend);
        }

        public static void SendBotMessage(this Chat msg, string messageToSend)
        {
            msg.SendMessage(Program.Form.Nickname + " : " + messageToSend);
        }

        public static void EditMessage(this ChatMessage msg, string messageToSend)
        {
            msg.Body = Program.Form.Nickname + " : " + messageToSend;
        }

        public static string SubStringTillChar(this string s, string subChar)
        {
            int l = s.IndexOf(subChar, StringComparison.Ordinal);

            if (l > 0)
            {
                return s.Substring(0, l);
            }
            return s;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
