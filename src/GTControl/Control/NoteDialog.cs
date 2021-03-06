﻿using System;
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
    public partial class NoteDialog : Form
    {
        private NoteDialog()
        {
            InitializeComponent();
        }

        internal NoteDialog(string title, string content, int width, int height) : this()
        {
            Text = title;
            richTextBox_content.Text = content;
            Width = width;
            Height = height;
        }

        private void NoteDialog_Load(object sender, EventArgs e)
        {
            LayoutSetting.Invalidate(this);
        }

        private void NoteDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) DialogResult = DialogResult.Cancel;
        }
    }
}