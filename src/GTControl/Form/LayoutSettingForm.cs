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
            public SizeMode SizeModeWidth { get; set; }

            public SizeMode SizeModeHeight { get; set; }
        }

        private Cell _startCell;
        private Cell _endCell;
        private List<Cell> _cells;
        private PageItem _selectedItem;

        #region Constructor
        public LayoutSettingForm()
        {
            InitializeComponent();

            _cells = new List<Cell>();

            propertyGrid.SelectedObject = new Property()
            {
                SizeModeWidth = Setting.SizeModeWidth,
                SizeModeHeight = Setting.SizeModeHeight,
            };
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
            if (_selectedItem != null) return;

            var selectedCells = _cells.Where(o => o.IsSelected).ToList();
            if (selectedCells.Count == 0) return;

            int minCol = selectedCells.Min(o => o.Column);
            int maxCol = selectedCells.Max(o => o.Column);
            int minRow = selectedCells.Min(o => o.Row);
            int maxRow = selectedCells.Max(o => o.Row);

            var item = new PageItem();
            item.IsEditMode = true;
            item.OnMouseDown += pageItem_MouseDown;

            pageBody.Controls.Add(item, minCol, minRow);
            pageBody.SetColumnSpan(item, maxCol - minCol + 1);
            pageBody.SetRowSpan(item, maxRow - minRow + 1);

            _cells.ForEach(o => o.IsSelected = false);
        }

        private void menuItem_delete_Click(object sender, EventArgs e)
        {
            if (_selectedItem == null) return;

            pageBody.Controls.Remove(_selectedItem);
            _selectedItem.Dispose();
            _selectedItem = null;
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

        private void pageItem_MouseDown(object sender, MouseEventArgs e)
        {
            _selectedItem = sender as PageItem;

            // TODO : TableLayoutPanel 속성 표기 방법
            // TODO : 필요한 속성만 표기 방법 or LayoutSetting 전용 Entity 추가
            // TODO : Folder용 Page셋팅 화면 (ClickMode가 Folder일시 사용하기 위함)
            // TODO : PageItem에 Folder용Page 연결할 속성 추가
            propertyGrid.SelectedObject = _selectedItem;
        }

        private void pageBody_MouseDown(object sender, MouseEventArgs e)
        {
            _cells.ForEach(o => o.IsSelected = false);
            _selectedItem = null;

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
