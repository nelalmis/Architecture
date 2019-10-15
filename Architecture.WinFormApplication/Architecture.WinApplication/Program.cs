using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace Architecture.WinApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //The Asphalt World
            //Money Twins
            //Glass Oceans
            //Stardust
            UserLookAndFeel.Default.SkinName = "Office 2007 Blue";
            SkinManager.EnableFormSkins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            Architecture.Helper.DllCollect.DllCopy();
            Application.Run(new ArchitectureWinForm());
        }
    }
}
