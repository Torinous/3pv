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
   public class Place : NetElement, IXmlSerializable
   {
      private TokensList tokens;

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Не смертельно")]
      public Place(Point point) : base(point)
      {
         this.Size = new Size(50, 50);
         this.tokens = new TokensList();
         this.tokens.Change += this.TokensListChangeHandler;
         this.UpdateHitRegion();
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Не смертельно")]
      public Place(XmlReader reader) : this(new Point(0, 0))
      {
         this.ReadXml(reader);
      }

      public TokensList Tokens
      {
         get
         {
            return this.tokens;
         }
      }

      public override Point Center
      {
         get
         {
            return new Point(X + ((int)Size.Width / 2), Y + ((int)Size.Height / 2));
         }
      }

      public override void Draw(PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
         SolidBrush grayBrush = new SolidBrush(Color.FromArgb(200, 100, 100, 100));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
         Font font1 = new Font(new FontFamily("Arial"), 16, FontStyle.Regular, GraphicsUnit.Pixel);

         GraphicsPath tmpPath = new GraphicsPath();
         tmpPath.AddEllipse(X, Y, Size.Width, Size.Height);
         Region fillRegion = new Region(tmpPath);
         dc.FillRegion(grayBrush, fillRegion);
         dc.DrawEllipse(blackPen, X, Y, Size.Width, Size.Height);
         dc.DrawString(Name, font1, blackBrush, X + ((int)Size.Width / 2) + 5, Y - 5);
         dc.DrawString(this.Tokens.Count.ToString(CultureInfo.CurrentCulture), font1, blackBrush, X + ((int)Size.Width / 2) - 10, Y + ((int)Size.Height / 2) - 10);
         base.Draw(e);
      }

      public override Point GetConnectPoint(Point from)
      {
         return this.GetConnectPoint(from, this.ParentNet.Canvas);
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
         writer.WriteStartElement("initialMarking");
         this.Tokens.WriteXml(writer);
         writer.WriteEndElement(); // initialMarking
      }

      public void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.Read();
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
                     int tmpX, tmpY;
                     reader.ReadToDescendant("position");
                     reader.MoveToAttribute("x");
                     tmpX = (int)reader.ReadContentAsDouble();
                     reader.MoveToAttribute("y");
                     tmpY = (int)reader.ReadContentAsDouble();
                     this.MoveBy(new Point(tmpX, tmpY));
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
                  this.Tokens.ReadXml(subTreeReader);
                  subTreeReader.Close();
                  reader.Skip();
                  break;
               default:
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
            this.Id = "P" + number;
            if (String.IsNullOrEmpty(this.Name))
            {
               this.Name = this.Id;
            }
         }
      }

      protected override void UpdateHitRegion()
      {
         using (PreciseTimer pr = new PreciseTimer("Place.UpdateRegion"))
         {
            HitRegion.MakeEmpty();
            GraphicsPath tmpPath = new GraphicsPath();
            tmpPath.AddEllipse(X, Y, Size.Width, Size.Height);
            HitRegion.Union(tmpPath);
         }
      }

      private void TokensListChangeHandler(object sender, EventArgs args)
      {
         OnChange(args);
      }
   }
}