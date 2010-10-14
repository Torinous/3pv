namespace Pppv.Editor
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class CanvasMouseEventArgs : MouseEventArgs
   {
      public CanvasMouseEventArgs(MouseEventArgs arg, Point place) : base(arg.Button, arg.Clicks, place.X, place.Y, arg.Delta)
      {
      }
   }
}
