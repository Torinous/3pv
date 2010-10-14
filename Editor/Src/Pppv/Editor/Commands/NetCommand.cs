namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.ApplicationFramework.Commands;

   public abstract class NetCommand : Command
   {
      private PetriNetGraphical net;

      protected NetCommand(PetriNetGraphical net) : base()
      {
         this.Net = net;
      }

      protected NetCommand() : base()
      {
      }

      public PetriNetGraphical Net
      {
         get { return this.net; }
         set { this.net = value; }
      }
   }
}