using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PPPv.Net {
   public abstract class NetElementElement: GraphicalElement {

      protected GraphicalElement parent;

      public GraphicalElement ParentNetElement{
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

      public NetElementElement(int x_, int y_, int width_, int height_, bool sizeable_):base(x_, y_, width_, height_, sizeable_){
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

