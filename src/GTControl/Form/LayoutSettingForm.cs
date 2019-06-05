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
        public class Property
        {
            [Category("Page Option")]
            public SizeMode SizeModeWidth { get; set; }

            [Category("Page Option")]
            public SizeMode SizeModeHeight { get; set; }
        }

        private Property _background;
        private Cell _startCell;
        private Cell _endCell;
        private List<Cell> _cells;
        private PageItem _selectedItem;

        #region Constructor
        public LayoutSettingForm()
        {
            InitializeComponent();

            _background = new Property()
            {
                SizeModeWidth = Setting.SizeModeWidth,
                SizeModeHeight = Setting.SizeModeHeight,
            };
            _cells = new List<Cell>();

            propertyGrid.BrowsableAttributes = new AttributeCollection(new Attribute[] { new CategoryAttribute("Page Option") });
            propertyGrid.SelectedObject = _background;
            panel_container.Width = Setting.GetWidth(Setting.SizeModeWidth);
            panel_container.Height = Setting.GetHeight(Setting.SizeModeHeight);
        }
        #endregion

        #region Properties
        public PageItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (_selectedItem != null)
                {
                    propertyGrid.SelectedObject = _selectedItem;
                }
                else
                {
                    propertyGrid.SelectedObject = _background;
                }
            }
        }
        #endregion

        #region Control Event
        private void LayoutSettingForm_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            MinimumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            ResetCells();
        }

        private void menuItem_add_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null) return;

            var selectedCells = _cells.Where(o => o.IsSelected).ToList();
            if (selectedCells.Count == 0) return;

            int minCol = selectedCells.Min(o => o.Column);
            int maxCol = selectedCells.Max(o => o.Column);
            int minRow = selectedCells.Min(o => o.Row);
            int maxRow = selectedCells.Max(o => o.Row);

            var item = new PageItem();
            item.IsEditMode = true;
            item.OnMouseDownEvent += pageItem_MouseDown;

            // TODO : 추가할 공간이 없을시 예외 처리
            pageBody.Controls.Add(item, minCol, minRow);
            pageBody.SetColumnSpan(item, maxCol - minCol + 1);
            pageBody.SetRowSpan(item, maxRow - minRow + 1);

            SelectedItem = item;

            _cells.ForEach(o => o.IsSelected = false);
        }

        private void menuItem_delete_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null) return;

            pageBody.Controls.Remove(SelectedItem);
            SelectedItem.Dispose();
            SelectedItem = null;
        }

        private void pageItem_MouseDown(object sender, MouseEventArgs e)
        {
            SelectedItem = sender as PageItem;
            if (SelectedItem == null) return;

            _cells.ForEach(o => o.IsSelected = false);
            pageBody.Invalidate();

            // TODO : TableLayoutPanel 속성 표기 방법
            // TODO : 필요한 속성만 표기 방법 or LayoutSetting 전용 Entity 추가
            // TODO : Folder용 Page셋팅 화면 (ClickMode가 Folder일시 사용하기 위함)
            // TODO : PageItem에 Folder용Page 연결할 속성 추가
        }

        private void pageBody_MouseDown(object sender, MouseEventArgs e)
        {
            _cells.ForEach(o => o.IsSelected = false);
            SelectedItem = null;

            _startCell = _cells.FirstOrDefault(o => o.IsInLocation(e.Location));
            _endCell = _startCell;
            if (_startCell != null) pageBody.Invalidate();
        }

        private void pageBody_MouseMove(object sender, MouseEventArgs e)
        {
            if (_startCell == null) return;

            _endCell = _cells.FirstOrDefault(o => o.IsInLocation(e.Location));
            if (_endCell != null) pageBody.Invalidate();
        }

        private void pageBody_MouseUp(object sender, MouseEventArgs e)
        {
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
            ResetCells();
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            switch (e.ChangedItem.Label)
            {
                case "SizeModeWidth":
                    panel_container.Width = Setting.GetWidth((SizeMode) e.ChangedItem.Value);
                    break;
                case "SizeModeHeight":
                    panel_container.Height = Setting.GetHeight((SizeMode) e.ChangedItem.Value);
                    break;
            }
        }
        #endregion

        #region Private Method
        private void ResetCells()
        {
            _cells.Clear();
            int width = pageBody.Width / pageBody.ColumnCount;
            int height = pageBody.Height / pageBody.RowCount;
            for (int col = 0; col < pageBody.ColumnCount; col++)
            {
                for (int row = 0; row < pageBody.RowCount; row++)
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
    }
}
