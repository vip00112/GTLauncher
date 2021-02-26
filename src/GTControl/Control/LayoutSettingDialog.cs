using GTLocalization;
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
    public partial class LayoutSettingDialog : Form
    {
        private LayoutProperty _layout;
        private List<Page> _pages;
        private List<PageItem> _pageItems;

        private Cell _startCell;
        private Cell _endCell;
        private List<Cell> _cells;
        private List<PageItem> _ancherItems;
        private List<PageItem> _copyItems;
        private Page _selectedPage;
        private bool _isPressedCtrl;

        #region Constructor
        public LayoutSettingDialog()
        {
            InitializeComponent();

            _cells = new List<Cell>();
            _ancherItems = new List<PageItem>();
            _copyItems = new List<PageItem>();
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
        #endregion

        #region Control Event
        private void LayoutSettingForm_Load(object sender, EventArgs e)
        {
            if (Runtime.DesignMode) return;

            LayoutSetting.Invalidate(this);
            LayoutSetting.IsEditMode = true;

            _layout = new LayoutProperty()
            {
                DockMode = LayoutSetting.DockMode,
                SizeModeWidth = LayoutSetting.SizeModeWidth,
                SizeModeHeight = LayoutSetting.SizeModeHeight,
            };
            propertyGrid_layout.SelectedObject = _layout;
            propertyGrid_page.BrowsableAttributes = new AttributeCollection(new Attribute[] { new CategoryAttribute("Page Option") });

            _pages = LayoutSetting.Pages.ToList();
            _pageItems = LayoutSetting.PageItems.ToList();
            foreach (var p in _pages)
            {
                string pageName = p.PageName;
                var page = CreatePage(pageName, p);

                var pageItems = _pageItems.Where(o => o.PageName == pageName);
                foreach (var pageItem in pageItems)
                {
                    CreatePageItem(page, pageItem, pageItem.PageName);
                }
            }

            var pages = tabControl_pages.SelectedTab.Controls[0].Controls.OfType<Page>().ToList();
            if (pages.Count == 0) return;

            SelectedPage = pages[0];
            ResetCells();
        }

        private void LayoutSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LayoutSetting.IsEditMode = false;
        }

        private void LayoutSettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _isPressedCtrl = true;
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
            using (var dialog = new CreatePageDialog(pageNames))
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

            var src = new PageItem()
            {
                X = minX,
                Y = minY,
                Width = maxX - minX + PageBody.Grid,
                Height = maxY - minY + PageBody.Grid,
            };
            var item = CreatePageItem(SelectedPage, src, SelectedPage.PageName);
            if (item != null) AddAncherItem(item);

            _cells.ForEach(o => o.IsSelected = false);
        }

        private void menuItem_deletePage_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (SelectedPage == null) return;
            if (!MessageBoxUtil.Confirm(Resource.GetString(Key.PageDeleteConfirmMsg))) return;
            if (SelectedPage.PageName == "Main")
            {
                MessageBoxUtil.Error(Resource.GetString(Key.PageDeleteErrorMsg));
                return;
            }

            var items = SelectedPage.PageItems.ToList();
            foreach (var item in items)
            {
                DeletePageItem(SelectedPage, item);
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
            if (!MessageBoxUtil.Confirm(Resource.GetString(Key.PageItemDeleteConfirmMsg))) return;

            foreach (var item in _ancherItems)
            {
                DeletePageItem(SelectedPage, item);
            }
            ResetAncherItems();
        }

        private void menuItem_copy_Click(object sender, EventArgs e)
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

        private void menuItem_paste_Click(object sender, EventArgs e)
        {
            if (!CanCopyPaste()) return;
            if (SelectedPage == null) return;
            if (_copyItems.Count == 0) return;

            foreach (var copy in _copyItems)
            {
                var item = CreatePageItem(SelectedPage, copy, SelectedPage.PageName);
                if (item != null) AddAncherItem(item);
            }
            ResetCopyItems();
        }

        private void menuItem_save_Click(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (!MessageBoxUtil.Confirm(Resource.GetString(Key.LayoutSaveConfirmMsg))) return;

            _pages.Clear();
            _pageItems.Clear();
            foreach (TabPage tabPage in tabControl_pages.TabPages)
            {
                var pages = tabPage.Controls[0].Controls.OfType<Page>().ToList();
                if (pages.Count == 0) continue;

                var page = pages[0];
                _pages.Add(page);

                var items = page.PageItems;
                foreach (var item in items)
                {
                    _pageItems.Add(item);
                }
            }

            LayoutSetting.DockMode = _layout.DockMode;
            LayoutSetting.SizeModeWidth = _layout.SizeModeWidth;
            LayoutSetting.SizeModeHeight = _layout.SizeModeHeight;
            LayoutSetting.Pages = _pages;
            LayoutSetting.PageItems = _pageItems;
            LayoutSetting.InvalidateAllForms();

            DialogResult = DialogResult.OK;
        }

        private void menuItem_specialFolder_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"{MyComputer}");
            sb.AppendLine(@"C:\Windows : {Windows}");
            sb.AppendLine(@"C:\Users\User : {UserProfile}");
            sb.AppendLine(@"C:\Users\User\Desktop : {Desktop}");
            sb.AppendLine(@"C:\Users\User\Documents : {MyDocuments}");
            sb.AppendLine(@"C:\Users\User\Musics : {MyMusic}");
            sb.AppendLine(@"C:\Users\User\Videos : {MyVideos}");
            sb.AppendLine(@"C:\Users\User\Pictures : {MyPictures}");
            sb.AppendLine(@"C:\Users\User\AppData\Local : {LocalApplicationData}");
            sb.AppendLine(@"C:\Users\User\AppData\Roaming : {ApplicationData}");

            string title = "Special Folder";
            string content = sb.ToString();
            int width = 320;
            int height = 230;
            using (var dialog = new NoteDialog(title, content, width, height))
            {
                dialog.ShowDialog();
            }
        }

        private void tabControl_pages_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isPressedCtrl = false;

            if (tabControl_pages.SelectedTab == null) return;

            var pages = tabControl_pages.SelectedTab.Controls[0].Controls.OfType<Page>().ToList();
            if (pages.Count == 0) return;

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
                            page.Parent.Width = LayoutSetting.GetWidth((SizeMode) e.ChangedItem.Value);
                        }
                    }
                    break;
                case "SizeModeHeight":
                    foreach (TabPage tabPage in tabControl_pages.TabPages)
                    {
                        foreach (var page in tabPage.Controls[0].Controls.OfType<Page>())
                        {
                            page.Parent.Height = LayoutSetting.GetHeight((SizeMode) e.ChangedItem.Value);
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
        private Page CreatePage(string pageName, Page src)
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
            panel.Width = LayoutSetting.GetWidth(LayoutSetting.SizeModeWidth);
            panel.Height = LayoutSetting.GetHeight(LayoutSetting.SizeModeHeight);
            tabPage.Controls.Add(panel);

            var page = new Page(pageName);
            page.Title = (src != null) ? src.Title : "Title";
            page.VisibleTitle = (src != null) ? src.VisibleTitle : true;
            page.VisibleBackButton = (src != null) ? src.VisibleBackButton : true;
            page.CloseMode = (src != null) ? src.CloseMode : PageCloseMode.Hide;
            page.PageBody.Resize += pageBody_Resize;
            page.PageBody.Paint += pageBody_Paint;
            page.PageBody.MouseDown += pageBody_MouseDown;
            page.PageBody.MouseMove += pageBody_MouseMove;
            page.PageBody.MouseUp += pageBody_MouseUp;
            panel.Controls.Add(page);
            LayoutSetting.Invalidate(tabPage);

            return page;
        }

        private PageItem CreatePageItem(Page targetPage, PageItem src, string pageName)
        {
            var item = targetPage.AddItemAfterCopy(src);
            if (item != null)
            {
                item.PageName = pageName;
                item.BringToFront();
                targetPage.PageBody.StartEditItem(item, pageItem_MouseDown, pageItem_Paint);
            }
            return item;
        }

        private void DeletePageItem(Page targetPage, PageItem item)
        {
            targetPage.PageBody.StopEditItem(item, pageItem_MouseDown, pageItem_Paint);
            targetPage.RemoveItem(item);
        }

        private void ResetCells()
        {
            _cells.Clear();

            for (int col = 0; col < SelectedPage.PageBody.ColumnCount; col++)
            {
                for (int row = 0; row < SelectedPage.PageBody.RowCount; row++)
                {
                    var cell = new Cell()
                    {
                        X = col * PageBody.Grid,
                        Y = row * PageBody.Grid,
                        Width = PageBody.Grid,
                        Height = PageBody.Grid,
                        IsSelected = false,
                    };
                    _cells.Add(cell);
                }
            }
        }

        private void DrawCell(Graphics g)
        {
            using (var b = new SolidBrush(Color.FromArgb(70, LayoutSetting.GetBackColorHover(LayoutSetting.Theme))))
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
            var items = _ancherItems.ToList();
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
            var items = _copyItems.ToList();
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