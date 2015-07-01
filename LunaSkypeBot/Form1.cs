using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LunaSkypeBot.Commands;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Database;
using LunaSkypeBot.Pulses;
using Nito.AsyncEx;
using SKYPE4COMLib;

namespace LunaSkypeBot
{
    public delegate void PulseDelegate();

    public partial class Form1 : Form
    {
        public Skype skype;
        private const string trigger = "~"; // Say !help
        private LunaRandom _random;

        public List<string> ListOfAllQuotes = new List<string>(); 
        public List<CommandProcessor> CommandProcessors = new List<CommandProcessor>();

        public event PulseDelegate Pulsed;

        public PulseDelegate Pulser = DelegateMethod;

        private System.Timers.Timer _pulse;

        public string Nickname
        {
            get
            {
                if (ConfigManager.Config == null || ConfigManager.Config.SFWOnly)
                    return "Luna Natwork";

                return "Luna Natwork Afterdark";
            }
        }

        public Form1()
        {
            InitializeComponent();

            _random = new LunaRandom();

            skype = new Skype();
            if (!skype.Client.IsRunning)
            {
                // start minimized with no splash screen
                skype.Client.Start(true, true);
            }

            // wait for the client to be connected and ready
            skype.Attach(8, true);

            skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);


            var files = Directory.GetFiles("Quotes", "*", SearchOption.AllDirectories).ToList();
            if (files.Count != 0)
            {
                foreach (var file in files)
                {
                    var strings = File.ReadAllLines(file);

                    var convertedStrings = new List<string>();
                    foreach (var s in strings)
                    {
                        convertedStrings.Add(s + " - " + Path.GetFileNameWithoutExtension(file));
                    }
                    ListOfAllQuotes.AddRange(convertedStrings);
                }
                
            }

            SetupCommandProcessors();

            _pulse = new System.Timers.Timer();
            _pulse.Elapsed += Pulse;
            _pulse.Interval = 1000; // in miliseconds
            _pulse.Start();

        }

        private void Pulse(object sender, EventArgs e)
        {
            _pulse.Stop();
            _pulse.Start();

            Pulsed?.Invoke();
        }

        public static void DelegateMethod()
        {
            int bob = 1;
        }

        private void SetupCommandProcessors()
        {
            new AddGlobalAdminCommand(ref CommandProcessors);
            new RemoveGlobalAdminCommand(ref CommandProcessors);
            new QuoteProcessor(ref CommandProcessors);
            new FriendshipCannonCommand(ref CommandProcessors);
            new LunaCommand(ref CommandProcessors);
            new CelestiaCommand(ref CommandProcessors);
            new SettingsCommand(ref CommandProcessors);
            new AddQuoteCommand(ref CommandProcessors);
            new SetPermissionsCommand(ref CommandProcessors);
            new MisCommands(ref CommandProcessors);
            new RandomCommand(ref CommandProcessors);
            new SearchCommand(ref CommandProcessors);
            new LunaOfTheDayCommand(ref CommandProcessors);


            //pulsers
            new LunaOfTheDay(this);
            

            int bob = 1;
        }




        private void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
            if (true)
            {
                Debug.WriteLine(string.Format("{0} : {1} - {2}", msg.Sender.FullName, msg.Sender.Handle, status));
            }

            if (msg.Body.IndexOf(trigger, StringComparison.Ordinal) != 0 ||
                TChatMessageStatus.cmsSending == status || TChatMessageStatus.cmsRead == status)
                return;


            AsyncContext.Run(() => ProcessCommands(msg));


            /* if (command.ToLower().StartsWith("sfwonly"))
            {
                handleSfwCommand(msg, command);
                return;
            }


            var processed = ProcessCommand(msg, command);

            if (processed.Length != 0)
                msg.Chat.SendMessage(Nickname + ": " + processed);*/
        }

        private async Task ProcessCommands(ChatMessage msg)
        {
            // Remove trigger string and make lower case
            string command = msg.Body.Remove(0, trigger.Length);

            ConfigManager.LoadChatConfiguration(msg, command);

            foreach (var commandProcessor in CommandProcessors.Where(commandProcessor => commandProcessor.AcceptCommand(msg, command)))
            {
                if (await commandProcessor.ProcessCommand(msg, command))
                    return;
            }

            return;
        }

        

        

        

        private string GetRandomTwilightPic()
        {

            var files = Directory.GetFiles("G:\\Dropbox\\Public\\Pics\\pony\\Twilight\\").ToList();
            if (files.Count == 0)
                return "";

            var file = files[_random.Next(files.Count)];
            var fileName = Path.GetFileName(file);

            var url = "https://dl.dropboxusercontent.com/u/69140190/Pics/pony/Twilight/" + fileName;


            return url;
        }

       /* private string GetNatfothQuote()
        {
            if (SafeForWorkOnly)
                return "The Guardian of the Night";

            return "The Guardian of Moongazing";
        }*/



        

        private string ProcessCommand(ChatMessage msg, string str)
        {
            string result;
            switch (str)
            {
                case "currentconfig":
                    result = ConfigManager.Config.ChatName;
                    break;
                case "chattitle":
                    result = msg.Chat.Topic;
                    break;
                case "bestpony":
                    result = "https://dl.dropboxusercontent.com/u/69140190/Pics/pony/Luna/880755__safe_princess%2Bluna_text_logo_best%2Bpony_sparkles_artist-colon-jamescorck_mlp%2Blogo_truth.png";
                    break;
                case "rattletrap":
                    result = "Testing, Testing, 1,2,3";
                    break;
                case "dateme":
                    result = "I'm afraid that I'm reserving myself for a special somepony.";
                    break;
                case "twilight":
                    result =
                        GetRandomTwilightPic();
                    break;
                case "moon":
                    result =
                        "(moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) (moon) ";
                    break;
                case "storm":
                    result = "Quick! Hide in the barn!";
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        }

        private async Task<string> GetURLResponse(string url)
        {
            return await GetURLResponse(new Uri(url));
        }

        private async Task<string> GetURLResponse(Uri url)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept",
                "text/html,application/xhtml+xml,application/xml");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            var response = await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(decompressedStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
