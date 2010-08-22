namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor;

   public class SaveEventArgs : EventArgs
   {
      private string fileName;
      private string netId;

      public string NetId {
         get { return netId; }
         set { netId = value; }
      }

      public string FileName {
         get { return fileName; }
         set { fileName = value; }
      }

      public SaveEventArgs(string fileName, string netId){
         this.fileName = fileName;
         this.netId = netId;
      }
   }
}
