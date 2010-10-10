namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor;

   public class MouseEventArgs : System.Windows.Forms.MouseEventArgs
   {
      private bool alreadyPerform;

      public MouseEventArgs(CanvasMouseEventArgs arg) : base(arg.Button, arg.Clicks, arg.X, arg.Y, arg.Delta)
      {
         this.AlreadyPerform = false;
      }

      public bool AlreadyPerform 
      {
         get { return this.alreadyPerform; }
         set { this.alreadyPerform = value; }
      }
   }
}
