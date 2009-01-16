
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PPPv.Net {
   public abstract class NetElementElement: GraphicalElement {

      protected NetElement parent;

      public NetElement ParentNetElement{
         get{
            return parent;
         }
         set{
            if(parent != null){
               parent.Paint                 -= this.Draw;
               parent.MouseMove             -= this.MouseMoveHandler;
               parent.MouseDown             -= this.MouseDownHandler;
               parent.Move                  -= this.MoveHandler;
            }
            parent = value;
            if(parent != null){
               parent.Paint                 += this.Draw;
               parent.MouseMove             += this.MouseMoveHandler;
               parent.MouseDown             += this.MouseDownHandler;
               parent.Move                  += this.MoveHandler;
            }
         }
      }

      /*Вся внутренняя подготовка перед удалением элемента сети*/
      public override void PrepareToDeletion(){
         /*Отпишемся от всех событый*/
         ParentNetElement = null;
      }

      protected override void ShowSelectionMarker(Graphics dc){

      }

      protected virtual void MoveHandler(object sender, MoveEventArgs args){

      }
   }
}

