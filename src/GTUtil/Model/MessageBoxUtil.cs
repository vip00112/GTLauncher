using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTUtil
{
    public static class MessageBoxUtil
    {
        public static void Info(string msg, string title = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "GTLauncher";
            }
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Error(string msg, string title = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "GTLauncher";
            }
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirm(string msg, string title = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "GTLauncher";
            }
            var result = MessageBox.Show(msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            return result == DialogResult.OK;
        }

        public static DialogResult ConfirmYesNoCancel(string msg, string title = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "GTLauncher";
            }
            var result = MessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            return result;
        }

    }
}
