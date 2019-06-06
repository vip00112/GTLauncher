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
    public partial class CreatePageForm : Form
    {
        private List<string> _pageNames;

        private CreatePageForm()
        {
            InitializeComponent();
        }

        public CreatePageForm(List<string> pageNames) : this()
        {
            _pageNames = pageNames;
        }

        public string PageName { get; private set; }

        private void button_create_Click(object sender, EventArgs e)
        {
            string pageName = textBox_pageName.Text;
            if (string.IsNullOrWhiteSpace(pageName))
            {
                MessageBoxUtil.Error("You must input PageName.");
                return;
            }
            if (_pageNames.Contains(pageName))
            {
                MessageBoxUtil.Error("Inputed PageName is already exist.");
                return;
            }

            PageName = pageName;
            DialogResult = DialogResult.OK;
        }
    }
}
