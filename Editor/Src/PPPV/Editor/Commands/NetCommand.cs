namespace PPPV.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;
   
   using PPPV.Net;

   public abstract class NetCommand:Command
   {
   //Данные
   private PetriNet net;
   
   //Акцессоры
   public PetriNet Net
   {
      get{
         return net;
      }
      set{
         net = value;
      }
    }

      protected NetCommand(PetriNetWrapper net):base()
      {
         Net = net;
      }

      protected NetCommand():base()
      {
      }
   }
}
