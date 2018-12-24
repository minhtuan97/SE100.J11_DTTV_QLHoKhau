﻿using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Dark Style");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD
            Application.Run(new HocSinhSinhVienGUI());
=======
            Application.Run(new AdminGUI());
>>>>>>> KienTrucMoi
        }
    }
}
