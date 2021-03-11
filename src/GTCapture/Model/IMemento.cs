using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTCapture
{
    public interface IMemento
    {
        bool CanDraw();

        IMemento Clone();

        void Draw(Graphics g);
    }
}