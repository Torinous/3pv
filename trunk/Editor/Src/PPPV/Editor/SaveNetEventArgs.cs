/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 26.08.2010
 * Time: 22:01
 */

namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor;

   public class SaveNetEventArgs : EventArgs
   {
      private string filePath;
      private string netId;

      public SaveNetEventArgs(PetriNetGraphical net)
      {
         this.filePath = net.FileOfNetPath;
         this.netId = net.Id;
      }

      public string NetId
      {
         get { return this.netId; }
         set { this.netId = value; }
      }

      public string FilePath
      {
         get { return this.filePath; }
         set { this.filePath = value; }
      }
   }
}