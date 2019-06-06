using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public static class Setting
    {
        private static List<PageItem> _pageItems;

        #region Properties
        /// <summary>
        /// 런처 Window의 이동 가능 여부
        /// </summary>
        public static bool CanMove { get; set; }

        /// <summary>
        /// 모든 Control의 테마
        /// </summary>
        public static Theme Theme { get; set; }

        /// <summary>
        /// 런처 넓이
        /// </summary>
        public static SizeMode SizeModeWidth { get; set; }

        /// <summary>
        /// 런처 높이
        /// </summary>
        public static SizeMode SizeModeHeight { get; set; }

        /// <summary>
        /// 동적 생성한 아이템
        /// </summary>
        public static List<PageItem> PageItems
        {
            get { return _pageItems; }
            set
            {
                if (_pageItems != null)
                {
                    _pageItems.ForEach(o => o.Dispose());
                }
                _pageItems = value;
            }
        }
        #endregion

        #region Public Method
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

        public static void Save()
        {
            var properties = new Dictionary<string, object>();
            properties.Add("CanMove", CanMove);
            properties.Add("Theme", Theme);
            properties.Add("SizeModeWidth", SizeModeWidth);
            properties.Add("SizeModeHeight", SizeModeHeight);
            if (PageItems != null)
            {
                var pageItemProperties = new List<Dictionary<string, object>>();
                foreach (var pageItem in PageItems)
                {
                    var itemProperties = new Dictionary<string, object>();
                    itemProperties.Add("BackgroundImage", ToBase64(pageItem.BackgroundImage));
                    itemProperties.Add("TextContent", pageItem.TextContent);
                    itemProperties.Add("TextAlign", pageItem.TextAlign);

                    itemProperties.Add("TextFont_Name", pageItem.TextFont.Name);
                    itemProperties.Add("TextFont_Size", pageItem.TextFont.Size);
                    itemProperties.Add("TextFont_Bold", pageItem.TextFont.Bold);
                    itemProperties.Add("TextFont_Italic", pageItem.TextFont.Italic);
                    itemProperties.Add("TextFont_Strikeout", pageItem.TextFont.Strikeout);
                    itemProperties.Add("TextFont_Underline", pageItem.TextFont.Underline);

                    itemProperties.Add("ClickMode", pageItem.ClickMode);
                    itemProperties.Add("FilePath", pageItem.FilePath);
                    itemProperties.Add("Arguments", pageItem.Arguments);
                    itemProperties.Add("Column", pageItem.Column);
                    itemProperties.Add("Row", pageItem.Row);
                    itemProperties.Add("ColumnSpan", pageItem.ColumnSpan);
                    itemProperties.Add("RowSpan", pageItem.RowSpan);
                    pageItemProperties.Add(itemProperties);
                }
                properties.Add("pageItemProperties", pageItemProperties);
            }

            string json = JsonConvert.SerializeObject(properties, Formatting.Indented);
            File.WriteAllText("Setting.json", json);

            Invalidate();
        }

        public static void Load()
        {
            if (!File.Exists("Setting.json")) return;

            string json = File.ReadAllText("Setting.json");
            var properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            if (properties.ContainsKey("pageItemProperties"))
            {
                var array = properties["pageItemProperties"] as JArray;
                if (array != null)
                {
                    var pageItemProperties = array.ToObject<List<Dictionary<string, object>>>();
                }
            }
        }
        #endregion

        #region Private Method
        private static void Invalidate()
        {
            foreach (Form form in Application.OpenForms)
            {
                SetTheme(form, Theme);
            }
        }

        private static string ToBase64(Image img)
        {
            if (img == null) return null;

            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private static Image FromBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) return null;

            using (var ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                return Image.FromStream(ms);
            }
        }
        #endregion
    }
}
