namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class SelectionChangeEventArgs : EventArgs
   {
      private bool newState;

      public SelectionChangeEventArgs(bool state)
      {
         this.NewState = state;
      }

      public bool NewState
      {
         get { return this.newState; }
         set { this.newState = value; }
      }
   }
}