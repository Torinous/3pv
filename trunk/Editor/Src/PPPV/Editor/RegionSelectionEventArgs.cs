namespace PPPV.Editor
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class RegionSelectionEventArgs : EventArgs
   {
      Rectangle selectionRectangle;

      public Rectangle SelectionRectangle {
         get { return selectionRectangle; }
         set { selectionRectangle = value; }
      }

      public RegionSelectionEventArgs(){
         selectionRectangle = new Rectangle( new Point(0,0), new Size(0,0));
      }
   }
}
