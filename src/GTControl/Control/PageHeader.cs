using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class PageHeader : Panel
    {
        #region Constructor
        public PageHeader()
        {
            DoubleBuffered = true;

            Padding = new Padding(0);
            Margin = new Padding(0);
            Dock = DockStyle.Top;
            Height = 30;
        }
        #endregion
    }
}
