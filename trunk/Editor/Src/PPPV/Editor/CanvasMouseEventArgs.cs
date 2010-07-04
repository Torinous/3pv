using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Editor
{
  public class CanvasMouseEventArgs : MouseEventArgs
  {
    public CanvasMouseEventArgs(MouseEventArgs _arg, Point p):base(_arg.Button, _arg.Clicks, p.X, p.Y, _arg.Delta)
    {
    }
  }
}
