using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SteamStatus.Net.Properties;
using System.Linq;
using Timer = System.Threading.Timer;

namespace SteamStatus.Net
{
    public partial class TrayIcon : Form
    {
        protected NotifyIcon notifyIcon = new NotifyIcon();
        protected Client client = new Client();
        protected Dictionary<string, MenuItem> menuItems;
        protected Timer Timer;
        protected Json json;

        private async void Update(object state)
        {
            var data = await client.GetAsync();
            if (json != null && json.Online < 75 && data.Online >= 75)
            {
                notifyIcon.ShowBalloonTip(5000, Resources.SteamIsBackOnline, string.Format(Resources.SteamOnlineText, data.Online), ToolTipIcon.Info);
            }

            json = data;
            Rebuild();
        }

        private void Rebuild()
        {
            var menuItems = notifyIcon.ContextMenu.MenuItems;
            menuItems.Clear();

            if (json != null)
            {
                /* STEAM */
                var steam = new List<MenuItem>
                {
                    new MenuItem(string.Format(format: Resources.ItemTemplate,
                        arg0: Resources.SteamOnline,
                        arg1: json.Services.Where(p => (string) p[0] == "cms").Select(p => p[2]).FirstOrDefault())),
                    new MenuItem(String.Format(Resources.ItemTemplate,
                        Resources.CommunityOnline,
                        json.Services.Where(p => (string) p[0] == "community").Select(p => p[2]).FirstOrDefault())),
                    new MenuItem(String.Format(Resources.ItemTemplate,
                        Resources.WebApi,
                        json.Services.Where(p => (string) p[0] == "webapi").Select(p => p[2]).FirstOrDefault())),
                    new MenuItem(String.Format(Resources.ItemTemplate,
                        Resources.Store,
                        json.Services.Where(p => (string) p[0] == "store").Select(p => p[2]).FirstOrDefault())),
                    new MenuItem(String.Format(Resources.ItemTemplate,
                        Resources.Online,
                        json.Services.Where(p => (string) p[0] == "online").Select(p => p[2]).FirstOrDefault()))
                };
                menuItems.Add(Resources.Steam, steam.ToArray());


                /* CS:GO */
                var csgo = new List<MenuItem>
                {
                    new MenuItem(String.Format(Resources.ItemTemplate,
                        Resources.CSGO,
                        json.Services.Where(p => (string) p[0] == "csgo").Select(p => p[2]).FirstOrDefault())),
                    new MenuItem(String.Format(Resources.ItemTemplate,
                        Resources.CSGO_Inventory,
                        json.Services.Where(p => (string) p[0] == "csgo_community").Select(p => p[2]).FirstOrDefault())),
                    new MenuItem(String.Format(Resources.ItemTemplate,
                        Resources.CSGO_MM,
                        json.Services.Where(p => (string) p[0] == "csgo_mm_scheduler").Select(p => p[2]).FirstOrDefault())),
                    new MenuItem(String.Format(Resources.ItemTemplate,
                        Resources.CSGO_Sessions,
                        json.Services.Where(p => (string) p[0] == "csgo_sessions").Select(p => p[2]).FirstOrDefault()))
                };
                menuItems.Add(Resources.CSGO, csgo.ToArray());

                /* TO-DO: regions */

                var dict = json.ServicesDictionary();
                foreach (var dictItem in dict)
                {
                    Debugger.Log(255, "ServiceStatus", $"{dictItem.Key}: {dictItem.Value}\n");
                }
            }

            menuItems.Add("-");
            menuItems.Add(Resources.TrayIconExit, OnExit);
        }

        public TrayIcon()
        {
            var trayMenu = new ContextMenu();

            notifyIcon.Text = Resources.TrayIconText;
            notifyIcon.Icon = new Icon(Program.ApplicationIcon, 40, 40);
            notifyIcon.ContextMenu = trayMenu;
            notifyIcon.Visible = true;

            Rebuild();

            Timer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromSeconds(45));
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                notifyIcon.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
