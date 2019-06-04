using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public static class Setting
    {
        private static Theme _theme;

        /// <summary>
        /// 런처 Window의 이동 가능 여부
        /// </summary>
        public static bool CanMove { get; set; }

        /// <summary>
        /// 모든 Control의 테마
        /// </summary>
        public static Theme Theme
        {
            get { return _theme; }
            set
            {
                bool changed = _theme != value;
                _theme = value;

                // 기존 테마에서 변경시 Update
                if (changed)
                {
                    ChangeTheme(_theme);
                }
            }
        }

        /// <summary>
        /// 런처 넓이
        /// </summary>
        public static SizeMode SizeModeWidth { get; set; }

        /// <summary>
        /// 런처 높이
        /// </summary>
        public static SizeMode SizeModeHeight { get; set; }

        #region Public Method
        public static void ChangeTheme(Theme theme)
        {
            foreach (Form form in Application.OpenForms)
            {
                SetTheme(form, theme);
            }
        }

        public static void SetTheme(Control control, Theme theme)
        {
            if (control is Label)
            {
                control.BackColor = Color.Transparent;
            }
            else
            {
                control.BackColor = GetBackColorCommon(theme);
            }
            control.ForeColor = GetForeColorCommon(theme);

            foreach (Control child in control.Controls)
            {
                SetTheme(child, theme);
            }
        }

        public static Color GetBackColorCommon(Theme theme)
        {
            switch (theme)
            {
                case Theme.Dark:
                    return Color.FromArgb(0x12, 0x12, 0x12);
                case Theme.Light:
                    return Color.FromArgb(255, 255, 255);
                default:
                    return GetBackColorCommon(Theme.Dark);
            }
        }

        public static Color GetBackColorHover(Theme theme)
        {
            switch (theme)
            {
                case Theme.Dark:
                    return Color.FromArgb(255, 255, 255);
                case Theme.Light:
                    return Color.FromArgb(0x12, 0x12, 0x12);
                default:
                    return GetBackColorHover(Theme.Dark);
            }
        }

        public static Color GetForeColorCommon(Theme theme)
        {
            switch (theme)
            {
                case Theme.Dark:
                    return Color.FromArgb(255, 255, 255);
                case Theme.Light:
                    return Color.FromArgb(0x12, 0x12, 0x12);
                default:
                    return GetForeColorCommon(Theme.Dark);
            }
        }

        public static Color GetForeColorHover(Theme theme)
        {
            switch (theme)
            {
                case Theme.Dark:
                    return Color.FromArgb(0x12, 0x12, 0x12);
                case Theme.Light:
                    return Color.FromArgb(255, 255, 255);
                default:
                    return GetForeColorHover(Theme.Dark);
            }
        }

        public static int GetWidth(SizeMode sizeMode)
        {
            switch (sizeMode)
            {
                case SizeMode.Small:
                    return 400;
                case SizeMode.Medium:
                    return 800;
                case SizeMode.Large:
                    return 1200;
                case SizeMode.XLarge:
                    return 1600;
                default:
                    return GetWidth(SizeMode.Medium);
            }
        }

        public static int GetHeight(SizeMode sizeMode)
        {
            switch (sizeMode)
            {
                case SizeMode.Small:
                    return 200;
                case SizeMode.Medium:
                    return 400;
                case SizeMode.Large:
                    return 600;
                case SizeMode.XLarge:
                    return 800;
                default:
                    return GetHeight(SizeMode.Medium);
            }
        }
        #endregion
    }
}
