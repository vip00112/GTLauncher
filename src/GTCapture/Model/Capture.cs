using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public class Capture : NativeWindow
    {
        #region Constructor
        public Capture(IntPtr hWnd)
        {
            // WndProc 이벤트가 발생하도록 핸들 등록
            AssignHandle(hWnd);

            Setting.Handle = hWnd;
            Setting.Load();
        }
        #endregion

        #region Protected Method
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            var modifier = (KeyModifiers) ((int) m.LParam & 0xFFFF);
            var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);

            CaptureMode mode = Setting.GetCaptureMode(modifier, key);
            if (mode == CaptureMode.None) return;

            switch (mode)
            {
                case CaptureMode.FullScreen:
                    // TODO : 전체 화면 캡쳐
                    System.Diagnostics.Process.Start("notepad.exe");
                    break;
                case CaptureMode.ActiveProcess:
                    // TODO : 활성화 프로세스 화면 캡쳐
                    break;
                case CaptureMode.Region:
                    // TODO : 선택 영역 캡쳐
                    break;
            }
        }
        #endregion

        #region Public Method
        public void ShowSettingForm()
        {
            using (var dialog = new SettingForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
            }
        }
        #endregion
    }
}