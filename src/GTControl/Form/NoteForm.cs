using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public partial class NoteForm : Form
    {
        private NoteForm()
        {
            InitializeComponent();
        }

        public NoteForm(string title, string content, int width, int height) : this()
        {
            Text = title;
            richTextBox_content.Text = content;
            Width = width;
            Height = height;
        }
    }
}
