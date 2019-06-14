using GTUtil;
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
        private LayoutProperty _layout;
        private Cell _startCell;
        private Cell _endCell;
        private List<Cell> _cells;
        private List<PageItem> _ancherItems;
        private Page _selectedPage;
        private bool _isPressedCtrl;

        #region Constructor
        private LayoutSettingForm()
        {
            InitializeComponent();

            _layout = new LayoutProperty()
            {
                SizeModeWidth = Setting.SizeModeWidth,
                SizeModeHeight = Setting.SizeModeHeight,
            };
            _cells = new List<Cell>();
            _ancherItems = new List<PageItem>();
        }

        internal LayoutSettingForm(SizeMode width, SizeMode height, List<Page> pages, List<PageItem> pageItems) : this()
        {
            SizeModeWidth = width;
            SizeModeHeight = height;

            if (pages != null) Pages = pages;
            else Pages = Setting.Pages.ToList();

            if (pageItems != null) PageItems = pageItems;
            else PageItems = Setting.PageItems.ToList();
        }
        #endregion

        #region Properties
        public Page SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
                if (_selectedPage != null)
                {
                    propertyGrid_page.SelectedObject = _selectedPage;
                }
            }
        }

        public SizeMode SizeModeWidth { get; private set; }

        public SizeMode SizeModeHeight { get; private set; }

        public List<Page> Pages { get; private set; }

        public List<PageItem> PageItems { get; private set; }
        #endregion

        #region Control Event
        private void LayoutSettingForm_Load(object sender, EventArgs e)
        {
            Setting.IsEditMode = true;

            Location = new Point(0, 0);
            MinimumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            propertyGrid_layout.SelectedObject = _layout;
            propertyGrid_page.BrowsableAttributes = new AttributeCollection(new Attribute[] { new CategoryAttribute("Page Option") });

            foreach (var p in Pages)
            {
                var tabPage = new TabPage();
                tabPage.Text = p.PageName;
                tabControl_pages.TabPages.Add(tabPage);

                var panel = new Panel();
                panel.Name = p.PageName;
                panel.Location = new Point(0, 0);
                panel.Width = Setting.GetWidth(Setting.SizeModeWidth);
                panel.Height = Setting.GetHeight(Setting.SizeModeHeight);
                tabPage.Controls.Add(panel);

                var page = new Page(p.PageName);
                page.Title = p.Title;
                page.VisibleTitle = p.VisibleTitle;
                page.VisibleHeader = p.VisibleHeader;
                page.VisibleBackButton = p.VisibleBackButton;
                page.CloseMode = p.CloseMode;
                page.PageBody.MouseDown += pageBody_MouseDown;
                page.PageBody.MouseMove += pageBody_MouseMove;
                page.PageBody.MouseUp += pageBody_MouseUp;
                page.PageBody.Paint += pageBody_Paint;
                page.PageBody.Resize += pageBody_Resize;
                panel.Controls.Add(page);

                var pageItems = PageItems.Where(o => o.PageName == p.PageName);
                foreach (var pageItem in pageItems)
                {
                    var item = page.AddItem(pageItem);
                    if (item != null)
                    {
                        item.OnMouseDownEvent += pageItem_MouseDown;
                        item.OnPaintEvent += pageItem_Paint;
                    }
                }
            }

            var pages = tabControl_pages.SelectedTab.Controls[0].Controls.OfType<Page>().ToArray();
            if (pages.Length == 0) return;

            SelectedPage = pages[0];
            ResetCells();
        }

        private void LayoutSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Setting.IsEditMode = false;
            Setting.Invalidate();
        }

        private void LayoutSettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _isPressedCtrl = true;
                Console.WriteLine(_isPressedCtrl);
            }
        }

        private void LayoutSettingForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _isPressedCtrl = false;
                Console.WriteLine(_isPressedCtrl);
            }
        }

        private void menuItem_addPage_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            var pageNames = new List<string>();
            foreach (TabPage tabPage in tabControl_pages.TabPages)
            {
                pageNames.Add(tabPage.Text);
            }
            using (var dialog = new CreatePageForm(pageNames))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                string pageName = dialog.PageName;
                var tabPage = new TabPage();
                tabPage.Text = pageName;
                tabControl_pages.TabPages.Add(tabPage);

                var panel = new Panel();
                panel.Name = pageName;
                panel.Location = new Point(0, 0);
                panel.Width = Setting.GetWidth(Setting.SizeModeWidth);
                panel.Height = Setting.GetHeight(Setting.SizeModeHeight);
                tabPage.Controls.Add(panel);

                var page = new Page(pageName);
                page.Title = "Title";
                page.VisibleTitle = true;
                page.VisibleHeader = true;
                page.VisibleBackButton = true;
                page.CloseMode = PageCloseMode.Hide;
                page.PageBody.MouseDown += pageBody_MouseDown;
                page.PageBody.MouseMove += pageBody_MouseMove;
                page.PageBody.MouseUp += pageBody_MouseUp;
                page.PageBody.Paint += pageBody_Paint;
                page.PageBody.Resize += pageBody_Resize;
                panel.Controls.Add(page);
            }
        }

        private void menuItem_addItem_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (SelectedPage == null) return;
            if (_ancherItems.Count > 0) return;

            var selectedCells = _cells.Where(o => o.IsSelected).ToList();
            if (selectedCells.Count == 0) return;

            int minCol = selectedCells.Min(o => o.Column);
            int maxCol = selectedCells.Max(o => o.Column);
            int colSpan = maxCol - minCol + 1;

            int minRow = selectedCells.Min(o => o.Row);
            int maxRow = selectedCells.Max(o => o.Row);
            int rowSpan = maxRow - minRow + 1;

            var item = SelectedPage.AddItem(new PageItem()
            {
                PageName = SelectedPage.PageName,
                Column = minCol,
                Row = minRow,
                ColumnSpan = colSpan,
                RowSpan = rowSpan,
            });
            if (item != null)
            {
                item.OnMouseDownEvent += pageItem_MouseDown;
                item.OnPaintEvent += pageItem_Paint;
            }
            AddAncherItem(item);

            _cells.ForEach(o => o.IsSelected = false);
        }

        private void menuItem_deletePage_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (SelectedPage == null) return;
            if (SelectedPage.PageName == "Main")
            {
                MessageBoxUtil.Error("Can't remove Main page.");
                return;
            }
            if (!MessageBoxUtil.Confirm("Are you sure you want to delete page?")) return;

            var items = SelectedPage.PageItems.ToArray();
            foreach (var item in items)
            {
                SelectedPage.RemoveItem(item);
            }

            var tab = tabControl_pages.SelectedTab;
            if (tab == null) return;

            tabControl_pages.TabPages.Remove(tab);
            tab.Dispose();
        }

        private void menuItem_deleteItem_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (SelectedPage == null) return;
            if (_ancherItems.Count == 0) return;
            if (!MessageBoxUtil.Confirm("Are you sure you want to delete item?")) return;

            foreach (var item in _ancherItems)
            {
                SelectedPage.RemoveItem(item);
            }
            ResetAncherItems();
        }

        private void menuItem_save_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (!MessageBoxUtil.Confirm("Are you sure you want to save layout?")) return;

            SizeModeWidth = _layout.SizeModeWidth;
            SizeModeHeight = _layout.SizeModeHeight;

            Pages.Clear();
            PageItems.Clear();
            foreach (TabPage tabPage in tabControl_pages.TabPages)
            {
                var pages = tabPage.Controls[0].Controls.OfType<Page>().ToArray();
                if (pages.Length == 0) continue;

                var page = pages[0];
                Pages.Add(page);

                var items = page.PageItems;
                foreach (var item in items)
                {
                    PageItems.Add(item);
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void menuItem_specialFolder_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("바탕화면 : {Desktop}");
            sb.AppendLine("내문서 : {MyDocuments}");
            sb.AppendLine("내음악 : {MyMusic}");
            sb.AppendLine("내비디오 : {MyVideos}");
            sb.AppendLine("내그림 : {MyPictures}");
            sb.AppendLine("내컴퓨터 : {MyComputer}");
            sb.AppendLine("현재사용자 : {UserProfile}");
            sb.AppendLine("윈도우 : {Windows}");
            sb.AppendLine("User\\AppData\\Local : {LocalApplicationData}");
            sb.Append("User\\AppData\\Roaming : {ApplicationData}");

            string title = "Special Folder";
            string content = sb.ToString();
            int width = 320;
            int height = 230;
            using (var dialog = new NoteForm(title, content, width, height))
            {
                dialog.ShowDialog();
            }
        }

        private void tabControl_pages_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (tabControl_pages.SelectedTab == null) return;

            var pages = tabControl_pages.SelectedTab.Controls[0].Controls.OfType<Page>().ToArray();
            if (pages.Length == 0) return;

            SelectedPage = pages[0];
            ResetCells();
        }

        private void pageItem_MouseDown(object sender, MouseEventArgs e)
        {
            var item = sender as PageItem;
            if (item == null) return;

            AddAncherItem(item);

            _cells.ForEach(o => o.IsSelected = false);
            SelectedPage.PageBody.Invalidate();
        }

        private void pageItem_Paint(object sender, PaintEventArgs e)
        {
            var item = sender as PageItem;
            if (item == null) return;

            if (_ancherItems.Contains(item))
            {
                using (var b = new SolidBrush(Color.FromArgb(100, Color.Red)))
                {
                    e.Graphics.FillRectangle(b, 0, 0, item.Width, item.Height);
                }
            }
        }

        private void pageBody_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedPage == null) return;

            ResetAncherItems();
            _cells.ForEach(o => o.IsSelected = false);

            _startCell = _cells.FirstOrDefault(o => o.IsInLocation(e.Location));
            _endCell = _startCell;
            if (_startCell != null) SelectedPage.PageBody.Invalidate();

            propertyGrid_page.SelectedObject = SelectedPage;
        }

        private void pageBody_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedPage == null) return;
            if (_startCell == null) return;

            _endCell = _cells.FirstOrDefault(o => o.IsInLocation(e.Location));
            if (_endCell != null) SelectedPage.PageBody.Invalidate();
        }

        private void pageBody_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedPage == null) return;
            if (_startCell == null) return;

            _endCell = _cells.FirstOrDefault(o => o.IsInLocation(e.Location));
            if (_endCell == null) return;

            foreach (var cell in _cells)
            {
                if (cell.IsInRange(_startCell, _endCell))
                {
                    cell.IsSelected = true;
                }
            }
            _startCell = null;
            _endCell = null;
        }

        private void pageBody_Paint(object sender, PaintEventArgs e)
        {
            if (SelectedPage == null) return;

            using (var b = new SolidBrush(Color.FromArgb(50, Color.Blue)))
            {
                if (_startCell != null && _endCell != null)
                {
                    foreach (var cell in _cells)
                    {
                        if (cell.IsInRange(_startCell, _endCell))
                        {
                            e.Graphics.FillRectangle(b, cell.X, cell.Y, cell.Width, cell.Height);
                        }
                    }
                }
                else
                {
                    foreach (var cell in _cells)
                    {
                        if (!cell.IsSelected) continue;

                        e.Graphics.FillRectangle(b, cell.X, cell.Y, cell.Width, cell.Height);
                    }
                }
            }
        }

        private void pageBody_Resize(object sender, EventArgs e)
        {
            if (SelectedPage == null) return;

            ResetCells();
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            switch (e.ChangedItem.Label)
            {
                case "SizeModeWidth":
                    SelectedPage.Parent.Width = Setting.GetWidth((SizeMode) e.ChangedItem.Value);
                    break;
                case "SizeModeHeight":
                    SelectedPage.Parent.Height = Setting.GetHeight((SizeMode) e.ChangedItem.Value);
                    break;
                case "PageName":
                    var tab = tabControl_pages.SelectedTab;
                    if (tab == null) return;

                    string pageName = e.ChangedItem.Value as string;
                    if (string.IsNullOrWhiteSpace(pageName)) return;
                    if (pageName == "Main") return;

                    tab.Text = pageName;
                    SelectedPage.PageName = pageName;
                    SelectedPage.PageItems.ForEach(o => o.PageName = pageName);
                    break;
            }
        }
        #endregion

        #region Private Method
        private void ResetCells()
        {
            _cells.Clear();
            int width = SelectedPage.PageBody.Width / SelectedPage.PageBody.ColumnCount;
            int height = SelectedPage.PageBody.Height / SelectedPage.PageBody.RowCount;
            for (int col = 0; col < SelectedPage.PageBody.ColumnCount; col++)
            {
                for (int row = 0; row < SelectedPage.PageBody.RowCount; row++)
                {
                    var cell = new Cell()
                    {
                        X = col * width,
                        Y = row * height,
                        Width = width,
                        Height = height,
                        Column = col,
                        Row = row,
                        IsSelected = false,
                    };
                    _cells.Add(cell);
                }
            }
        }

        private void ResetAncherItems()
        {
            var items = _ancherItems.ToArray();
            foreach (var item in items)
            {
                _ancherItems.Remove(item);
                if (!item.IsDisposed)
                {
                    item.Invalidate();
                }
            }
        }

        private void AddAncherItem(PageItem item)
        {
            if (_isPressedCtrl)
            {
                if (_ancherItems.Contains(item))
                {
                    _ancherItems.Remove(item);
                }
                else
                {
                    _ancherItems.Add(item);
                }
            }
            else
            {
                ResetAncherItems();
                _ancherItems.Add(item);
            }

            if (_ancherItems.Count > 0)
            {
                propertyGrid_page.SelectedObjects = _ancherItems.ToArray();
            }

            item.Invalidate();
        }
        #endregion

        #region Inner Class
        private class LayoutProperty
        {
            [Category("Page Option")]
            public SizeMode SizeModeWidth { get; set; }

            [Category("Page Option")]
            public SizeMode SizeModeHeight { get; set; }
        }
        #endregion
    }
}