using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPv.Editor{

  public class RegionSelectionEventArgs{
      public Rectangle selectionRectangle;

      public RegionSelectionEventArgs(){
         selectionRectangle = new Rectangle( new Point(0,0), new Size(0,0));
      }
   }
}
