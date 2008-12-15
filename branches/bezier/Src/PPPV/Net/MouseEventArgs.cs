using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Editor;

namespace PPPv.Net{

   public class MouseEventArgs : System.Windows.Forms.MouseEventArgs{
      public ToolEnum currentTool;

      public MouseEventArgs(CanvasMouseEventArgs _arg):base(_arg.Button,_arg.Clicks,_arg.X,_arg.Y,_arg.Delta){
         currentTool = _arg.currentTool;
      }
   }
}
