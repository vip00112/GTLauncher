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
        private Page _selectedPage;
        private PageItem _selectedItem;

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
        }

        public LayoutSettingForm(SizeMode width, SizeMode height, List<Page> pages, List<PageItem> pageItems) : this()
        {
            SizeModeWidth = width;
            SizeModeHeight = height;

            if (pages != null) Pages = pages;
            else Pages = Setting.Pages;

            if (pageItems != null) PageItems = pageItems;
            else PageItems = Setting.PageItems;
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

        public PageItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem != null)
                {
                    propertyGrid_page.SelectedObject = _selectedItem;
                }
                else
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

                var page = new Page();
                page.PageName = p.PageName;
                page.VisibleHeader = p.VisibleHeader;
                page.VisibleBackButton = p.VisibleBackButton;
                page.VisibleOptionButton = p.VisibleOptionButton;
                page.CloseMode = p.CloseMode;
                page.PageBody.IsEditMode = true;
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
                        item.IsEditMode = true;
                        item.OnMouseDownEvent += pageItem_MouseDown;
                    }
                }
            }

            var pages = tabControl_pages.SelectedTab.Controls[0].Controls.OfType<Page>().ToArray();
            if (pages.Length == 0) return;

            SelectedPage = pages[0];
            ResetCells();
        }

        private void menuItem_addPage_Click(object sender, EventArgs e)
        {

        }

        private void menuItem_addItem_Click(object sender, EventArgs e)
        {
            if (SelectedPage == null) return;
            if (SelectedItem != null) return;

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
                IsEditMode = true,
                Column = minCol,
                Row = minRow,
                ColumnSpan = colSpan,
                RowSpan = rowSpan,
            });
            if (item != null)
            {
                item.OnMouseDownEvent += pageItem_MouseDown;
                SelectedItem = item;
            }
            SelectedItem = item;

            _cells.ForEach(o => o.IsSelected = false);
        }

        private void menuItem_deletePage_Click(object sender, EventArgs e)
        {
            if (SelectedPage == null) return;
            if (!MessageBoxUtil.Confirm("Are you sure you want to delete page?")) return;
        }

        private void menuItem_deleteItem_Click(object sender, EventArgs e)
        {
            if (SelectedPage == null) return;
            if (SelectedItem == null) return;
            if (!MessageBoxUtil.Confirm("Are you sure you want to delete item?")) return;

            SelectedPage.RemoveItem(SelectedItem);
            SelectedItem = null;
        }

        private void menuItem_save_Click(object sender, EventArgs e)
        {
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

        private void tabControl_pages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_pages.SelectedTab == null) return;

            var pages = tabControl_pages.SelectedTab.Controls[0].Controls.OfType<Page>().ToArray();
            if (pages.Length == 0) return;

            SelectedPage = pages[0];
            ResetCells();
        }

        private void pageItem_MouseDown(object sender, MouseEventArgs e)
        {
            SelectedItem = sender as PageItem;
            if (SelectedItem == null) return;

            _cells.ForEach(o => o.IsSelected = false);
            SelectedPage.Invalidate();
        }

        private void pageBody_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedPage == null) return;

            _cells.ForEach(o => o.IsSelected = false);
            SelectedItem = null;

            _startCell = _cells.FirstOrDefault(o => o.IsInLocation(e.Location));
            _endCell = _startCell;
            if (_startCell != null) SelectedPage.PageBody.Invalidate();
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
