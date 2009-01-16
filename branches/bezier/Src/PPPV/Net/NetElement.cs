
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
               parent.MouseMove             -= this.MouseMoveHandler;
               parent.MouseClick            -= this.MouseClickHandler;
               parent.MouseUp               -= this.MouseUpHandler;
               parent.MouseDown             -= this.MouseDownHandler;
               parent.RegionSelectionStart  -= this.RegionSelectionStartHandler;
               parent.RegionSelectionUpdate -= this.RegionSelectionUpdateHandler;
               parent.RegionSelectionEnd    -= this.RegionSelectionEndHandler;
               parent.KeyDown               -= this.KeyDownHandler;
            }
            parent = value;
            if(parent != null){
               parent.Paint                 += this.Draw;
               parent.MouseMove             += this.MouseMoveHandler;
               parent.MouseClick            += this.MouseClickHandler;
               parent.MouseUp               += this.MouseUpHandler;
               parent.MouseDown             += this.MouseDownHandler;
               parent.RegionSelectionStart  += this.RegionSelectionStartHandler;
               parent.RegionSelectionUpdate += this.RegionSelectionUpdateHandler;
               parent.RegionSelectionEnd    += this.RegionSelectionEndHandler;
               parent.KeyDown               += this.KeyDownHandler;
            }
         }
      }

      public NetElement(){
         this.SelectionChange += this.SelectionChangeHandler;
      }

      protected void SelectionChangeHandler(object sender, SelectionChangeEventArgs args){
         if(args.newState)
            ParentNet.Select(this);
         if(!args.newState)
            ParentNet.Unselect(this);

      }
   }
}

