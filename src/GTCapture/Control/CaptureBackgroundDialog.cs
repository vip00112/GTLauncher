using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public partial class CaptureBackgroundDialog : Form
    {
        #region Constructor
        public CaptureBackgroundDialog(Image img)
        {
            InitializeComponent();

            DoubleBuffered = true;
            Size = img.Size;
            BackgroundImage = img;
        }
        #endregion

        #region Properties
        public Image Image { get; private set; }
        #endregion

        #region Control Event
        private void CaptureBackgroundDialog_Shown(object sender, EventArgs e)
        {
            using (var dialog = new CaptureRegionDialog())
            {
                dialog.TopMost = true;
                dialog.BringToFront();
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                var region = dialog.SelectedRegion;
                Image = CropImage(BackgroundImage, region);
                DialogResult = DialogResult.OK;
            }
        }

        private void CaptureBackgroundDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BackgroundImage != null) BackgroundImage.Dispose();
        }
        #endregion

        #region Private Method
        private Bitmap CropImage(Image src, Rectangle region)
        {
            var bitmap = src as Bitmap;
            return bitmap.Clone(region, bitmap.PixelFormat);
        }
        #endregion
    }
}
