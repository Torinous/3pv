namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class SelectionChangeEventArgs{
      private bool newState;

      public bool NewState {
         get { return newState; }
         set { newState = value; }
      }

      public SelectionChangeEventArgs(bool state)
      {
         NewState = state;
      }
   }
}
