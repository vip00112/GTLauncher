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
        private Point _loc;

        #region Constructor
        public CaptureBackgroundDialog(Point loc, Image backgroundImg)
        {
            InitializeComponent();

            DoubleBuffered = true;
            Size = backgroundImg.Size;
            BackgroundImage = backgroundImg;

            _loc = loc;
        }
        #endregion

        #region Properties
        public Image Image { get; private set; }
        #endregion

        #region Control Event
        private void CaptureBackgroundDialog_Load(object sender, EventArgs e)
        {
            Location = _loc;
        }

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
