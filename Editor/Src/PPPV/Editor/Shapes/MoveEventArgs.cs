/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 21:35
 */
 
namespace Pppv.Editor.Shapes
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class MoveEventArgs : EventArgs
   {
      private Point from, to;

      public MoveEventArgs(Point start, Point stop)
      {
         this.From = new Point(0, 0);
         this.To = new Point(0, 0);
         this.From = start;
         this.To = stop;
      }
      
      public Point To
      {
         get { return this.to; }
         set { this.to = value; }
      }
      
      public Point From
      {
         get { return this.from; }
         set { this.from = value; }
      }
   }
}