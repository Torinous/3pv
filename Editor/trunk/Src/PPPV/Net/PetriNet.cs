using System;
using System.Text;
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
  public partial class PetriNet:IXmlSerializable 
  {
    private string id;
    private string type;
    /*Путь к файлу в который сохранена сеть*/
    private string linkedFile;
    /*Элементы сети*/
    private ArrayList places;
    private ArrayList transitions;
    private ArrayList arcs;
    private string additionalCode;
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

    public Editor.NetCanvas Canvas
    {
      get
      {
        return canvas;
      }
      set
      {
        if(canvas != null)
        {
          canvas.Paint                  -= CanvasPaintHandler;
          canvas.Paint                  -= CanvasPaintRetranslator;
          canvas.Load                   -= LoadHandler;
        }

        canvas = value;

        if(canvas != null)
        {
          canvas.Paint                  += CanvasPaintHandler;
          canvas.Paint                  += CanvasPaintRetranslator;
          canvas.Load                   += LoadHandler;
        }
      }
    }

    public ArrayList Places
    {
      get
      {
        return places;
      }
      private set
      {
        places = value;
      }
    }

    public ArrayList Transitions
    {
      get
      {
        return transitions;
      }
      private set
      {
        transitions = value;
      }
    }
  
    public ArrayList Arcs
    {
      get
      {
        return arcs;
      }
      private set
      {
        arcs = value;
      }
    }

    /*Специальное свойство для добавления элементов в сеть*/
    public NetElement ElementPortal
    {
      set
      {
        if(value is Place)
        {
          Places.Add(value);
        }
        if(value is Transition)
        {
          Transitions.Add(value);
        }
        if(value is Arc)
        {
          Arcs.Add(value);
        }
        value.ParentNet = this;
        value.Change += NetElementChangeHandler;
        OnChange(new EventArgs());
      }
    }

    /*Специальное свойство для удаления элементов из сети*/
    public NetElement ElementNullPortal
    {
      set
      {
        value.PrepareToDeletion();
        if(value is Place)
        {
          Places.Remove(value);
        }
        if(value is Transition)
        {
          Transitions.Remove(value);
        }
        if(value is Arc)
        {
          Arcs.Remove(value);
        }
        value.Change -= NetElementChangeHandler;
        OnChange(new EventArgs());
      }
    }

    public string AdditionalCode
    {
      get
      {
        return additionalCode;
      }
      set
      {
        additionalCode = value;
        OnChange(new EventArgs());
      }
    }

    /*События*/
    public event PaintEventHandler Paint;

    public event EventHandler Change;
    
    private void CanvasPaintHandler(object sender, PaintEventArgs args)
    {
    }
    
    private void CanvasPaintRetranslator(object sender, PaintEventArgs args)
    {
      OnPaint(args);
    }

    /*Событие генерируется при сохранении сети в файл*/
    public event SaveEventHandler Save;

    private void OnChange(EventArgs args)
    {
      Saved = false;
      if(Change != null)
      {
        Change(this,args);
      }
    }

    private void OnPaint(PaintEventArgs e)
    {
      if(Paint != null)
      {
        using(PreciseTimer pr = new PreciseTimer("PetriNet.Draw"))
        {
          Paint(this,e);
        }
      }
    }

    private void OnSave(SaveEventArgs args){
      Saved = true;
      LinkedFile = args.fileName;
      if (Save != null)
      {
        Save(this, args);
      }
    }

    public NetElement AddPlace(Point position)
    {
      Place tmpPlace = new Place(position.X, position.Y);
      return ElementPortal = tmpPlace;
    }

    public NetElement AddTransition(Point position)
    {
      Transition tmpTransition = new Transition(position.X, position.Y);
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
      for(i=0;i<Transitions.Count;++i)
      {
        if(((NetElement)Transitions[i]).IsIntersectWith(_p))
        {
          return (NetElement)Transitions[i];
        }
      }
      for(i=0;i<Places.Count;++i)
      {
        if(((NetElement)Places[i]).IsIntersectWith(_p))
        {
          return (NetElement)Places[i];
        }
      }
      for(i=0;i<Arcs.Count;++i)
      {
        if(((NetElement)Arcs[i]).IsIntersectWith(_p))
        {
          return (NetElement)Arcs[i];
        }
      }
      return null;
    }

    public ArrayList NetElementUnder(Rectangle selectedRectangle)
    {
      ArrayList selectedObjects = new ArrayList();
      int i = 0;
      for(i=0;i<Transitions.Count;++i)
      {
        if(((NetElement)Transitions[i]).IsIntersectWith(selectedRectangle))
        {
          selectedObjects.Add((NetElement)Transitions[i]);
        }
      }
      for(i=0;i<Places.Count;++i)
      {
        if(((NetElement)Places[i]).IsIntersectWith(selectedRectangle))
        {
          selectedObjects.Add((NetElement)Places[i]);
        }
      }
      for(i=0;i<Arcs.Count;++i)
      {
        if(((NetElement)Arcs[i]).IsIntersectWith(selectedRectangle))
        {
          selectedObjects.Add((NetElement)Arcs[i]);
        }
      }
      return selectedObjects;
    }

    public bool HaveUnfinishedArcs()
    {
      for(int i=0; i<Arcs.Count; ++i)
      {
        if(((Arc)Arcs[i]).Unfinished)
        {
          return true;
        }
      }
      return false;
    }

    public bool HaveArcBetween(NetElement from_, NetElement to_){
      for(int i=0;i<Arcs.Count;++i)
      {
        if((Arcs[i] as Arc).Source == from_ && (Arcs[i] as Arc).Target == to_)
        {
          return true;
        }
      }
      return false;
    }

    private void MenuSaveHandler(object sender, System.EventArgs args){
      if(Canvas.Visible)
      {
        if(SaveNet())
          OnSave(new SaveEventArgs(LinkedFile, this.ID));
      }
    }

    private void MenuSaveAsHandler(object sender, System.EventArgs args){
      if(Canvas.Visible)
      {
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
      StreamWriter stream;
      if(LinkedFile != "")
      {
        if (File.Exists(LinkedFile))
        {
          File.Delete(LinkedFile);
        }
        stream = new StreamWriter(LinkedFile, false, Encoding.GetEncoding(1251));
        if(stream != null)
        {
          XmlSerializer serealizer = new XmlSerializer(this.GetType());
          serealizer.Serialize(stream, this);
          stream.Close();
          result = true;
        }
      }
      else
      {
        result = SaveNetAs();
      }
      return result;
    }

    private bool SaveNetAs(){
      bool result = false;
      StreamWriter stream;
      string fileName = "";
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.Filter = "pnml files (*.pnml)|*.pnml|All files (*.*)|*.*";
      saveFileDialog1.FilterIndex = 1 ;
      saveFileDialog1.RestoreDirectory = true ;

      if(saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        stream = new StreamWriter(saveFileDialog1.FileName, false, Encoding.GetEncoding(1251));
        if(stream != null)
        {
          LinkedFile = fileName = saveFileDialog1.FileName;
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
      writer.WriteStartElement("additionalCode");
      writer.WriteString(this.AdditionalCode);
      writer.WriteEndElement(); // additionalCode
      writer.WriteEndElement(); // net
      //writer.WriteEndElement(); // pnml
    }

    public void ReadXml (XmlReader reader)
    {
      XmlReader subTreeReader;
      reader.ReadStartElement("pnml");
      this.ID = reader.GetAttribute("id");
      this.Type = reader.GetAttribute("type");
      ;

      if(!reader.IsEmptyElement)
      {
        reader.ReadStartElement("net");
        while(reader.NodeType != XmlNodeType.EndElement)
        {
          switch(reader.Name)
          {
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
          case "additionalCode":
            if(!reader.IsEmptyElement)
            {
              reader.ReadStartElement("additionalCode");
              /*Причину Replace см. Issue 27*/
              this.AdditionalCode = reader.ReadString().Replace("\n", System.Environment.NewLine);
              reader.ReadEndElement(); // additionalCode
            }
            else
            {
              reader.Skip();
            }
            break;
          default:
            reader.Read();
            break;
          }
        }
        reader.ReadEndElement();
        reader.ReadEndElement();
      }
      else
      {
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
