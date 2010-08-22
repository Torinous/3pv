namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class ResizeEventArgs
   {
      private Size oldSize, newSize;

      public Size NewSize {
         get { return newSize; }
         set { newSize = value; }
      }
      
      public Size OldSize {
         get { return oldSize; }
         set { oldSize = value; }
      }

      public ResizeEventArgs(Size oldSize, Size newSize)
      {
         this.oldSize = oldSize;
         this.newSize = newSize;
      }
   }
}
