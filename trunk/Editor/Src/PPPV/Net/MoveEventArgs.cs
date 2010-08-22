namespace PPPV.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class MoveEventArgs : EventArgs
   {
      private Point from, to;
      
      public Point To {
         get { return to; }
         set { to = value; }
      }
      
      public Point From {
         get { return from; }
         set { from = value; }
      }

      public MoveEventArgs(Point start, Point stop)
      {
         from = new Point(0,0);
         to = new Point(0,0);
         from.X = start.X;
         from.Y = start.Y;
         to.X = stop.X;
         to.Y = stop.Y;
      }
   }
}
