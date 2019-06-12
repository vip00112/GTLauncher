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
    public partial class CreatePageForm : Form
    {
        private List<string> _pageNames;

        #region Constructor
        private CreatePageForm()
        {
            InitializeComponent();
        }

        internal CreatePageForm(List<string> pageNames) : this()
        {
            _pageNames = pageNames;
        }
        #endregion

        #region Properties
        public string PageName { get; private set; }
        #endregion

        #region Control Event
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
        #endregion
    }
}
