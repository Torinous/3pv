namespace Pppv.Editor
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class RemoveTabPageEventArgs : EventArgs
   {
      private int tabIndex;
      private bool breakEvent;

      public RemoveTabPageEventArgs(int tabIndex)
      {
         this.TabIndex = tabIndex;
      }

      public int TabIndex
      {
         get { return this.tabIndex; }
         set { this.tabIndex = value; }
      }

      public bool BreakEvent
      {
         get { return this.breakEvent; }
         set { this.breakEvent = value; }
      }
   }
}
