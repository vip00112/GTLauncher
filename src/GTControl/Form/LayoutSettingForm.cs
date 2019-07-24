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
        private List<PageItem> _copyItems;
        private Page _selectedPage;
        private bool _isPressedCtrl;

        #region Constructor
        private LayoutSettingForm()
        {
            InitializeComponent();

            _cells = new List<Cell>();
            _ancherItems = new List<PageItem>();
            _copyItems = new List<PageItem>();
        }

        internal LayoutSettingForm(DockMode dock, SizeMode width, SizeMode height, List<Page> pages, List<PageItem> pageItems) : this()
        {
            _layout = new LayoutProperty()
            {
                DockMode = dock,
                SizeModeWidth = width,
                SizeModeHeight = height,
            };

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

        public DockMode DockMode { get; private set; }

        public SizeMode SizeModeWidth { get; private set; }

        public SizeMode SizeModeHeight { get; private set; }

        public List<Page> Pages { get; private set; }

        public List<PageItem> PageItems { get; private set; }
        #endregion

        #region Control Event
        private void LayoutSettingForm_Load(object sender, EventArgs e)
        {
            Setting.IsEditMode = true;

            propertyGrid_layout.SelectedObject = _layout;
            propertyGrid_page.BrowsableAttributes = new AttributeCollection(new Attribute[] { new CategoryAttribute("Page Option") });

            foreach (var p in Pages)
            {
                string pageName = p.PageName;
                var page = CreatePage(pageName, p);

                var pageItems = PageItems.Where(o => o.PageName == pageName);
                foreach (var pageItem in pageItems)
                {
                    var item = page.AddItem(pageItem);
                    if (item != null)
                    {
                        page.PageBody.StartEditItem(item);
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
            }
            if (!_isPressedCtrl) return;

            if (e.KeyCode == Keys.C)
            {
                if (!CanCopyPaste()) return;

                ResetCopyItems();
                foreach (var item in _ancherItems)
                {
                    var copy = item.Copy();
                    if (copy == null) continue;

                    _copyItems.Add(copy);
                }
            }
            else if (e.KeyCode == Keys.V)
            {
                if (!CanCopyPaste()) return;

                if (SelectedPage == null) return;
                if (_copyItems.Count == 0) return;

                var items = _copyItems.ToArray();
                foreach (var copy in items)
                {
                    _copyItems.Remove(copy);

                    var item = SelectedPage.AddItem(copy);
                    if (item != null)
                    {
                        item.BringToFront();
                        item.PageName = SelectedPage.PageName;

                        SelectedPage.PageBody.StartEditItem(item);
                        item.OnMouseDownEvent += pageItem_MouseDown;
                        item.OnPaintEvent += pageItem_Paint;
                        AddAncherItem(item);
                    }
                }
                ResetCopyItems();
            }
        }

        private void LayoutSettingForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _isPressedCtrl = false;
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
                CreatePage(pageName, null);
            }
        }

        private void menuItem_addItem_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (SelectedPage == null) return;
            if (_ancherItems.Count > 0) return;

            var selectedCells = _cells.Where(o => o.IsSelected).ToList();
            if (selectedCells.Count == 0) return;

            var minX = selectedCells.Min(o => o.X);
            var minY = selectedCells.Min(o => o.Y);
            var maxX = selectedCells.Max(o => o.X);
            var maxY = selectedCells.Max(o => o.Y);

            int grid = PageBody.Grid;
            var item = SelectedPage.AddItem(new PageItem()
            {
                PageName = SelectedPage.PageName,
                X = minX,
                Y = minY,
                Width = maxX - minX + grid,
                Height = maxY - minY + grid,
            });
            if (item != null)
            {
                item.BringToFront();
                SelectedPage.PageBody.StartEditItem(item);
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
                SelectedPage.PageBody.StopEditItem(item);
            }
            ResetAncherItems();
        }

        private void menuItem_save_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (!MessageBoxUtil.Confirm("Are you sure you want to save layout?")) return;

            DockMode = _layout.DockMode;
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

        private void pageItem_MouseDown(object sender, MouseEventArgs e)
        {
            var item = sender as PageItem;
            if (item == null) return;

            AddAncherItem(item);

            _cells.ForEach(o => o.IsSelected = false);
            SelectedPage.PageBody.Invalidate();
        }

        private void pageBody_Resize(object sender, EventArgs e)
        {
            if (SelectedPage == null) return;

            ResetCells();
        }

        private void pageBody_Paint(object sender, PaintEventArgs e)
        {
            if (SelectedPage == null) return;

            DrawCell(e.Graphics);
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
            tabControl_pages.Focus();
        }

        private void pageBody_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedPage == null) return;
            if (_startCell == null) return;

            int x = e.Location.X > SelectedPage.PageBody.Width ? SelectedPage.PageBody.Width : e.Location.X;
            int y = e.Location.Y > SelectedPage.PageBody.Height ? SelectedPage.PageBody.Height : e.Location.Y;
            _endCell = _cells.FirstOrDefault(o => o.IsInLocation(new Point(x, y)));
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
            SelectedPage.PageBody.Invalidate();
        }

        private void pageBody_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedPage == null) return;
            if (_startCell == null) return;

            int x = e.Location.X > SelectedPage.PageBody.Width ? SelectedPage.PageBody.Width : e.Location.X;
            int y = e.Location.Y > SelectedPage.PageBody.Height ? SelectedPage.PageBody.Height : e.Location.Y;
            _endCell = _cells.FirstOrDefault(o => o.IsInLocation(new Point(x, y)));
            if (_endCell != null) SelectedPage.PageBody.Invalidate();
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            switch (e.ChangedItem.Label)
            {
                case "SizeModeWidth":
                    foreach (TabPage tabPage in tabControl_pages.TabPages)
                    {
                        foreach (var page in tabPage.Controls[0].Controls.OfType<Page>())
                        {
                            page.Parent.Width = Setting.GetWidth((SizeMode) e.ChangedItem.Value);
                        }
                    }
                    break;
                case "SizeModeHeight":
                    foreach (TabPage tabPage in tabControl_pages.TabPages)
                    {
                        foreach (var page in tabPage.Controls[0].Controls.OfType<Page>())
                        {
                            page.Parent.Height = Setting.GetHeight((SizeMode) e.ChangedItem.Value);
                        }
                    }
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
        private Page CreatePage(string pageName, Page clone)
        {
            var tabPage = new TabPage();
            tabPage.Text = pageName;
            tabPage.AutoScroll = true;
            tabControl_pages.TabPages.Add(tabPage);

            var panel = new Panel();
            panel.Name = pageName;
            panel.BackColor = Color.Transparent;
            panel.Padding = new Padding(0);
            panel.Location = new Point(0, 0);
            panel.Width = Setting.GetWidth(Setting.SizeModeWidth);
            panel.Height = Setting.GetHeight(Setting.SizeModeHeight);
            tabPage.Controls.Add(panel);

            var page = new Page(pageName);
            page.Title = (clone != null) ? clone.Title : "Title";
            page.VisibleTitle = (clone != null) ? clone.VisibleTitle : true;
            page.VisibleBackButton = (clone != null) ? clone.VisibleBackButton : true;
            page.CloseMode = (clone != null) ? clone.CloseMode : PageCloseMode.Hide;
            page.PageBody.Resize += pageBody_Resize;
            page.PageBody.Paint += pageBody_Paint;
            page.PageBody.MouseDown += pageBody_MouseDown;
            page.PageBody.MouseMove += pageBody_MouseMove;
            page.PageBody.MouseUp += pageBody_MouseUp;
            panel.Controls.Add(page);

            return page;
        }

        private void ResetCells()
        {
            _cells.Clear();

            int grid = PageBody.Grid;
            for (int col = 0; col < SelectedPage.PageBody.ColumnCount; col++)
            {
                for (int row = 0; row < SelectedPage.PageBody.RowCount; row++)
                {
                    var cell = new Cell()
                    {
                        X = col * grid,
                        Y = row * grid,
                        Width = grid,
                        Height = grid,
                        IsSelected = false,
                    };
                    _cells.Add(cell);
                }
            }
        }

        private void DrawCell(Graphics g)
        {
            using (var b = new SolidBrush(Color.FromArgb(50, Color.Blue)))
            {
                if (_startCell != null && _endCell != null)
                {
                    foreach (var cell in _cells)
                    {
                        if (cell.IsInRange(_startCell, _endCell))
                        {
                            g.FillRectangle(b, cell.X, cell.Y, cell.Width, cell.Height);
                        }
                    }
                }
                else
                {
                    foreach (var cell in _cells)
                    {
                        if (!cell.IsSelected) continue;

                        g.FillRectangle(b, cell.X, cell.Y, cell.Width, cell.Height);
                    }
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

        private void ResetCopyItems()
        {
            var items = _copyItems.ToArray();
            foreach (var item in items)
            {
                _copyItems.Remove(item);
                item.Dispose();
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
            tabControl_pages.Focus();
        }

        private bool CanCopyPaste()
        {
            Control control = this;
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }

            return control != null && control is TabControl;
        }
        #endregion

        #region Inner Class
        private class LayoutProperty
        {
            [Category("Page Option")]
            public DockMode DockMode { get; set; }

            [Category("Page Option")]
            public SizeMode SizeModeWidth { get; set; }

            [Category("Page Option")]
            public SizeMode SizeModeHeight { get; set; }
        }
        #endregion
    }
}