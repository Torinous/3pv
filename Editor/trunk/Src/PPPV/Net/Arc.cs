using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using PPPV.Utils;
using PPPV.Editor;

namespace PPPV.Net {
   public class Arc : NetElement, IXmlSerializable {
      protected static Pen ArrowedBlackPen = ArrowedBlackPenFactory();
      private NetElement source, target;
      private Point sourcePilon, targetPilon;
      private CortegeList cortege;
      private ArrayList points;

      /*Конструктор*/
      public Arc(NetElement startElement):base(0, 0, 0, 0, false) {
         Source = startElement;
         if(Source != null)
            targetPilon = Source.Center;
         Points = new ArrayList(20);
         Cortege = new CortegeList(10);
      }

      public Arc(XmlReader reader, PetriNet net):this((NetElement)null){
         ParentNet = net;
         this.ReadXml(reader);
      }

      public string ID{
         get {
            string source_="", target_="";
            if(Source != null)
               source_ = Source.ID;
            if(Target != null)
               target_ = Target.ID;
            return source_+" to "+target_;
         }
      }
      
      public ArrayList Points{
         get{
            return points;
         }
         set{
            points = value;
            OnChange(new EventArgs());
         }
      }

      public CortegeList Cortege{
         get{
            return cortege;
         }
         set{
            if(cortege != null){
               cortege.Change -= CortegeChangeHandler;
            }
            cortege = value;
            if(cortege != null){
               cortege.Change += CortegeChangeHandler;
            }
            OnChange(new EventArgs());
         }
      }

      private Point SourcePilon{
         get{
            return sourcePilon;
         }
         set{
            sourcePilon = value;
            UpdateHitRegion();
            OnChange(new EventArgs());
         }
      }

      private Point TargetPilon{
         get{
            return targetPilon;
         }
         set{
            targetPilon = value;
            UpdateHitRegion();
            OnChange(new EventArgs());
         }
      }

      public NetElement Target{
         get{
            return target;
         }
         set{
            if(target != null){
               target.Move -= MoveHandler;
               target.Resize -= ResizeLinkedElementsHandler;
            }
            target = value;
            if(target != null){
               UpdatePosition();
               target.Move += MoveHandler;
               target.Resize += ResizeLinkedElementsHandler;
            }
            OnChange(new EventArgs());
         }
      }

      public NetElement Source{
         get{
            return source;
         }
         set{
            if(source != null){
               source.Move -= MoveHandler;
               source.Resize -= ResizeLinkedElementsHandler;
            }
            source = value;
            if(source != null){
               sourcePilon = source.Center;
               source.Move += MoveHandler;
               source.Resize += ResizeLinkedElementsHandler;
            }
            OnChange(new EventArgs());
         }
      }

      public bool Unfinished{
         get{
            return target == null;
         }
      }

      public override Point Center{
         get{
            if(Points.Count == 0){
               return new Point((sourcePilon.X+targetPilon.X)/2,(sourcePilon.Y+targetPilon.Y)/2);
            }else{
               if(Points.Count%2 == 1){
                  Pilon p1 = (Pilon)Points[( (Points.Count+1)/2)-1 ];
                  return new Point(p1.X, p1.Y);
               }else{
                  Pilon p1,p2;
                  p1 = (Pilon)Points[( (Points.Count)/2)-1 ];
                  p2 = (Pilon)Points[( (Points.Count)/2+1)-1 ];
                  return new Point((p1.X + p2.X)/2, (p1.Y + p2.Y)/2);
               }
            }
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
         Font font1 = new Font(fF_Arial, 16, FontStyle.Regular, GraphicsUnit.Pixel);

         if( Points.Count == 0 ){
            dc.DrawLine(ArrowedBlackPen, sourcePilon, targetPilon);
         }else{
            dc.DrawLine(blackPen, sourcePilon,(Points[0] as Pilon).Location);
            for(int i = 1;i < Points.Count; ++i){
               dc.DrawLine(blackPen, (Points[i-1] as Pilon).Location, (Points[i] as Pilon).Location);
            }
            dc.DrawLine(ArrowedBlackPen, (Points[Points.Count-1] as Pilon).Location,targetPilon);
         }
         dc.DrawString(Cortege.Text, font1, blackBrush, Center.X, Center.Y-15);
      }

      protected override void ShowSelectionMarker(Graphics dc){
         dc.SmoothingMode = SmoothingMode.HighQuality;
         SolidBrush redBrush = new SolidBrush(Color.FromArgb(100,250,50,50));
         dc.FillRegion(redBrush, HitRegion);
      }


      private void MoveHandler(object sender, MoveEventArgs args){
         UpdatePosition();
         OnChange(new EventArgs());
      }

      private void OneOfPointMoveHandler(object sender, MoveEventArgs args){
         UpdatePosition();
         OnChange(new EventArgs());
      }

      private void ResizeLinkedElementsHandler(object sender, ResizeEventArgs args){
         UpdatePosition();
         OnChange(new EventArgs());
      }

      private void UpdatePosition(){
         UpdateHitRegion();
         if(Points.Count == 0)
            sourcePilon = source.GetPilon(target.Center,this.ParentNet.Canvas);
         else
            sourcePilon = source.GetPilon((Points[0] as Pilon).Center,this.ParentNet.Canvas);

         if(Points.Count == 0)
            targetPilon = target.GetPilon(source.Center,this.ParentNet.Canvas);
         else
            targetPilon = target.GetPilon((Points[Points.Count-1] as Pilon).Center,this.ParentNet.Canvas);
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
                  targetPilon = new Point(args.X,args.Y);
                  if(Points.Count == 0)
                     sourcePilon = Source.GetPilon(targetPilon,this.ParentNet.Canvas);
                  else
                     sourcePilon = Source.GetPilon((Points[0] as Pilon).Center,this.ParentNet.Canvas);
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
                        if(Source.GetType() != clicked.GetType()){
                           if(!ParentNet.HaveArcBetween(Source,clicked)){
                              Target = clicked;
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
         OnChange(new EventArgs());
      }

      private void DeletePoint(Pilon p){
         Points.Remove(p);
         p.Move -= OneOfPointMoveHandler;
         OnChange(new EventArgs());
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
            if(Target == null){
               parent.ElementNullPortal = this;
               (sender as PetriNet).Canvas.Invalidate();//TODO: полный Invalidate это нехорошо!!!
            }
         }
      }

      protected override void UpdateHitRegion(){
         using(PreciseTimer pr = new PreciseTimer("Arc.UpdateRegion")){
            if(!Unfinished){
               HitRegion.MakeEmpty();
               GraphicsPath tmpPath = new GraphicsPath();

               Point lastPoint = this.SourcePilon;
               foreach(Pilon p in Points){
                  tmpPath.AddLine(lastPoint.X, lastPoint.Y, p.X, p.Y);
                  lastPoint = p.Location;
               }
               tmpPath.AddLine(lastPoint.X, lastPoint.Y, TargetPilon.X, TargetPilon.Y);

               tmpPath.Widen(new Pen(Color.Red, 4));

               HitRegion.Union(tmpPath);
            }
         }
      }

      /*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
      public override Point GetPilon(Point source, NetCanvas on){
         return Center;
      }

      public override void PrepareToDeletion(){
         Source = null;
         Target = null;
         base.PrepareToDeletion();
      }

      private void CortegeChangeHandler(object sender, EventArgs args){
         OnChange(args);
      }

      public void WriteXml (XmlWriter writer)
      {
         int i = 0;
         writer.WriteAttributeString("id",this.ID);
         writer.WriteAttributeString("source", this.Source.Name);
         writer.WriteAttributeString("target", this.Target.Name);
         foreach(Pilon p in Points){
            writer.WriteStartElement("arcpath");
            writer.WriteAttributeString("id",  String.Format("{0:000}",i));
            writer.WriteAttributeString("x", p.X.ToString());
            writer.WriteAttributeString("y", p.Y.ToString());
            writer.WriteAttributeString("curvePoint", "false");
            writer.WriteEndElement(); // arcpath
            i++;
         }
         writer.WriteStartElement("cortege");
         this.cortege.WriteXml(writer);
         writer.WriteEndElement(); // cortege
      }

      public void ReadXml (XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.Read();
         reader.MoveToAttribute("id");
         reader.MoveToAttribute("source");
         this.Source = parent.GetElementByID(reader.Value);
         reader.MoveToAttribute("target");
         this.Target = parent.GetElementByID(reader.Value);
         reader.ReadStartElement("arc");
         while(reader.NodeType != XmlNodeType.EndElement)
         {
            switch(reader.Name){
               case "cortege":
                  subTreeReader = reader.ReadSubtree();
                  Cortege.ReadXml(subTreeReader);
                  subTreeReader.Close();
                  reader.Skip();
               break;
               case "arcpath":
                  Points.Add(new Pilon(int.Parse(reader.GetAttribute("x")),int.Parse(reader.GetAttribute("y")),(GraphicalElement)this));
                  reader.MoveToAttribute("curvePoint");
                  reader.MoveToElement();
                  reader.Skip();
                  
               break;
               default:
                  reader.Read();
               break;
            }
         }
      }

      public XmlSchema GetSchema()
      {
         return(null);
      }
   } // Arc
} // namespace
