using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Net {

  public class BaseNetElementMoveEventArgs{
    public Point from, to;

    public BaseNetElementMoveEventArgs(Point start, Point stop){
      from = new Point(0,0);
      to = new Point(0,0);
      from.X = start.X;
      from.Y = start.Y;
      to.X = stop.X;
      to.Y = stop.Y;
    }
  }
}
