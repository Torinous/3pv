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
      private NetCanvas canvas;

      public NetCanvas Canvas{
         get{
            return canvas;
         }
         set{
            canvas = value;
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

      public BaseNetElement AddPlace(int x, int y) {
         Place tmpPlace = new Place(x,y);
         Places.Add(tmpPlace);
         tmpPlace.ParentNet = this;
         return tmpPlace;
      }

      public BaseNetElement AddTransition(int x, int y) {
         Transition tmpTransition = new Transition(x,y);
         Transitions.Add(tmpTransition);
         tmpTransition.ParentNet = this;
         return tmpTransition;
      }

      public BaseNetElement AddArc(BaseNetElement startElement, NetCanvas netCanvas) {
         Arc tmpArc = new Arc(startElement,netCanvas);
         Arcs.Add(tmpArc);
         tmpArc.ParentNet = this;
         return tmpArc;
      }

      /*Конструктор*/
      public PetriNet() {
         Places = new ArrayList(30);
         Transitions = new ArrayList(30);
         Arcs = new ArrayList(60);
      }

      public void Draw(object sender, PaintEventArgs e) {
         using(PreciseTimer pr = new PreciseTimer("PetriNet.Draw")){
            Pen RedPen = new Pen(Color.Red, 1);
            Graphics dc = e.Graphics;
            dc.SmoothingMode = SmoothingMode.HighQuality;
            int i;
            for(i=Arcs.Count-1;i>=0;--i) {
               ((IDrawable)Arcs[i]).Draw(dc);
            }
            for(i=Places.Count-1;i>=0;--i) {
               ((IDrawable)Places[i]).Draw(dc);
            }
            for(i=Transitions.Count-1;i>=0;--i) {
               ((IDrawable)Transitions[i]).Draw(dc);
            }
         }
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
