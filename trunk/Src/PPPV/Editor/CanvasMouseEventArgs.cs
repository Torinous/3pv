using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor{

  public class CanvasMouseEventArgs : MouseEventArgs{
    public ToolEnum currentTool;

    public CanvasMouseEventArgs(MouseEventArgs _arg):base(_arg.Button,_arg.Clicks,_arg.X,_arg.Y,_arg.Delta){
    }
  }
}
