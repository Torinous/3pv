using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Editor{

  public class RemoveTabPageEventArgs
  {
      public int tabIndex;
      
		public int TabIndex{
			get { return tabIndex; }
			set { tabIndex = value; }
		}

      public RemoveTabPageEventArgs(int tabIndex){
         TabIndex = tabIndex;
      }
   }
}
