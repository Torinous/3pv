using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPv.Net{

  public class SelectionChangeEventArgs{
      public bool newState;

      /*Конструкторы*/

      public SelectionChangeEventArgs(bool state){
         newState = state;
      }
   }
}
