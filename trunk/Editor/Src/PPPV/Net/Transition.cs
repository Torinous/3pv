namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;
   using System.Globalization;

   using Pppv.Utils;

   [Serializable()]
   [XmlRoot("transition")]
   public class Transition : NetElement, IXmlSerializable
   {
      private static int _ID;
      private string guardFunction;

      public Transition(Point point):base(point)
      {
         _ID++;
         Name = Id = "T"+_ID;
         Size = new Size(20,50);
         guardFunction = "";
      }

      public Transition(XmlReader reader):this(new Point(0,0))
      {
         this.ReadXml(reader);
      }

      public string GuardFunction
      {
         get{
            return guardFunction;
         }
         set{
            guardFunction = value;
            OnChange(new EventArgs());
         }
      }

      public override Point Center
      {
         get{
            return new Point(X + (int)(Size.Width/2), Y + (int)(Size.Height/2));
         }
      }
      
      public override void Draw(object sender, PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));
         Pen RedPen = new Pen(Color.FromArgb(255,255,0,0));
         SolidBrush grayBrush = new SolidBrush(Color.FromArgb(200,100,100,100));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200,0,0,0));
         FontFamily fF_Arial = new FontFamily("Arial");
         Font font1 = new Font(fF_Arial,16,FontStyle.Regular,GraphicsUnit.Pixel);

         Region fillRegion = new Region(new Rectangle( X, Y, Size.Width, Size.Height));
         dc.FillRegion(grayBrush, fillRegion);
         dc.DrawRectangle(blackPen, X, Y, Size.Width, Size.Height);
         dc.DrawString(Name+"\n"+guardFunction, font1, blackBrush, X+20, Y-17);
      }

      protected override void UpdateHitRegion()
      {
         using(PreciseTimer pr = new PreciseTimer("Transition.UpdateRegion")){
            HitRegion.MakeEmpty();
            HitRegion.Union(new Rectangle( X, Y, Size.Width, Size.Height));
         }
      }

      public override Point GetPilon(Point from)
      {
         return this.GetPilon(from, this.ParentNet.Canvas);
      }

      public void WriteXml (XmlWriter writer)
      {
         writer.WriteAttributeString("id", this.Name);

         writer.WriteStartElement("graphics");
         writer.WriteStartElement("position");
         writer.WriteAttributeString("x", this.X.ToString(CultureInfo.CurrentCulture)+".0");
         writer.WriteAttributeString("y", this.Y.ToString(CultureInfo.CurrentCulture)+".0");
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
         this.Id = reader.Value;
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
