using GTUtil;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class LayoutSetting
    {
        private const string SaveFileName = "Setting.Layout.json";

        private static List<Page> _pages;
        private static List<PageItem> _pageItems;

        #region Properties
        /// <summary>
        /// 모든 Control의 테마
        /// </summary>
        public static Theme Theme { get; set; }

        /// <summary>
        /// 런처 위치
        /// </summary>
        public static DockMode DockMode { get; set; }

        /// <summary>
        /// 런처 넓이
        /// </summary>
        public static SizeMode SizeModeWidth { get; set; }

        /// <summary>
        /// 런처 높이
        /// </summary>
        public static SizeMode SizeModeHeight { get; set; }

        /// <summary>
        /// 동적 생성한 페이지
        /// </summary>
        public static List<Page> Pages
        {
            get { return _pages; }
            set
            {
                if (_pages != null)
                {
                    foreach (var page in _pages)
                    {
                        var items = page.PageItems;
                        items.ForEach(o => o.Dispose());
                        page.Dispose();
                    }
                }
                _pages = value;
            }
        }

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

        public static bool IsEditMode { get; set; }
        #endregion

        #region Public Method
        public static Color GetBackColorCommon(Theme theme)
        {
            switch (theme)
            {
                case Theme.Dark:
                    return Color.FromArgb(0x12, 0x12, 0x12);
                case Theme.Light:
                    return Color.FromArgb(0xfa, 0xfa, 0xfa);
                default:
                    return GetBackColorCommon(Theme.Dark);
            }
        }

        public static Color GetBackColorHover(Theme theme)
        {
            switch (theme)
            {
                case Theme.Dark:
                    return Color.FromArgb(0xfa, 0xfa, 0xfa);
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
                    return Color.FromArgb(0xfa, 0xfa, 0xfa);
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
                    return Color.FromArgb(0xfa, 0xfa, 0xfa);
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
            try
            {
                var properties = new Dictionary<string, object>();
                properties.Add("Theme", Theme.ToString());
                properties.Add("DockMode", DockMode.ToString());
                properties.Add("SizeModeWidth", SizeModeWidth.ToString());
                properties.Add("SizeModeHeight", SizeModeHeight.ToString());

                var pageProperties = new List<Dictionary<string, object>>();
                foreach (var page in Pages)
                {
                    var props = new Dictionary<string, object>();
                    props.Add("PageName", page.PageName);
                    props.Add("Title", page.Title);
                    props.Add("VisibleTitle", page.VisibleTitle);
                    props.Add("VisibleBackButton", page.VisibleBackButton);
                    props.Add("CloseMode", page.CloseMode.ToString());
                    pageProperties.Add(props);
                }
                properties.Add("PageProperties", pageProperties);

                var pageItemProperties = new List<Dictionary<string, object>>();
                foreach (var pageItem in PageItems)
                {
                    var props = new Dictionary<string, object>();
                    props.Add("PageName", pageItem.PageName);
                    props.Add("BackgroundImage", ImageUtil.ToBase64(pageItem.BackgroundImage));
                    props.Add("TextContent", pageItem.TextContent);
                    props.Add("TextAlign", pageItem.TextAlign.ToString());

                    props.Add("TextFont_Name", pageItem.TextFont.Name);
                    props.Add("TextFont_Size", pageItem.TextFont.Size);
                    props.Add("TextFont_Bold", pageItem.TextFont.Bold);
                    props.Add("TextFont_Italic", pageItem.TextFont.Italic);
                    props.Add("TextFont_Strikeout", pageItem.TextFont.Strikeout);
                    props.Add("TextFont_Underline", pageItem.TextFont.Underline);

                    props.Add("ClickMode", pageItem.ClickMode.ToString());
                    props.Add("StartWithAdministrator", pageItem.StartWithAdministrator);
                    props.Add("FilePath", pageItem.FilePath);
                    props.Add("Arguments", pageItem.Arguments);
                    props.Add("LinkPageName", pageItem.LinkPageName);
                    props.Add("X", pageItem.X);
                    props.Add("Y", pageItem.Y);
                    props.Add("Width", pageItem.Width);
                    props.Add("Height", pageItem.Height);
                    pageItemProperties.Add(props);
                }
                properties.Add("PageItemProperties", pageItemProperties);

                string path = Path.Combine(Application.StartupPath, SaveFileName);
                string json = JsonUtil.FromProperties(properties);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                SetDefault();
                InvalidateAllForms();
            }
        }

        public static void Load()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, SaveFileName);
                if (!File.Exists(path))
                {
                    DockMode = DockMode.BottomCenter;
                    return;
                }

                string json = File.ReadAllText(path);
                var properties = JsonUtil.FromJson(json);
                Theme = JsonUtil.GetValue<Theme>(properties, "Theme");
                DockMode = JsonUtil.GetValue<DockMode>(properties, "DockMode");
                SizeModeWidth = JsonUtil.GetValue<SizeMode>(properties, "SizeModeWidth");
                SizeModeHeight = JsonUtil.GetValue<SizeMode>(properties, "SizeModeHeight");

                LoadPages(properties);
                LoadPageItems(properties);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                SetDefault();
                InvalidateAllForms();
            }
        }

        public static void InvalidateAllForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                Invalidate(form);
            }
        }

        public static void Invalidate(Control control)
        {
            if (control is Form)
            {
                var form = control as Form;
                bool isDarkMode = Theme == Theme.Dark;
                WinAPI.SetTitleBarTheme(form.Handle, isDarkMode);

                if (form.FormBorderStyle == FormBorderStyle.None)
                {
                    WinAPI.SetFormShadow(form.Handle);
                }
            }

            if (control is Button)
            {
                var btn = control as Button;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, GetBackColorHover(Theme));
            }

            if (ToolStripManager.Renderer is ThemeToolStripRenderer == false)
            {
                ToolStripManager.Renderer = new ThemeToolStripRenderer();
            }

            var backColor = (control is Label) ? Color.Transparent : GetBackColorCommon(Theme);
            control.BackColor = backColor;
            control.ForeColor = GetForeColorCommon(Theme);
            control.Invalidate();

            foreach (Control child in control.Controls)
            {
                Invalidate(child);
            }
        }
        #endregion

        #region Private Method
        private static void LoadPages(Dictionary<string, object> properties)
        {

            if (!properties.ContainsKey("PageProperties")) return;

            var pageProperties = JsonUtil.FromJArray(properties["PageProperties"]);
            if (pageProperties == null && pageProperties.Count == 0) return;

            Pages = new List<Page>();
            foreach (var props in pageProperties)
            {
                string pageName = JsonUtil.GetValue<string>(props, "PageName");
                var page = new Page(pageName);
                page.Title = JsonUtil.GetValue<string>(props, "Title");
                page.VisibleTitle = JsonUtil.GetValue<bool>(props, "VisibleTitle");
                page.VisibleBackButton = JsonUtil.GetValue<bool>(props, "VisibleBackButton");
                page.CloseMode = JsonUtil.GetValue<PageCloseMode>(props, "CloseMode");

                Pages.Add(page);
            }
        }

        private static void LoadPageItems(Dictionary<string, object> properties)
        {
            if (!properties.ContainsKey("PageItemProperties")) return;

            var pageItemProperties = JsonUtil.FromJArray(properties["PageItemProperties"]);
            if (pageItemProperties == null && pageItemProperties.Count == 0) return;

            PageItems = new List<PageItem>();
            foreach (var props in pageItemProperties)
            {
                var item = new PageItem();
                item.PageName = JsonUtil.GetValue<string>(props, "PageName");
                item.BackgroundImage = ImageUtil.FromBase64(JsonUtil.GetValue<string>(props, "BackgroundImage"));
                item.TextContent = JsonUtil.GetValue<string>(props, "TextContent");
                item.TextAlign = JsonUtil.GetValue<ContentAlignment>(props, "TextAlign");

                string fontName = JsonUtil.GetValue<string>(props, "TextFont_Name");
                int size = (int) JsonUtil.GetValue<double>(props, "TextFont_Size");
                bool bold = JsonUtil.GetValue<bool>(props, "TextFont_Bold");
                bool italic = JsonUtil.GetValue<bool>(props, "TextFont_Italic");
                bool strikeout = JsonUtil.GetValue<bool>(props, "TextFont_Strikeout");
                bool underline = JsonUtil.GetValue<bool>(props, "TextFont_Underline");

                FontStyle style = FontStyle.Regular;
                if (bold) style = FontStyle.Bold;
                if (italic) style |= FontStyle.Italic;
                if (strikeout) style |= FontStyle.Strikeout;
                if (underline) style |= FontStyle.Underline;
                Font font = new Font(fontName, size, style);
                item.TextFont = font;

                item.ClickMode = JsonUtil.GetValue<ClickMode>(props, "ClickMode");
                item.StartWithAdministrator = JsonUtil.GetValue<bool>(props, "StartWithAdministrator");
                item.FilePath = JsonUtil.GetValue<string>(props, "FilePath");
                item.Arguments = JsonUtil.GetValue<string>(props, "Arguments");
                item.LinkPageName = JsonUtil.GetValue<string>(props, "LinkPageName");
                item.X = (int) JsonUtil.GetValue<long>(props, "X");
                item.Y = (int) JsonUtil.GetValue<long>(props, "Y");
                item.Width = (int) JsonUtil.GetValue<long>(props, "Width");
                item.Height = (int) JsonUtil.GetValue<long>(props, "Height");

                PageItems.Add(item);
            }
        }

        private static void SetDefault()
        {
            if (Pages == null)
            {
                Pages = new List<Page>();
                Pages.Add(new Page("Main"));
            }
            if (PageItems == null)
            {
                PageItems = new List<PageItem>();
            }
        }
        #endregion
    }
}
