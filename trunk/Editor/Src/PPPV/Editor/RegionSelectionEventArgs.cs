namespace Pppv.Editor
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class RegionSelectionEventArgs : EventArgs
   {
      private Rectangle selectionRectangle;

      public RegionSelectionEventArgs()
      {
         this.selectionRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
      }

      public Rectangle SelectionRectangle
      {
         get { return this.selectionRectangle; }
         set { this.selectionRectangle = value; }
      }
   }
}