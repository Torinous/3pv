/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 06.10.2010
 * Time: 17:47
 */
namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public abstract class NetInterfaceCommand : InterfaceCommand
   {
      private PetriNetGraphical net;

      protected NetInterfaceCommand(PetriNetGraphical net) : base()
      {
         this.Net = net;
      }

      protected NetInterfaceCommand() : base()
      {
      }

      public PetriNetGraphical Net
      {
         get { return this.net; }
         set { this.net = value; }
      }
   }
}
