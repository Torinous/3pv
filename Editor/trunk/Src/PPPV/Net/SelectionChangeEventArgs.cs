using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Net{

  public class SelectionChangeEventArgs{
      public bool newState;

      /*Конструкторы*/

      public SelectionChangeEventArgs(bool state){
         newState = state;
      }
   }
}
