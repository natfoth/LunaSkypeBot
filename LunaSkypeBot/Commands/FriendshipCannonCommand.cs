using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using SKYPE4COMLib;

namespace LunaSkypeBot.Commands
{
    public class FriendshipCannonCommand : CommandProcessor
    {
        public static bool IsFriendshipCannonRunning = false;

        public FriendshipCannonCommand(ref List<CommandProcessor> listToAttachTo) : base(ref listToAttachTo)
        {
        }

        public override List<string> Commands => new List<string>() { "OFC" };

        private ChatMessage _msg;

        public override async Task<bool> ProcessCommand(ChatMessage msg, string command)
        {
            if (IsFriendshipCannonRunning)
                return false;

            IsFriendshipCannonRunning = true;

            _msg = msg;

            await SendMessage("Orbital Friendship Cannon - Online");

            await Task.Delay(TimeSpan.FromMilliseconds(3000));

            await SendMessage("Acquiring Target...");

            await Task.Delay(TimeSpan.FromMilliseconds(2000));

            await SendMessage("Target Locked");

            await Task.Delay(TimeSpan.FromMilliseconds(2000));

            await SendMessage("Firing : " + "http://www.allmystery.de/i/t1ce2b1_FiringOrbitalFriendshipCannon.gif?bc");

            int asdf = 3;

            IsFriendshipCannonRunning = false;

            //Timer t = new Timer(AcquireTarget, 5, 0, 8000);

            return true;
        }

        public async Task SendMessage(string message)
        {
            _msg.Chat.SendMessage(Program.Form.Nickname + ": " + message);
        }

        private void AcquireTarget(Object state)
        {
            int bob = 1;
        }
    }
}
