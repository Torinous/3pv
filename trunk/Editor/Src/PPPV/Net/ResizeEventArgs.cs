namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class ResizeEventArgs : EventArgs
   {
      private Size oldSize, newSize;

      public ResizeEventArgs(Size oldSize, Size newSize)
      {
         this.oldSize = oldSize;
         this.newSize = newSize;
      }

      public Size NewSize
      {
         get { return this.newSize; }
         set { this.newSize = value; }
      }
      
      public Size OldSize
      {
         get { return this.oldSize; }
         set { this.oldSize = value; }
      }
   }
}