namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;
   using System.Xml.Serialization;

   [Serializable()]
   public abstract class NetElement : Graphical
   {
      [NonSerializedAttribute]
      private PetriNet parent;

      [NonSerializedAttribute]
      private string id;

      protected NetElement(Point point) : base(point)
      {
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

      public PetriNet ParentNet
      {
         get
         {
            return this.parent;
         }

         set
         {
            if (this.parent != null)
            {
               this.parent.Paint -= this.ParentNetDrawHandler;
            }

            this.parent = value;
            if (this.parent != null)
            {
               this.parent.Paint += this.ParentNetDrawHandler;
            }
         }
      }

      public override void PrepareToDeletion()
      {
         this.ParentNet = null;
      }

      private void ParentNetDrawHandler(object sender, PaintEventArgs e)
      {
         this.Draw(e);
      }
   }
}