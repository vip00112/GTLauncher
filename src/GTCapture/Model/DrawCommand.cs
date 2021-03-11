using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTCapture
{
    public class DrawCommand
    {
        private List<IMemento> _mementos;
        private List<IMemento> _history;

        #region Constructor
        public DrawCommand()
        {
            _mementos = new List<IMemento>();
            _history = new List<IMemento>();
        }
        #endregion

        #region Properties
        public bool CanUndo { get { return _mementos.Count > 0; } }

        public bool CanRedo { get { return _history.Count > 0; } }
        #endregion

        #region Public Method
        public bool Undo()
        {
            if (!CanUndo) return false;

            var memento = _mementos.Last();
            _mementos.Remove(memento);
            _history.Add(memento);
            return true;
        }

        public bool Redo()
        {
            if (!CanRedo) return false;

            var memento = _history.Last();
            _history.Remove(memento);
            _mementos.Add(memento);
            return true;
        }

        public void AddMemento(IMemento memento)
        {
            _mementos.Add(memento);
            _history.Clear();
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = InterpolationMode.High;
            foreach (var memento in _mementos)
            {
                memento.Draw(g);
            }
        }
        #endregion
    }
}