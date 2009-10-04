using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using PPPV.Editor;
using PPPV.Utils;

namespace PPPV.Net {
   [Serializable()]
   [XmlRoot("pnml")]
   public partial class PetriNet:IXmlSerializable {
      private string id;
      private string type;
      /*Путь к файлу в который сохранена сеть*/
      private string linkedFile;
      private ArrayList places;
      private ArrayList transitions;
      private ArrayList arcs;
      /*Флаг, было ли сохранено текущее состояние сети*/
      private bool saved;

      private ArrayList currentSelectedObjects;

      private Editor.NetCanvas canvas;

      /*Конструктор*/
      public PetriNet() {
         Saved = false;
         LinkedFile = "";
         ID = "";
         Type = "PPr/T net";
         Places = new ArrayList(30);
         Transitions = new ArrayList(30);
         Arcs = new ArrayList(60);
         currentSelectedObjects = new ArrayList(50);
      }

      /*Свойства*/
      public string ID{
         get {
            return id;
         }
         private set{
            id = value;
         }
      }
      
      public bool Saved{
         get {
            return saved;
         }
         private set{
            saved = value;
         }
      }

      public string LinkedFile{
         get {
            return linkedFile;
         }
         set{
            linkedFile = value;
         }
      }

      public string Type{
         get {
            return type;
         }
         private set{
            type = value;
         }
      }
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
               canvas.CanvasMouseClick       -= CanvasMouseClickHandler;
               canvas.CanvasMouseClick       -= CanvasMouseClickRetranslator;
               canvas.CanvasMouseMove        -= CanvasMouseMoveHandler;
               canvas.CanvasMouseMove        -= CanvasMouseMoveRetranslator;
               canvas.CanvasMouseDown        -= CanvasMouseDownHandler;
               canvas.CanvasMouseDown        -= CanvasMouseDownRetranslator;
               canvas.CanvasMouseUp          -= CanvasMouseUpHandler;
               canvas.CanvasMouseUp          -= CanvasMouseUpRetranslator;
               canvas.Paint                  -= CanvasPaintHandler;
               canvas.Paint                  -= CanvasPaintRetranslator;
               canvas.RegionSelectionStart   -= RegionSelectionStartHandler;
               canvas.RegionSelectionStart   -= RegionSelectionStartRetranslator;
               canvas.RegionSelectionUpdate  -= RegionSelectionUpdateHandler;
               canvas.RegionSelectionUpdate  -= RegionSelectionUpdateRetranslator;
               canvas.RegionSelectionEnd     -= RegionSelectionEndHandler;
               canvas.RegionSelectionEnd     -= RegionSelectionEndRetranslator;
               canvas.KeyDown                -= CanvasKeyDownRetranslator;
               canvas.KeyDown                -= CanvasKeyDownHandler;
               canvas.Load                   -= LoadHandler;
            }

            canvas = value;

            if(canvas != null){
               canvas.CanvasMouseClick       += CanvasMouseClickHandler;
               canvas.CanvasMouseClick       += CanvasMouseClickRetranslator;
               canvas.CanvasMouseMove        += CanvasMouseMoveHandler;
               canvas.CanvasMouseMove        += CanvasMouseMoveRetranslator;
               canvas.CanvasMouseDown        += CanvasMouseDownHandler;
               canvas.CanvasMouseDown        += CanvasMouseDownRetranslator;
               canvas.CanvasMouseUp          += CanvasMouseUpHandler;
               canvas.CanvasMouseUp          += CanvasMouseUpRetranslator;
               canvas.Paint                  += CanvasPaintHandler;
               canvas.Paint                  += CanvasPaintRetranslator;
               canvas.RegionSelectionStart   += RegionSelectionStartHandler;
               canvas.RegionSelectionStart   += RegionSelectionStartRetranslator;
               canvas.RegionSelectionUpdate  += RegionSelectionUpdateHandler;
               canvas.RegionSelectionUpdate  += RegionSelectionUpdateRetranslator;
               canvas.RegionSelectionEnd     += RegionSelectionEndHandler;
               canvas.RegionSelectionEnd     += RegionSelectionEndRetranslator;
               canvas.KeyDown                += CanvasKeyDownHandler;
               canvas.KeyDown                += CanvasKeyDownRetranslator;
               canvas.Load                   += LoadHandler;
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

      /*Специальное свойство для добавления элементов в сеть*/
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
            value.Change += NetElementChangeHandler;
            OnChange(new EventArgs());
         }
      }

      /*Специальное свойство для удаления элементов из сети*/
      public NetElement ElementNullPortal{
         set{
            value.PrepareToDeletion();
            if(value is Place){
               Places.Remove(value);
            }
            if(value is Transition){
               Transitions.Remove(value);
            }
            if(value is Arc){
               Arcs.Remove(value);
            }
            value.Change -= NetElementChangeHandler;
            OnChange(new EventArgs());
         }
      }

      /*События*/
      public event MouseEventHandler MouseClick;
      public event MouseEventHandler MouseMove;
      public event MouseEventHandler MouseDown;
      public event MouseEventHandler MouseUp;
      public event PaintEventHandler Paint;

      public event RegionSelectionEventHandler RegionSelectionStart;
      public event RegionSelectionEventHandler RegionSelectionUpdate;
      public event RegionSelectionEventHandler RegionSelectionEnd;
      
      public event EventHandler Change;

      public event KeyEventHandler KeyDown;

      /*Событие генерируется при сохранении сети в файл*/
      public event SaveEventHandler Save;
      
      private void OnChange(EventArgs args){
         Saved = false;
         if(Change != null){
            Change(this,args);
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

      private void OnSave(SaveEventArgs args){
         Saved = true;
         LinkedFile = args.fileName;
         if (Save != null){
            Save(this, args);
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

      private void CanvasKeyDownHandler(object sender, KeyEventArgs args){
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
            if((Arcs[i] as Arc).Source == from_ && (Arcs[i] as Arc).Target == to_){
               return true;
            }
         }
         return false;
      }
      
      private void MenuSaveHandler(object sender, System.EventArgs args){
         if(Canvas.Visible){
            if(SaveNet())
               OnSave(new SaveEventArgs(LinkedFile, this.ID));
         }
      }

      private void MenuSaveAsHandler(object sender, System.EventArgs args){
         if(Canvas.Visible){
            if(SaveNetAs())
               OnSave(new SaveEventArgs(LinkedFile, this.ID));
         }
      }

      private void NetElementChangeHandler(object sender, System.EventArgs args){
         OnChange(new EventArgs());
      }
      
      private void LoadHandler(object sender, System.EventArgs args){
         ((canvas.FindForm() as MainForm).MainMenuStrip as MainMenuStrip).toolStripMenuSave.Click   += MenuSaveHandler;
         ((canvas.FindForm() as MainForm).MainMenuStrip as MainMenuStrip).toolStripMenuSaveAs.Click += MenuSaveAsHandler;
      }

      private bool SaveNet(){
         bool result = false;
         if(LinkedFile != ""){
            if (File.Exists(LinkedFile)){
               File.Delete(LinkedFile);
            }
            using (FileStream fs = File.Create(LinkedFile)){
               XmlSerializer serealizer = new XmlSerializer(this.GetType());
               serealizer.Serialize(fs, this);
               fs.Close();
               result = true;
            }
         }else{
            result = SaveNetAs();
         }
         return result;
      }

      private bool SaveNetAs(){
         bool result = false;
         Stream stream;
         string fileName = "";
         SaveFileDialog saveFileDialog1 = new SaveFileDialog();
         saveFileDialog1.Filter = "pnml files (*.pnml)|*.pnml|All files (*.*)|*.*";
         saveFileDialog1.FilterIndex = 1 ;
         saveFileDialog1.RestoreDirectory = true ;

         if(saveFileDialog1.ShowDialog() == DialogResult.OK){
            if((stream = saveFileDialog1.OpenFile()) != null){
               LinkedFile = fileName = (stream as FileStream).Name;
               if(this.ID=="")
                  this.ID = fileName.Substring(fileName.LastIndexOf("\\")+1);

               XmlSerializer serializer = new XmlSerializer(this.GetType());
               serializer.Serialize(stream, this);
               stream.Close();
               result = true;
            }
         }
         return result;
      }

      public void WriteXml (XmlWriter writer)
      {
         //writer.WriteStartElement("pnml");
         writer.WriteStartElement("net");
         writer.WriteAttributeString("id", ID);
         writer.WriteAttributeString("type", Type);
         foreach(Place place in Places){
            writer.WriteStartElement("place");
            place.WriteXml(writer);
            writer.WriteEndElement(); // place
         }
         foreach(Transition transition in Transitions){
            writer.WriteStartElement("transition");
            transition.WriteXml(writer);
            writer.WriteEndElement(); // transition
         }
         foreach(Arc arc in Arcs){
            writer.WriteStartElement("arc");
            arc.WriteXml(writer);
            writer.WriteEndElement(); // arc
         }
         writer.WriteEndElement(); // net
         //writer.WriteEndElement(); // pnml
     }

      public void ReadXml (XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.ReadStartElement("pnml");
         this.ID = reader.GetAttribute("id");
         this.Type = reader.GetAttribute("type");;

         if(!reader.IsEmptyElement){
            reader.ReadStartElement("net");
            while(reader.NodeType != XmlNodeType.EndElement)
            {
               switch(reader.Name){
                  case "place":
                     subTreeReader = reader.ReadSubtree();
                     ElementPortal = new Place(subTreeReader);
                     subTreeReader.Close();
                     reader.Skip();
                  break;
                  case "transition":
                     subTreeReader = reader.ReadSubtree();
                     ElementPortal = new Transition(subTreeReader);
                     subTreeReader.Close();
                     reader.Skip();
                  break;
                  case "arc":
                     subTreeReader = reader.ReadSubtree();
                     ElementPortal = new Arc(subTreeReader, this);
                     subTreeReader.Close();
                     reader.Skip();
                  break;
                  default:
                     reader.Read();
                  break;
               }
            }
            reader.ReadEndElement();
            reader.ReadEndElement();
         }else{
            reader.Skip();
            reader.ReadEndElement();
         }
         
         Saved = true;
      }

      public XmlSchema GetSchema()
      {
         return(null);
      }
      
      public NetElement GetElementByID(string ID_){
         if(ID_ == "")
            return null;
         foreach(Place place in Places){
            if(place.ID == ID_)
               return place;
         }
         foreach(Transition transition in Transitions){
            if(transition.ID == ID_)
               return transition;
         }
         foreach(Arc arc in Arcs){
            if(arc.ID == ID_)
               return arc;
         }
         return null;
      }
   } // PetriNet
} // namespace
