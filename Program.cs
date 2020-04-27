using System;
using System.Drawing;
using System.Windows.Forms;

namespace SteamStatus.Net
{
    static class Program
    {
        public static Icon ApplicationIcon => new Icon("../../../icon.ico");

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayIcon());
        }
    }
}
