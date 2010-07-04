using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Net {

  public class MoveEventArgs{
    public Point from, to;

    public MoveEventArgs(Point start, Point stop){
      from = new Point(0,0);
      to = new Point(0,0);
      from.X = start.X;
      from.Y = start.Y;
      to.X = stop.X;
      to.Y = stop.Y;
    }
  }
}
