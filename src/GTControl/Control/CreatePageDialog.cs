﻿using GTLocalization;
using GTUtil;
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
    public partial class CreatePageDialog : Form
    {
        private List<string> _pageNames;

        #region Constructor
        private CreatePageDialog()
        {
            InitializeComponent();
        }

        internal CreatePageDialog(List<string> pageNames) : this()
        {
            _pageNames = pageNames;
        }
        #endregion

        #region Properties
        public string PageName { get; private set; }
        #endregion

        #region Control Event
        private void CreatePageDialog_Load(object sender, EventArgs e)
        {
            LayoutSetting.Invalidate(this);
        }

        private void CreatePageDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) DialogResult = DialogResult.Cancel;
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            string pageName = textBox_pageName.Text;
            if (string.IsNullOrWhiteSpace(pageName))
            {
                MessageBoxUtil.Error(Resource.GetString(Key.PageNameInputErrorMsg));
                return;
            }
            if (_pageNames.Contains(pageName))
            {
                MessageBoxUtil.Error(Resource.GetString(Key.PageNameAlreadyErrorMsg));
                return;
            }

            PageName = pageName;
            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
