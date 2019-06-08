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
        private IntPtr _hWnd;
        private Dictionary<CaptureMode, HotKey> _hotKeys;

        #region Constructor
        public Capture(IntPtr hWnd)
        {
            _hWnd = hWnd;

            _hotKeys = new Dictionary<CaptureMode, HotKey>();
            foreach (CaptureMode mode in Enum.GetValues(typeof(CaptureMode)))
            {
                _hotKeys.Add(mode, new HotKey());
            }
        }
        #endregion

        #region Protected Method
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            var modifier = (KeyModifiers) ((int) m.LParam & 0xFFFF);
            var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);
            CaptureMode mode = GetCaptureMode(modifier, key);

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
        public void OnWndProc(ref Message m)
        {
            var modifier = (KeyModifiers) ((int) m.LParam & 0xFFFF);
            var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);
            CaptureMode mode = GetCaptureMode(modifier, key);

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

        public bool IsExistHotKey(KeyModifiers modifiers, Keys key)
        {
            return _hotKeys.Values.Any(o => o.Modifier == modifiers && o.Key == key);
        }

        public void RegisterHotKey(CaptureMode mode, KeyModifiers modifiers, Keys key)
        {
            var hotKey = _hotKeys[mode];
            if (hotKey == null) return;

            if (hotKey.IsRegistered)
            {
                UnregisterHotKey(mode);
            }

            hotKey.Modifier = modifiers;
            hotKey.Key = key;
            if (WindowsAPI.RegisterHotKey(_hWnd, (int) mode, hotKey.Modifier, hotKey.Key))
            {
                hotKey.IsRegistered = true;
            }
        }

        public void UnregisterHotKey(CaptureMode mode)
        {
            var hotKey = _hotKeys[mode];
            if (hotKey == null) return;

            if (WindowsAPI.UnregisterHotKey(_hWnd, (int) mode))
            {
                hotKey.IsRegistered = false;
            }
        }
        #endregion

        #region Private Method
        private CaptureMode GetCaptureMode(KeyModifiers modifiers, Keys key)
        {
            foreach (var id in _hotKeys.Keys)
            {
                var hotKey = _hotKeys[id];
                if (hotKey.Modifier == modifiers && hotKey.Key == key)
                {
                    return id;
                }
            }
            return CaptureMode.None;
        }
        #endregion
    }
}