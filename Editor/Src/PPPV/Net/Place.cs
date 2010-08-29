﻿namespace Pppv.Net
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
      private static int id;
      private TokensList tokens;

      public Place(Point point) : base(point)
      {
         id++;
         Name = Id = "P" + id;
         Size = new Size(50, 50);
         this.tokens = new TokensList(10);
      }

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

         private set
         {
            if (this.tokens != null)
            {
               this.tokens.Change -= this.TokensListChangeHandler;
            }
            
            this.tokens = value;
            if (this.tokens != null)
            {
               this.tokens.Change += this.TokensListChangeHandler;
            }

            OnChange(new EventArgs());
         }
      }

      public override Point Center
      {
         get
         {
            return new Point(X + ((int)Size.Width / 2), Y + ((int)Size.Height / 2));
         }
      }

      public override void Draw(object sender, PaintEventArgs e)
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
         dc.DrawString(this.Tokens.List.Count.ToString(CultureInfo.CurrentCulture), font1, blackBrush, X + ((int)Size.Width / 2) - 10, Y + ((int)Size.Height / 2) - 10);
      }

      public override Point GetPilon(Point from)
      {
         return this.GetPilon(from, this.ParentNet.Canvas);
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