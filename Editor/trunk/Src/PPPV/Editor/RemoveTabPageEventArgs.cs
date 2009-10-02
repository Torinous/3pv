using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Editor{

  public class RemoveTabPageEventArgs{
      public int tabIndex;

      public RemoveTabPageEventArgs(int tabIndex_){
         tabIndex = tabIndex_;
      }
   }
}
