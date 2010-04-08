using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using PPPV.Utils;

namespace PPPV.Net {
  [Serializable()]
  [XmlRoot("transition")]
public class Transition : NetElement, IXmlSerializable {
    private static int _ID = 0;
    private string guardFunction;

    /*╨Ъ╨╛╨╜╤Б╤В╤А╤Г╨║╤В╨╛╤А╤Л*/
public Transition(int x_, int y_):base(x_,y_,20,50,true) {
      _ID++;
      Name = ID = "T"+_ID;
      //UpdateHitRegion();
      guardFunction = "";
    }

public Transition(XmlReader reader):this(0,0){
      this.ReadXml(reader);
    }

    /*╨Р╨║╤Ж╨╡╤Б╤Б╨╛╤А╤Л ╨┤╨╛╤Б╤В╤Г╨┐╨░*/
    public string GuardFunction{
      get{
        return guardFunction;
      }
      set{
        guardFunction = value;
        OnChange(new EventArgs());
      }
    }

    public override Point Center{
      get{
        return new Point(X + (int)(Width/2), Y + (int)(Height/2));
      }
    }

    public override void Draw(object sender, PaintEventArgs e) {

      base.Draw(sender,e);

      Graphics dc = e.Graphics;
      dc.SmoothingMode = SmoothingMode.HighQuality;
      Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));
      Pen RedPen = new Pen(Color.FromArgb(255,255,0,0));
      /*╨Ъ╨╕╤Б╤В╨╕*/
      SolidBrush grayBrush = new SolidBrush(Color.FromArgb(200,100,100,100));
      SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200,0,0,0));
      /*╨и╤А╨╕╤Д╤В*/
      FontFamily fF_Arial = new FontFamily("Arial");
      Font font1 = new Font(fF_Arial,16,FontStyle.Regular,GraphicsUnit.Pixel);

      Region fillRegion = new Region(new Rectangle( X, Y, Width, Height));
      dc.FillRegion(grayBrush, fillRegion);
      dc.DrawRectangle(blackPen, X, Y, Width, Height);
      dc.DrawString(Name+"\n"+guardFunction,font1,blackBrush,X+20,Y-17);
    }

    protected override void UpdateHitRegion(){
      using(PreciseTimer pr = new PreciseTimer("Transition.UpdateRegion")){
        HitRegion.MakeEmpty();
        HitRegion.Union(new Rectangle( X, Y, Width, Height));
      }
    }

    protected override void MouseClickHandler(object sender, MouseEventArgs args)
    {
    }

    protected override void MouseMoveHandler(object sender, MouseEventArgs args)
    {
      base.MouseMoveHandler(sender,args);
    }

    protected override void MouseDownHandler(object sender, MouseEventArgs args)
    {
      base.MouseDownHandler(sender,args);
      if(args.Button == MouseButtons.Left)
      {
      }
    }

    protected override void MouseUpHandler(object sender, MouseEventArgs args)
    {
    }

    protected override void RegionSelectionStartHandler(object sender, RegionSelectionEventArgs args)
    {
    }

    protected override void RegionSelectionUpdateHandler(object sender, RegionSelectionEventArgs args)
    {
    }

    protected override void RegionSelectionEndHandler(object sender, RegionSelectionEventArgs args)
    {
    }

    protected override void KeyDownHandler(object sender, KeyEventArgs arg)
    {
    }

    public void WriteXml (XmlWriter writer)
    {
      writer.WriteAttributeString("id", this.Name);

      writer.WriteStartElement("graphics");
      writer.WriteStartElement("position");
      writer.WriteAttributeString("x", this.X.ToString()+".0");
      writer.WriteAttributeString("y", this.Y.ToString()+".0");
      writer.WriteEndElement(); // position
      writer.WriteEndElement(); // graphics

      writer.WriteStartElement("name");
      writer.WriteStartElement("value");
      writer.WriteString(this.Name);
      writer.WriteEndElement(); // value
      writer.WriteEndElement(); // name

      writer.WriteStartElement("guard");
      writer.WriteStartElement("value");
      writer.WriteString(this.guardFunction);
      writer.WriteEndElement(); // value
      writer.WriteEndElement(); // guard
    }

    public void ReadXml (XmlReader reader)
    {
      reader.Read();
      reader.MoveToAttribute("id");
      this.ID = reader.Value;
      reader.ReadStartElement("transition");
      while(reader.NodeType != XmlNodeType.EndElement)
      {
        switch(reader.Name)
        {
        case "graphics":
          reader.ReadStartElement("graphics");
          /* ╨Ю╨▒╤А╨░╨▒╨╛╤В╨░╨╡╨╝ position*/
          {
            reader.ReadToDescendant("position");
            reader.MoveToAttribute("x");
            this.X = (int)reader.ReadContentAsDouble();
            reader.MoveToAttribute("y");
            this.Y = (int)reader.ReadContentAsDouble();
            reader.MoveToElement();
            reader.Skip();
          }
          reader.ReadEndElement(); // graphics
          break;
        case "name":
          reader.ReadToDescendant("value");
          this.Name = reader.ReadString();
          reader.ReadEndElement(); // value
          reader.ReadEndElement(); // name
          break;
        case "guard":
          reader.ReadToDescendant("value");
          if(!reader.IsEmptyElement)
          {
            this.GuardFunction = reader.ReadString();
            reader.ReadEndElement(); // value
            reader.ReadEndElement(); // guard
          }
          else
          {
            reader.Skip();
            reader.ReadEndElement(); // guard
          }
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
  } // Transition
} // namespace
