namespace Pppv.Net
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   [Serializable()]
   public abstract class NetElement : INetElement
   {
      [NonSerializedAttribute()]
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Для декартовых координат подходит и так")]
      private int x, y;

      [NonSerializedAttribute()]
      private PetriNet parent;

      [NonSerializedAttribute()]
      private string id;
      [NonSerializedAttribute()]
      private string name;

      protected NetElement(Point point)
      {
         this.X = point.X;
         this.Y = point.Y;
      }

      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Для декартовых координат сойдёт")]
      public int Y
      {
         get { return this.y; }
         set { this.y = value; }
      }

      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Для декартовых координат сойдёт")]
      public int X
      {
         get { return this.x; }
         set { this.x = value; }
      }

      public PetriNet Parent
      {
         get { return this.parent; }
         set { this.parent = value; }
      }

      public string Id
      {
         get { return this.id; }
         protected set { this.id = value; }
      }

      public string Name
      {
         get { return this.name; }
         set { this.name = value; }
      }

      public PetriNet ParentNet
      {
         get { return this.parent; }
         set { this.parent = value; }
      }
      
      public abstract void WriteXml(XmlWriter writer);

      public abstract void ReadXml(XmlReader reader);

      public abstract XmlSchema GetSchema();

      public abstract void SetId(int number);
   }
}
