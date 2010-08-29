namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;
   
   public abstract class ElementCommand : NetCommand
   {
      private NetElement element;

      protected ElementCommand() : base()
      {
      }

      protected ElementCommand(NetElement ne) : base(ne.ParentNet as PetriNetWrapper)
      {
      }

      public NetElement Element
      {
        get { return this.element; }
        set { this.element = value; }
      }
   }
}