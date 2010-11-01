namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor.Shapes;
   using Pppv.Net;

   public abstract class ElementCommand : NetCommand
   {
      private IShape element;

      protected ElementCommand() : base()
      {
      }

      protected ElementCommand(NetElement ne) : base(ne.ParentNet as PetriNetGraphical)
      {
      }

      public IShape Element
      {
        get { return this.element; }
        set { this.element = value; }
      }
   }
}