/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 06.10.2010
 * Time: 17:48
 */

namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor.Shapes;
   using Pppv.Net;

   public abstract class ElementInterfaceCommand : NetInterfaceCommand
   {
      private IShape element;

      protected ElementInterfaceCommand() : base()
      {
      }

      protected ElementInterfaceCommand(NetElement ne) : base(ne.ParentNet as PetriNetGraphical)
      {
      }

      public IShape Element
      {
        get { return this.element; }
        set { this.element = value; }
      }
   }
}