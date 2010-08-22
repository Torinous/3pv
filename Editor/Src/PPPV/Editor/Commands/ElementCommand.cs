namespace PPPV.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using PPPV.Net;
   public abstract class ElementCommand: NetCommand
   {
      private NetElement ne;
    
      public NetElement Element {
        get{ return ne; }
        set{ ne = value; }
      }

      protected ElementCommand():base()
      {

      }

      protected ElementCommand(NetElement ne):base(ne.ParentNet as PetriNetWrapper)
      {

      }
   }
}
