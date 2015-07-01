using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LunaSkypeBot.Database.Entity;
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
            DoMigration();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form = new Form1();
            Application.Run(Form);

            
        }


        private static void DoMigration()
        {
           // var configuration = new Migrations.Configuration();
          //  var migrator = new DbMigrator(configuration);

         //   migrator.Update();
        }
    }
}
