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
    public partial class LayoutSettingForm : Form
    {
        public LayoutSettingForm()
        {
            InitializeComponent();

            propertyGrid.SelectedObject = new Property()
            {
                SizeModeWidth = Setting.SizeModeWidth,
                SizeModeHeight = Setting.SizeModeHeight,
            };
        }

        private void LayoutSettingForm_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            MinimumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            switch (e.ChangedItem.Label)
            {
                case "SizeModeWidth":
                    panel_container.Width = Setting.GetWidth((SizeMode) e.ChangedItem.Value);
                    break;
                case "SizeModeHeight":
                    panel_container.Height = Setting.GetHeight((SizeMode) e.ChangedItem.Value);
                    break;
            }
        }

        public class Property
        {
            public SizeMode SizeModeWidth { get; set; }

            public SizeMode SizeModeHeight { get; set; }
        }
    }
}
