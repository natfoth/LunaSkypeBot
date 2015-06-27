using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SKYPE4COMLib;
using Application = System.Windows.Forms.Application;

namespace LunaSkypeBot
{
    static class Program
    {
        public static Form1 Form { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form = new Form1();
            Application.Run(Form);

            
        }
    }
}
