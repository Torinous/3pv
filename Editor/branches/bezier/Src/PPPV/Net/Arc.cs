using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;

using PPPv.Utils;
//using PPPv.Editor;

namespace PPPv.Net {
   public class Arc : NetElement {
      private static int _ID = 0;
      protected static Pen ArrowedBlackPen = ArrowedBlackPenFactory();
      private NetElement from,to;
      private Point fromPilon,toPilon;
      private string cortege;
      private ArrayList points;

      public ArrayList Points{
         get{
            return points;
         }
         set{
            points = value;
         }
      }

      public string Cortege{
         get{
            return cortege;
         }
         set{
            cortege = value;
         }
      }

      private Point FromPilon{
         get{
            return fromPilon;
         }
         set{
            fromPilon = value;
            UpdateHitRegion();
         }
      }

      private Point ToPilon{
         get{
            return toPilon;
         }
         set{
            toPilon = value;
            UpdateHitRegion();
         }
      }

      public NetElement To{
         get{
            return to;
         }
         set{
            if(to != null){
               to.Move -= MoveHandler;
               to.Resize -= ResizeLinkedElementsHandler;
            }
            to = value;
            if(to != null){
               UpdatePosition();
               to.Move += MoveHandler;
               to.Resize += ResizeLinkedElementsHandler;
            }
         }
      }

      public NetElement From{
         get{
            return from;
         }
         set{
            if(from != null){
               from.Move -= MoveHandler;
               from.Resize -= ResizeLinkedElementsHandler;
            }
            from = value;
            if(from != null){
               FromPilon = from.Center;
               from.Move += MoveHandler;
               from.Resize += ResizeLinkedElementsHandler;
            }
         }
      }

      public bool Unfinished{
         get{
            return To == null;
         }
      }

      /*Конструктор*/
      public Arc(NetElement startElement):base(0, 0, 0, 0, false) {
         _ID++;
         Name = "A"+_ID;
         From = startElement;
         ToPilon = From.Center;
         cortege = "2X";
         Points = new ArrayList(20);
      }

      public override Point Center{
         get{
            return new Point((FromPilon.X+ToPilon.X)/2,(FromPilon.Y+ToPilon.Y)/2);
         }
      }

      public override void Draw(object sender, PaintEventArgs e){

         base.Draw(sender,e);

         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));
         Pen RedPen = new Pen(Color.FromArgb(255,255,0,0));

         /*Кисти*/
         SolidBrush grayBrush = new SolidBrush(Color.FromArgb(200,100,100,100));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200,0,0,0));
         /*Шрифт*/
         FontFamily fF_Arial = new FontFamily("Arial");
         Font font1 = new Font(fF_Arial,16,FontStyle.Regular,GraphicsUnit.Pixel);

         if( Points.Count == 0 ){
            dc.DrawLine(ArrowedBlackPen,FromPilon,ToPilon);
            dc.DrawString(cortege,font1,blackBrush,Center.X,Center.Y+5);
         }else{
            dc.DrawLine(blackPen, FromPilon,(Points[0] as Pilon).Location);
            for(int i = 1;i < Points.Count; ++i){
               dc.DrawLine(blackPen, (Points[i-1] as Pilon).Location, (Points[i] as Pilon).Location);
            }
            dc.DrawLine(ArrowedBlackPen, (Points[Points.Count-1] as Pilon).Location,ToPilon);
            dc.DrawString(cortege, font1, blackBrush, Center.X, Center.Y+5);
         }
      }

      /*private override void ShowSelectionMarker(Graphics dc){

      }*/


      private void MoveHandler(object sender, MoveEventArgs args){
         UpdatePosition();
      }

      private void OneOfPointMoveHandler(object sender, MoveEventArgs args){
         UpdatePosition();
      }

      private void ResizeLinkedElementsHandler(object sender, ResizeEventArgs args){
         UpdatePosition();
      }

      private void UpdatePosition(){
         UpdateHitRegion();
         if(Points.Count == 0)
            FromPilon = from.GetPilon(to.Center,this.ParentNet.Canvas.CreateGraphics());
         else
            FromPilon = from.GetPilon((Points[0] as Pilon).Center,this.ParentNet.Canvas.CreateGraphics());

         if(Points.Count == 0)
            ToPilon = to.GetPilon(from.Center,this.ParentNet.Canvas.CreateGraphics());
         else
            ToPilon = to.GetPilon((Points[Points.Count-1] as Pilon).Center,this.ParentNet.Canvas.CreateGraphics());
      }

      protected override void MouseClickHandler(object sender, MouseEventArgs args){
      }

      protected override void MouseMoveHandler(object sender, MouseEventArgs args){
         switch(args.currentTool){
            case Editor.ToolEnum.Pointer:
               break;
            case Editor.ToolEnum.Place:
               break;
            case Editor.ToolEnum.Transition:
               break;
            case Editor.ToolEnum.Arc:
               if(Unfinished){
                  ToPilon = new Point(args.X,args.Y);
                  if(Points.Count == 0)
                     FromPilon = from.GetPilon(ToPilon,this.ParentNet.Canvas.CreateGraphics());
                  else
                     FromPilon = from.GetPilon((Points[0] as Pilon).Center,this.ParentNet.Canvas.CreateGraphics());
                  (sender as PetriNet).Canvas.Invalidate();
               }
               break;
            default:
               break;
         }
      }

      protected override void MouseDownHandler(object sender, MouseEventArgs args){
         base.MouseDownHandler(sender,args);
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  break;
               case Editor.ToolEnum.Place:
                  break;
               case Editor.ToolEnum.Transition:
                  break;
               case Editor.ToolEnum.Arc:
                  if(Unfinished){
                     NetElement clicked = parent.NetElementUnder(new Point(args.X,args.Y));
                     clicked = (clicked is Arc) ? null : clicked;
                     if(clicked != null){
                        if(From.GetType() != clicked.GetType()){
                           if(!ParentNet.HaveArcBetween(From,clicked)){
                              To = clicked;
                           }
                        }
                     }else{
                        AddPoint( new Pilon(args.X,args.Y,(this as GraphicalElement)));
                     }
                     (sender as PetriNet).Canvas.Invalidate();
                  }
                  break;
               default:
                  break;
            }
         }
      }

      private void AddPoint(Pilon p){
         Points.Add(p);
         p.Move += OneOfPointMoveHandler;
      }

      private void DeletePoint(Pilon p){
         Points.Remove(p);
         p.Move -= OneOfPointMoveHandler;
      }

      protected override void MouseUpHandler(object sender, MouseEventArgs args){
      }

      protected override void RegionSelectionStartHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void RegionSelectionUpdateHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void RegionSelectionEndHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void KeyDownHandler(object sender, KeyEventArgs arg){
         if(arg.KeyCode == Keys.Escape){
            if(to == null){
               parent.Delete(this);
               (sender as PetriNet).Canvas.Invalidate();//TODO: полный Invalidate это нехорошо!!!
            }
         }
      }

      protected override void UpdateHitRegion(){
         using(PreciseTimer pr = new PreciseTimer("Arc.UpdateRegion")){
            HitRegion.MakeEmpty();
            GraphicsPath tmpPath = new GraphicsPath();

            Point point1 = new Point(FromPilon.X,FromPilon.Y-1);
            Point point2 = new Point(ToPilon.X,ToPilon.Y-1);
            tmpPath.AddLine(point1,point2);

            point1 = new Point(ToPilon.X,ToPilon.Y-1);
            point2 = new Point(toPilon.X,ToPilon.Y+1);
            tmpPath.AddLine(point1,point2);

            point1 = new Point(ToPilon.X,ToPilon.Y+1);
            point2 = new Point(FromPilon.X,FromPilon.Y+1);
            tmpPath.AddLine(point1,point2);

            point1 = new Point(FromPilon.X,FromPilon.Y+1);
            point2 = new Point(FromPilon.X,FromPilon.Y-1);
            tmpPath.AddLine(point1,point2);

            tmpPath.Widen(new Pen(Color.Red,3));

            HitRegion.Union(tmpPath);
         }
      }

      /*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
      public override Point GetPilon(Point from,Graphics on){
         return Center;
      }

      public override void PrepareToDeletion(){
         From = null;
         To = null;
         base.PrepareToDeletion();
      }

   }
}
