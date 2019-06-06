using GTControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTLauncher
{
    public partial class MainForm : PageContainer
    {
        public MainForm()
        {
            InitializeComponent();

            ContextMenu ctx = new ContextMenu();
            ctx.MenuItems.Add(new MenuItem("종료", new EventHandler((s, e) => Close())));
            notifyIcon.ContextMenu = ctx;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Activate();
        }
    }
}
