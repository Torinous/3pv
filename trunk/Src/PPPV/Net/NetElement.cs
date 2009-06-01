
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PPPv.Net {
   public abstract class NetElement : GraphicalElement {

      protected PetriNet parent;

      public PetriNet ParentNet{
         get{
            return parent;
         }
         set{
            if(parent != null){
               parent.Paint                 -= this.Draw;
               parent.Paint                 -= this.PaintRetranslator;
               parent.MouseMove             -= this.MouseMoveHandler;
               parent.MouseMove             -= this.MouseMoveRetranslator;
               parent.MouseClick            -= this.MouseClickHandler;
               parent.MouseUp               -= this.MouseUpHandler;
               parent.MouseDown             -= this.MouseDownHandler;
               parent.MouseDown             -= this.MouseDownRetranslator;
               parent.RegionSelectionStart  -= this.RegionSelectionStartHandler;
               parent.RegionSelectionUpdate -= this.RegionSelectionUpdateHandler;
               parent.RegionSelectionEnd    -= this.RegionSelectionEndHandler;
               parent.KeyDown               -= this.KeyDownHandler;
            }
            parent = value;
            if(parent != null){
               parent.Paint                 += this.Draw;
               parent.Paint                 += this.PaintRetranslator;
               parent.MouseMove             += this.MouseMoveHandler;
               parent.MouseMove             += this.MouseMoveRetranslator;
               parent.MouseClick            += this.MouseClickHandler;
               parent.MouseUp               += this.MouseUpHandler;
               parent.MouseDown             += this.MouseDownHandler;
               parent.MouseDown             += this.MouseDownRetranslator;
               parent.RegionSelectionStart  += this.RegionSelectionStartHandler;
               parent.RegionSelectionUpdate += this.RegionSelectionUpdateHandler;
               parent.RegionSelectionEnd    += this.RegionSelectionEndHandler;
               parent.KeyDown               += this.KeyDownHandler;
            }
         }
      }

      public NetElement(int x_, int y_, int width_, int height_, bool sizeable_):base(x_, y_, width_, height_, sizeable_){
         this.SelectionChange += this.SelectionChangeHandler;
      }

      protected void SelectionChangeHandler(object sender, SelectionChangeEventArgs args){
         if(args.newState)
            ParentNet.Select(this);
         if(!args.newState)
            ParentNet.Unselect(this);

      }

      protected override void MouseClickHandler(object sender, MouseEventArgs args){
         base.MouseClickHandler(sender,args);
      }

      protected override void MouseMoveHandler(object sender, MouseEventArgs args){
         base.MouseMoveHandler(sender,args);
         (sender as PetriNet).Canvas.Invalidate();
      }

      protected override void MouseDownHandler(object sender, MouseEventArgs args){
         base.MouseDownHandler(sender,args);
         (sender as PetriNet).Canvas.Invalidate();
      }

      protected override void MouseUpHandler(object sender, MouseEventArgs args){
         base.MouseUpHandler(sender,args);
         (sender as PetriNet).Canvas.Invalidate();
      }

      protected override void RegionSelectionStartHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void RegionSelectionUpdateHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void RegionSelectionEndHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void KeyDownHandler(object sender, KeyEventArgs arg){
      }
      /*Вся внутренняя подготовка перед удалением элемента сети*/
      public override void PrepareToDeletion(){
         /*Отпишемся от всех событый*/
         ParentNet = null;
      }
   }
}

