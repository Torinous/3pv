using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Editor;

namespace PPPV.Net{

   public class SaveEventArgs{
      public string fileName;
      public string netID;

      public SaveEventArgs(string fileName_, string netID_){
         fileName = fileName_;
         netID = netID_;
      }
   }
}
