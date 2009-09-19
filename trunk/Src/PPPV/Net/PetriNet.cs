using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;

//using PPPv.Editor;
using PPPv.Utils;

namespace PPPv.Net {
   public class PetriNet {
      private ArrayList places;
      private ArrayList transitions;
      private ArrayList arcs;
      private ArrayList currentSelectedObjects;
      private Editor.NetCanvas canvas;

      /*События*/
      public event MouseEventHandler MouseClick;
      public event MouseEventHandler MouseMove;
      public event MouseEventHandler MouseDown;
      public event MouseEventHandler MouseUp;
      public event PaintEventHandler Paint;

      public event RegionSelectionEventHandler RegionSelectionStart;
      public event RegionSelectionEventHandler RegionSelectionUpdate;
      public event RegionSelectionEventHandler RegionSelectionEnd;

      public event KeyEventHandler KeyDown;

      public ArrayList CurrentSelected{
         get{
            return currentSelectedObjects;
         }
         private set{
            currentSelectedObjects = value;
         }
      }

      public Editor.NetCanvas Canvas{
         get{
            return canvas;
         }
         set{
            if(canvas != null){
               canvas.CanvasMouseClick      -= CanvasMouseClickHandler;
               canvas.CanvasMouseClick      -= CanvasMouseClickRetranslator;
               canvas.CanvasMouseMove       -= CanvasMouseMoveHandler;
               canvas.CanvasMouseMove       -= CanvasMouseMoveRetranslator;
               canvas.CanvasMouseDown       -= CanvasMouseDownHandler;
               canvas.CanvasMouseDown       -= CanvasMouseDownRetranslator;
               canvas.CanvasMouseUp         -= CanvasMouseUpHandler;
               canvas.CanvasMouseUp         -= CanvasMouseUpRetranslator;
               canvas.Paint                 -= CanvasPaintHandler;
               canvas.Paint                 -= CanvasPaintRetranslator;
               canvas.RegionSelectionStart  -= RegionSelectionStartHandler;
               canvas.RegionSelectionStart  -= RegionSelectionStartRetranslator;
               canvas.RegionSelectionUpdate -= RegionSelectionUpdateHandler;
               canvas.RegionSelectionUpdate -= RegionSelectionUpdateRetranslator;
               canvas.RegionSelectionEnd    -= RegionSelectionEndHandler;
               canvas.RegionSelectionEnd    -= RegionSelectionEndRetranslator;
               canvas.KeyDown               -= CanvasKeyDownRetranslator;
               canvas.KeyDown               -= CanvasKeyDownHandler;
            }

            canvas = value;

            if(canvas != null){
               canvas.CanvasMouseClick      += CanvasMouseClickHandler;
               canvas.CanvasMouseClick      += CanvasMouseClickRetranslator;
               canvas.CanvasMouseMove       += CanvasMouseMoveHandler;
               canvas.CanvasMouseMove       += CanvasMouseMoveRetranslator;
               canvas.CanvasMouseDown       += CanvasMouseDownHandler;
               canvas.CanvasMouseDown       += CanvasMouseDownRetranslator;
               canvas.CanvasMouseUp         += CanvasMouseUpHandler;
               canvas.CanvasMouseUp         += CanvasMouseUpRetranslator;
               canvas.Paint                 += CanvasPaintHandler;
               canvas.Paint                 += CanvasPaintRetranslator;
               canvas.RegionSelectionStart  += RegionSelectionStartHandler;
               canvas.RegionSelectionStart  += RegionSelectionStartRetranslator;
               canvas.RegionSelectionUpdate += RegionSelectionUpdateHandler;
               canvas.RegionSelectionUpdate += RegionSelectionUpdateRetranslator;
               canvas.RegionSelectionEnd    += RegionSelectionEndHandler;
               canvas.RegionSelectionEnd    += RegionSelectionEndRetranslator;
               canvas.KeyDown               += CanvasKeyDownHandler;
               canvas.KeyDown               += CanvasKeyDownRetranslator;
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

      public NetElement ElementPortal{
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

      private void OnRegionSelectionStart(Editor.RegionSelectionEventArgs e){
         if(RegionSelectionStart != null){
            RegionSelectionStart(this,new RegionSelectionEventArgs(e));
         }
      }

      private void OnRegionSelectionUpdate(Editor.RegionSelectionEventArgs e){
         if(RegionSelectionUpdate != null){
            RegionSelectionUpdate(this,new RegionSelectionEventArgs(e));
         }
      }

      private void OnRegionSelectionEnd(Editor.RegionSelectionEventArgs e){
         if(RegionSelectionEnd != null){
            RegionSelectionEnd(this,new RegionSelectionEventArgs(e));
         }
      }

      private void OnKeyDown(KeyEventArgs e){
         if(KeyDown != null){
            KeyDown(this,e);
         }
      }

      private void CanvasMouseClickRetranslator(object sender, Editor.CanvasMouseEventArgs args){
         MouseEventArgs newArgs = new MouseEventArgs(args);
         OnMouseClick(newArgs);
      }

      private void CanvasMouseMoveRetranslator(object sender, Editor.CanvasMouseEventArgs args){
         MouseEventArgs newArgs = new MouseEventArgs(args);
         OnMouseMove(newArgs);
      }

      private void CanvasMouseDownRetranslator(object sender, Editor.CanvasMouseEventArgs args){
         MouseEventArgs newArgs = new MouseEventArgs(args);
         OnMouseDown(newArgs);
      }

      private void CanvasMouseUpRetranslator(object sender, Editor.CanvasMouseEventArgs args){
         MouseEventArgs newArgs = new MouseEventArgs(args);
         OnMouseUp(newArgs);
      }

      private void CanvasPaintRetranslator(object sender, PaintEventArgs args){
         OnPaint(args);
      }

      private void RegionSelectionStartRetranslator(object sender, Editor.RegionSelectionEventArgs args){
         OnRegionSelectionStart(args);
      }

      private void RegionSelectionUpdateRetranslator(object sender, Editor.RegionSelectionEventArgs args){
         OnRegionSelectionUpdate(args);
      }
      private void RegionSelectionEndRetranslator(object sender, Editor.RegionSelectionEventArgs args){
         OnRegionSelectionEnd(args);
      }

      private void CanvasKeyDownRetranslator(object sender, KeyEventArgs arg){
         OnKeyDown(arg);
      }

      private void CanvasMouseClickHandler(object sender, Editor.CanvasMouseEventArgs args){
      }

      private void CanvasMouseMoveHandler(object sender, Editor.CanvasMouseEventArgs args){
      }

      private void CanvasMouseDownHandler(object sender, Editor.CanvasMouseEventArgs args){
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  break;
               case Editor.ToolEnum.Place:
                  AddPlace(args.X,args.Y);
                  (sender as Editor.NetCanvas).Invalidate();
                  break;
               case Editor.ToolEnum.Transition:
                  AddTransition(args.X,args.Y);
                  (sender as Editor.NetCanvas).Invalidate();
                  break;
               case Editor.ToolEnum.Arc:
                  NetElement clicked = NetElementUnder(new Point(args.X,args.Y));
                  clicked = (clicked is Arc) ? null : clicked;
                  if(clicked != null && !HaveUnfinishedArcs())
                     AddArc(clicked);
                  (sender as Editor.NetCanvas).Invalidate();
                  break;
               default:
                  break;
            }
         }
      }

      private void CanvasMouseUpHandler(object sender, Editor.CanvasMouseEventArgs args){
      }

      private void CanvasPaintHandler(object sender, PaintEventArgs args){
      }

      protected void RegionSelectionStartHandler(object sender, Editor.RegionSelectionEventArgs args){
      }

      protected void RegionSelectionUpdateHandler(object sender, Editor.RegionSelectionEventArgs args){
      }

      protected void RegionSelectionEndHandler(object sender, Editor.RegionSelectionEventArgs args){
      }

      private void CanvasKeyDownHandler(object sender, KeyEventArgs arg){
      }

      public NetElement AddPlace(int x, int y) {
         Place tmpPlace = new Place(x,y);
         return ElementPortal = tmpPlace;
      }

      public NetElement AddTransition(int x, int y) {
         Transition tmpTransition = new Transition(x,y);
         return ElementPortal = tmpTransition;
      }

      public NetElement AddArc(NetElement startElement) {
         Arc tmpArc = new Arc(startElement);
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
         a.PrepareToDeletion();
         Arcs.Remove(a);
      }

      public void Delete(Transition t){
         t.PrepareToDeletion();
         Transitions.Remove(t);
      }

      public void Delete(Place p){
         p.PrepareToDeletion();
         Places.Remove(p);
      }

      public void Select(NetElement ob){
         currentSelectedObjects.Add(ob);
      }

      public void Unselect(NetElement ob){
         currentSelectedObjects.Remove(ob);
      }

      public NetElement NetElementUnder(Point _p){
         int i = 0;
         for(i=0;i<Transitions.Count;++i) {
            if(((NetElement)Transitions[i]).IsIntersectWith(_p)){
               return (NetElement)Transitions[i];
            }
         }
         for(i=0;i<Places.Count;++i) {
            if(((NetElement)Places[i]).IsIntersectWith(_p)){
               return (NetElement)Places[i];
            }
         }
         for(i=0;i<Arcs.Count;++i) {
            if(((NetElement)Arcs[i]).IsIntersectWith(_p)){
               return (NetElement)Arcs[i];
            }
         }
         return null;
      }

      public ArrayList NetElementUnder(Rectangle selectedRectangle){
         ArrayList selectedObjects = new ArrayList();
         int i = 0;
         for(i=0;i<Transitions.Count;++i) {
            if(((NetElement)Transitions[i]).IsIntersectWith(selectedRectangle)){
               selectedObjects.Add((NetElement)Transitions[i]);
            }
         }
         for(i=0;i<Places.Count;++i) {
            if(((NetElement)Places[i]).IsIntersectWith(selectedRectangle)){
               selectedObjects.Add((NetElement)Places[i]);
            }
         }
         for(i=0;i<Arcs.Count;++i) {
            if(((NetElement)Arcs[i]).IsIntersectWith(selectedRectangle)){
               selectedObjects.Add((NetElement)Arcs[i]);
            }
         }
         return selectedObjects;
      }

      private bool HaveUnfinishedArcs(){
         for(int i=0;i<Arcs.Count;++i) {
            if(((Arc)Arcs[i]).Unfinished){
               return true;
            }
         }
         return false;
      }

      public bool HaveArcBetween(NetElement from_, NetElement to_){
         for(int i=0;i<Arcs.Count;++i) {
            if((Arcs[i] as Arc).From == from_ && (Arcs[i] as Arc).To == to_){
               return true;
            }
         }
         return false;
      }
   }
}
