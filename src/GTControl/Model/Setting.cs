﻿using Newtonsoft.Json;
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
        private static List<Page> _pages;
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
        /// 동적 생성한 페이지
        /// </summary>
        public static List<Page> Pages
        {
            get { return _pages; }
            set
            {
                if (_pages != null)
                {
                    _pages.ForEach(o => o.Dispose());
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
            try
            {
                var properties = new Dictionary<string, object>();
                properties.Add("CanMove", CanMove);
                properties.Add("Theme", Theme.ToString());
                properties.Add("SizeModeWidth", SizeModeWidth.ToString());
                properties.Add("SizeModeHeight", SizeModeHeight.ToString());

                var pageProperties = new List<Dictionary<string, object>>();
                foreach (var page in Pages)
                {
                    var props = new Dictionary<string, object>();
                    props.Add("PageName", page.PageName);
                    props.Add("VisibleHeader", page.VisibleHeader);
                    props.Add("VisibleBackButton", page.VisibleBackButton);
                    props.Add("VisibleOptionButton", page.VisibleOptionButton);
                    props.Add("CloseMode", page.CloseMode.ToString());
                    pageProperties.Add(props);
                }
                properties.Add("PageProperties", pageProperties);

                var pageItemProperties = new List<Dictionary<string, object>>();
                foreach (var pageItem in PageItems)
                {
                    var props = new Dictionary<string, object>();
                    props.Add("PageName", pageItem.PageName);
                    props.Add("BackgroundImage", ToBase64(pageItem.BackgroundImage));
                    props.Add("TextContent", pageItem.TextContent);
                    props.Add("TextAlign", pageItem.TextAlign.ToString());

                    props.Add("TextFont_Name", pageItem.TextFont.Name);
                    props.Add("TextFont_Size", pageItem.TextFont.Size);
                    props.Add("TextFont_Bold", pageItem.TextFont.Bold);
                    props.Add("TextFont_Italic", pageItem.TextFont.Italic);
                    props.Add("TextFont_Strikeout", pageItem.TextFont.Strikeout);
                    props.Add("TextFont_Underline", pageItem.TextFont.Underline);

                    props.Add("ClickMode", pageItem.ClickMode.ToString());
                    props.Add("FilePath", pageItem.FilePath);
                    props.Add("Arguments", pageItem.Arguments);
                    props.Add("Column", pageItem.Column);
                    props.Add("Row", pageItem.Row);
                    props.Add("ColumnSpan", pageItem.ColumnSpan);
                    props.Add("RowSpan", pageItem.RowSpan);
                    pageItemProperties.Add(props);
                }
                properties.Add("PageItemProperties", pageItemProperties);

                string json = JsonConvert.SerializeObject(properties, Formatting.Indented);
                File.WriteAllText("Setting.json", json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Invalidate();
            }
        }

        public static void Load()
        {
            try
            {
                if (!File.Exists("Setting.json")) return;

                string json = File.ReadAllText("Setting.json");
                var properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                CanMove = (bool) properties["CanMove"];
                Theme = (Theme) Enum.Parse(typeof(Theme), properties["Theme"] as string);
                SizeModeWidth = (SizeMode) Enum.Parse(typeof(SizeMode), properties["SizeModeWidth"] as string);
                SizeModeHeight = (SizeMode) Enum.Parse(typeof(SizeMode), properties["SizeModeHeight"] as string);

                LoadPages(properties);
                LoadPageItems(properties);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Invalidate();
            }
        }
        #endregion

        #region Private Method
        private static void LoadPages(Dictionary<string, object> properties)
        {

            if (!properties.ContainsKey("PageProperties")) return;

            var array = properties["PageProperties"] as JArray;
            if (array == null) return;

            var pageProperties = array.ToObject<List<Dictionary<string, object>>>();
            if (pageProperties == null && pageProperties.Count == 0) return;

            Pages = new List<Page>();
            foreach (var props in pageProperties)
            {
                var page = new Page();
                page.PageName = props["PageName"] as string;
                page.VisibleHeader = (bool) props["VisibleHeader"];
                page.VisibleBackButton = (bool) props["VisibleBackButton"];
                page.VisibleOptionButton = (bool) props["VisibleOptionButton"];
                page.CloseMode = (PageCloseMode) Enum.Parse(typeof(PageCloseMode), props["CloseMode"] as string);

                Pages.Add(page);
            }
        }

        private static void LoadPageItems(Dictionary<string, object> properties)
        {
            if (!properties.ContainsKey("PageItemProperties")) return;

            var array = properties["PageItemProperties"] as JArray;
            if (array == null) return;

            var pageItemProperties = array.ToObject<List<Dictionary<string, object>>>();
            if (pageItemProperties == null && pageItemProperties.Count == 0) return;

            PageItems = new List<PageItem>();
            foreach (var props in pageItemProperties)
            {
                var item = new PageItem();
                item.PageName = props["PageName"] as string;
                item.BackgroundImage = FromBase64(props["BackgroundImage"] as string);
                item.TextContent = props["TextContent"] as string;
                item.TextAlign = (ContentAlignment) Enum.Parse(typeof(ContentAlignment), props["TextAlign"] as string);

                string fontName = props["TextFont_Name"] as string;
                int size = (int) (double) props["TextFont_Size"];
                bool bold = (bool) props["TextFont_Bold"];
                bool italic = (bool) props["TextFont_Italic"];
                bool strikeout = (bool) props["TextFont_Strikeout"];
                bool underline = (bool) props["TextFont_Underline"];

                item.ClickMode = (ClickMode) Enum.Parse(typeof(ClickMode), props["ClickMode"] as string);
                item.FilePath = props["FilePath"] as string;
                item.Arguments = props["Arguments"] as string;
                item.Column = (int) (long) props["Column"];
                item.Row = (int) (long) props["Row"];
                item.ColumnSpan = (int) (long) props["ColumnSpan"];
                item.RowSpan = (int) (long) props["RowSpan"];

                PageItems.Add(item);
            }
        }

        private static void Invalidate()
        {
            if (Pages == null)
            {
                Pages = new List<Page>();

                var page = new Page();
                page.PageName = "Main";
                page.VisibleHeader = true;
                page.VisibleBackButton = false;
                page.VisibleOptionButton = true;
                page.CloseMode = PageCloseMode.Dispose;
                Pages.Add(page);
            }
            if (PageItems == null)
            {
                PageItems = new List<PageItem>();
            }

            foreach (Form form in Application.OpenForms)
            {
                var container = form as PageContainer;
                if (container == null) return;

                container.ResetLayout(SizeModeWidth, SizeModeHeight);
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
