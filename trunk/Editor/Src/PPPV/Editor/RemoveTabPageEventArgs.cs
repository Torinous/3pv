using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pppv.Editor
{
  public class RemoveTabPageEventArgs
  {
		int tabIndex;

		public int TabIndex{
			get { return tabIndex; }
			set { tabIndex = value; }
		}

      public RemoveTabPageEventArgs(int tabIndex){
         TabIndex = tabIndex;
      }
   }
}
