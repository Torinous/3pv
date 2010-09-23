namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public abstract class NetCommand : Command
   {
      private PetriNet net;

      protected NetCommand(PetriNet net) : base()
      {
         this.Net = net;
      }

      protected NetCommand() : base()
      {
      }

      public PetriNet Net
      {
         get { return this.net; }
         set { this.net = value; }
      }
   }
}