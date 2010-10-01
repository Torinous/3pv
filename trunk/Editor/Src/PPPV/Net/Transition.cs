namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Globalization;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Utils;

   [Serializable()]
   [XmlRoot("transition")]
   public class Transition : NetElement, IXmlSerializable
   {
      private string guardFunction;

      public Transition(Point point) : base(point)
      {
      }

      public Transition(XmlReader reader) : this(new Point(0, 0))
      {
         this.ReadXml(reader);
      }

      public Transition() : this(new Point(0, 0))
      {
      }

      public string GuardFunction
      {
         get
         {
            return this.guardFunction;
         }

         set
         {
            this.guardFunction = value;
         }
      }

      public void WriteXml(XmlWriter writer)
      {
         writer.WriteAttributeString("id", this.Name);

         writer.WriteStartElement("graphics");
         writer.WriteStartElement("position");
         writer.WriteAttributeString("x", this.X.ToString(CultureInfo.CurrentCulture) + ".0");
         writer.WriteAttributeString("y", this.Y.ToString(CultureInfo.CurrentCulture) + ".0");
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

      public void ReadXml(XmlReader reader)
      {
         reader.Read();
         reader.MoveToAttribute("id");
         Id = reader.Value;
         reader.ReadStartElement("transition");
         while (reader.NodeType != XmlNodeType.EndElement)
         {
            switch (reader.Name)
            {
               case "graphics":
                  reader.ReadStartElement("graphics");
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
                  if (!reader.IsEmptyElement)
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
         return null;
      }

      public override void SetId(int number)
      {
         if (String.IsNullOrEmpty(this.Id))
         {
            this.Id = "T" + number;
            if (String.IsNullOrEmpty(this.Name))
            {
               this.Name = this.Id;
            }
         }
      }
   }
}