using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;

using PPPv.Editor;
using PPPv.Utils;


namespace PPPv.Net {
   public class PetriNet {
      private ArrayList places;
      private ArrayList transitions;
      private ArrayList arcs;
      private ArrayList currentSelectedObjects;
      private NetCanvas canvas;

      /*События*/
      public event MouseEventHandler MouseClick;
      public event MouseEventHandler MouseMove;
      public event MouseEventHandler MouseDown;
      public event MouseEventHandler MouseUp;
      public event PaintEventHandler Paint;

      public ArrayList CurrentSelected{
         get{
            return currentSelectedObjects;
         }
         private set{
            currentSelectedObjects = value;
         }
      }

      public NetCanvas Canvas{
         get{
            return canvas;
         }
         set{
            if(canvas != null){
               canvas.CanvasMouseClick -= CanvasMouseClickRetranslator;
               canvas.CanvasMouseClick -= CanvasMouseClickHandler;
               canvas.CanvasMouseMove  -= CanvasMouseMoveRetranslator;
               canvas.CanvasMouseMove  -= CanvasMouseMoveHandler;
               canvas.CanvasMouseDown  -= CanvasMouseDownRetranslator;
               canvas.CanvasMouseDown  -= CanvasMouseDownHandler;
               canvas.CanvasMouseUp    -= CanvasMouseUpRetranslator;
               canvas.CanvasMouseUp    -= CanvasMouseUpHandler;
               canvas.Paint            -= CanvasPaintRetranslator;
               canvas.Paint            -= CanvasPaintHandler;
            }

            canvas = value;

            if(canvas != null){
               canvas.CanvasMouseClick += CanvasMouseClickRetranslator;
               canvas.CanvasMouseClick += CanvasMouseClickHandler;
               canvas.CanvasMouseMove  += CanvasMouseMoveRetranslator;
               canvas.CanvasMouseMove  += CanvasMouseMoveHandler;
               canvas.CanvasMouseDown  += CanvasMouseDownRetranslator;
               canvas.CanvasMouseDown  += CanvasMouseDownHandler;
               canvas.CanvasMouseUp    += CanvasMouseUpRetranslator;
               canvas.CanvasMouseUp    += CanvasMouseUpHandler;
               canvas.Paint            += CanvasPaintRetranslator;
               canvas.Paint            += CanvasPaintHandler;
            }
         }
      }

      public ArrayList Places{
         get{
            return places;
         }
         private set{
            places = value;
         }
      }

      public ArrayList Transitions{
         get{
            return transitions;
         }
         private set{
            transitions = value;
         }
      }

      public ArrayList Arcs{
         get{
            return arcs;
         }
         private set{
            arcs = value;
         }
      }

      public BaseNetElement ElementPortal{
         set{
            if(value is Place){
               Places.Add(value);
            }
            if(value is Transition){
               Transitions.Add(value);
            }
            if(value is Arc){
               Arcs.Add(value);
            }
            value.ParentNet = this;
         }
      }

      private void OnMouseClick(MouseEventArgs e){
         if(MouseClick != null){
            MouseClick(this,e);
         }
      }

      private void OnMouseMove(MouseEventArgs e){
         if(MouseMove != null){
            MouseMove(this,e);
         }
      }

      private void OnMouseDown(MouseEventArgs e){
         if(MouseDown != null){
            MouseDown(this,e);
         }
      }

      private void OnMouseUp(MouseEventArgs e){
         if(MouseUp != null){
            MouseUp(this,e);
         }
      }

      private void OnPaint(PaintEventArgs e){
         if(Paint != null){
            using(PreciseTimer pr = new PreciseTimer("PetriNet.Draw")){
               Paint(this,e);
            }
         }
      }

      private void CanvasMouseClickRetranslator(object sender, CanvasMouseEventArgs args){
         MouseEventArgs newArgs = new MouseEventArgs(args);
         OnMouseClick(newArgs);
      }

      private void CanvasMouseMoveRetranslator(object sender, CanvasMouseEventArgs args){
         MouseEventArgs newArgs = new MouseEventArgs(args);
         OnMouseMove(newArgs);
      }

      private void CanvasMouseDownRetranslator(object sender, CanvasMouseEventArgs args){
         MouseEventArgs newArgs = new MouseEventArgs(args);
         OnMouseDown(newArgs);
      }

      private void CanvasMouseUpRetranslator(object sender, CanvasMouseEventArgs args){
         MouseEventArgs newArgs = new MouseEventArgs(args);
         OnMouseUp(newArgs);
      }

      private void CanvasPaintRetranslator(object sender, PaintEventArgs args){
         OnPaint(args);
      }

      private void CanvasMouseClickHandler(object sender, CanvasMouseEventArgs args){
      }

      private void CanvasMouseMoveHandler(object sender, CanvasMouseEventArgs args){
      }

      private void CanvasMouseDownHandler(object sender, CanvasMouseEventArgs args){
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case ToolEnum.Pointer:
                  break;
               case ToolEnum.Place:
                  AddPlace(args.X,args.Y);
                  (sender as NetCanvas).Invalidate();
                  break;
               case ToolEnum.Transition:
                  AddTransition(args.X,args.Y);
                  (sender as NetCanvas).Invalidate();
                  break;
               case ToolEnum.Arc:
                  break;
               default:
                  break;
            }
         }
      }

      private void CanvasMouseUpHandler(object sender, CanvasMouseEventArgs args){
      }

      private void CanvasPaintHandler(object sender, PaintEventArgs args){
      }

      public BaseNetElement AddPlace(int x, int y) {
         Place tmpPlace = new Place(x,y);
         return ElementPortal = tmpPlace;
      }

      public BaseNetElement AddTransition(int x, int y) {
         Transition tmpTransition = new Transition(x,y);
         return ElementPortal = tmpTransition;
      }

      public BaseNetElement AddArc(BaseNetElement startElement, NetCanvas netCanvas) {
         Arc tmpArc = new Arc(startElement,netCanvas);
         return ElementPortal = tmpArc;
      }

      /*Конструктор*/
      public PetriNet() {
         Places = new ArrayList(30);
         Transitions = new ArrayList(30);
         Arcs = new ArrayList(60);
         currentSelectedObjects = new ArrayList(50);
      }

      public void Delete(Arc a){
         Arcs.Remove(a);
      }

      public void Delete(Transition t){
         Transitions.Remove(t);
      }

      public void Delete(Place p){
         Places.Remove(p);
      }

      public void Select(BaseNetElement ob){
         currentSelectedObjects.Add(ob);
      }

      public void Unselect(BaseNetElement ob){
         currentSelectedObjects.Remove(ob);
      }

      public BaseNetElement NetElementUnder(Point _p){
         int i = 0;
         for(i=0;i<Transitions.Count;++i) {
            if(((BaseNetElement)Transitions[i]).IsIntersectWith(_p)){
               return (BaseNetElement)Transitions[i];
            }
         }
         for(i=0;i<Places.Count;++i) {
            if(((BaseNetElement)Places[i]).IsIntersectWith(_p)){
               return (BaseNetElement)Places[i];
            }
         }
         for(i=0;i<Arcs.Count;++i) {
            if(((BaseNetElement)Arcs[i]).IsIntersectWith(_p)){
               return (BaseNetElement)Arcs[i];
            }
         }
         return null;
      }

      public ArrayList NetElementUnder(Rectangle selectedRectangle){
         ArrayList selectedObjects = new ArrayList();
         int i = 0;
         for(i=0;i<Transitions.Count;++i) {
            if(((BaseNetElement)Transitions[i]).IsIntersectWith(selectedRectangle)){
               selectedObjects.Add((BaseNetElement)Transitions[i]);
            }
         }
         for(i=0;i<Places.Count;++i) {
            if(((BaseNetElement)Places[i]).IsIntersectWith(selectedRectangle)){
               selectedObjects.Add((BaseNetElement)Places[i]);
            }
         }
         for(i=0;i<Arcs.Count;++i) {
            if(((BaseNetElement)Arcs[i]).IsIntersectWith(selectedRectangle)){
               selectedObjects.Add((BaseNetElement)Arcs[i]);
            }
         }
         return selectedObjects;
      }
   }
}
