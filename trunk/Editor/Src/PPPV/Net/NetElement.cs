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

      protected NetElement(Point point) : base(point)
      {
      }

      public override event EventHandler Change;

      public PetriNet Parent
      {
         get { return this.parent; }
         set { this.parent = value; }
      }

      public virtual string Id { get; protected set; }

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
               this.parent.Paint -= this.Draw;
               this.parent.Paint -= this.PaintRetranslator;
            }

            this.parent = value;
            if (this.parent != null)
            {
               this.parent.Paint += this.Draw;
               this.parent.Paint += this.PaintRetranslator;
            }
         }
      }

      public override void PrepareToDeletion()
      {
         this.ParentNet = null;
      }

      protected override void OnChange(EventArgs args)
      {
         if (this.Change != null)
         {
            this.Change(this, args);
         }

         base.OnChange(args);
      }
   }
}