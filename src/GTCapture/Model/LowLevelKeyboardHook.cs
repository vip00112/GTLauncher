using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public class LowLevelKeyboardHook
    {
        public event EventHandler<Keys> OnKeyPressed;
        public event EventHandler<Keys> OnKeyUnpressed;
        public delegate IntPtr CallBack(int nCode, IntPtr wParam, IntPtr lParam);

        private CallBack _callBack;
        private IntPtr _hookID;

        #region Constructor
        public LowLevelKeyboardHook()
        {
            _callBack = HookCallback;
            _hookID = IntPtr.Zero;
        }
        #endregion

        #region Properties
        public bool IsStared { get { return _hookID != IntPtr.Zero; } }
        #endregion

        #region Public Method
        public void Start()
        {
            if (IsStared) return;

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                IntPtr hWnd = WindowsAPI.GetModuleHandle(curModule.ModuleName);
                _hookID = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_KEYBOARD_LL, _callBack, hWnd, 0);
            }
        }

        public void Stop()
        {
            if (!IsStared) return;

            WindowsAPI.UnhookWindowsHookEx(_hookID);
            _hookID = IntPtr.Zero;
        }
        #endregion

        #region Private Method
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0) return WindowsAPI.CallNextHookEx(_hookID, nCode, wParam, lParam);

            int vkCode = Marshal.ReadInt32(lParam);
            if (IsPressed(wParam))
            {
                OnKeyPressed.Invoke(this, (Keys) vkCode);
            }
            else if (IsUnPressed(wParam))
            {
                OnKeyUnpressed.Invoke(this, (Keys) vkCode);
            }

            return WindowsAPI.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private bool IsPressed(IntPtr wParam)
        {
            return wParam == (IntPtr) WindowsAPI.WM_KEYDOWN || wParam == (IntPtr) WindowsAPI.WM_SYSKEYDOWN;
        }

        private bool IsUnPressed(IntPtr wParam)
        {
            return wParam == (IntPtr) WindowsAPI.WM_KEYUP || wParam == (IntPtr) WindowsAPI.WM_SYSKEYUP;
        }
        #endregion
    }
}