namespace Pppv.Net
{
   using System;
   using System.Collections;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Globalization;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Utils;

   [Serializable()]
   [XmlRoot("place")]
   public class Place : NetElement, IPlace
   {
      private TokensList tokens;

      public Place(Point point) : base(point)
      {
         this.tokens = new TokensList();
      }

      public Place() : this(new Point(0, 0))
      {
      }

      public TokensList Tokens
      {
         get { return this.tokens; }
         private set { this.tokens = value; }
      }

      public override void WriteXml(XmlWriter writer)
      {
         writer.WriteAttributeString("id", this.Id);
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
         XmlSerializer serealizer = new XmlSerializer(this.Tokens.GetType());
         serealizer.Serialize(writer, this.Tokens);
      }

      public override void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.MoveToAttribute("id");
         this.Id = reader.Value;
         reader.ReadStartElement("place");
         while (reader.NodeType != XmlNodeType.EndElement)
         {
            switch (reader.Name)
            {
               case "graphics":
                  reader.ReadStartElement("graphics");
                  /* Обработаем position*/
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
               case "initialMarking":
                  subTreeReader = reader.ReadSubtree();
                  this.Tokens = new TokensList();
                  XmlSerializer serealizer = new XmlSerializer(typeof(TokensList));
                  this.Tokens = serealizer.Deserialize(subTreeReader) as TokensList;
                  subTreeReader.Close();
                  reader.Skip();
                  break;
               default:
                  break;
            }
         }
      }

      public override XmlSchema GetSchema()
      {
         return null;
      }

      public override void SetId(int number)
      {
         if (String.IsNullOrEmpty(this.Id))
         {
            this.Id = "P" + number;
            if (String.IsNullOrEmpty(this.Name))
            {
               this.Name = this.Id;
            }
         }
      }
   }
}
