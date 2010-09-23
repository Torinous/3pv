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
      [NonSerializedAttribute]
      private string name;

      protected NetElement(Point point) : base(point)
      {
      }

      public event EventHandler<ParentNetChangeEventArgs> ParentNetChange;

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
         get
         {
            return this.name;
         }

         set
         {
            this.name = value;
            this.OnChange(new EventArgs());
         }
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

            this.OnParentNetChange(new ParentNetChangeEventArgs(this.parent, value));
            this.parent = value;

            if (this.parent != null)
            {
               this.parent.Paint += this.ParentNetDrawHandler;
            }
         }
      }

      public virtual void PrepareToDeletion()
      {
         this.ParentNet = null;
      }

      public abstract void SetId(int number);

      protected virtual void OnParentNetChange(ParentNetChangeEventArgs e)
      {
         if (this.ParentNetChange != null)
         {
            this.ParentNetChange(this, e);
         }
      }

      private void ParentNetDrawHandler(object sender, PaintEventArgs e)
      {
         this.Draw(e);
      }
   }
}