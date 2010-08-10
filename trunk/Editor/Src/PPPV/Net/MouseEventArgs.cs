using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Editor;

namespace PPPV.Net{

   public class MouseEventArgs : System.Windows.Forms.MouseEventArgs
   {
      public bool alreadyPerform;

      public MouseEventArgs(CanvasMouseEventArgs arg):base(arg.Button,arg.Clicks,arg.X,arg.Y,arg.Delta)
      {
         alreadyPerform = false;
      }
   }
}
