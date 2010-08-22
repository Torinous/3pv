namespace PPPV.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using PPPV.Net;
   public class RedoCommand : Command
   {
      public RedoCommand()
      {
         Name = "Повтор";
         Description = "Повтор последнего отменённого действия";
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Redo.png"), true);
      }

      public override void Execute()
      {

      }

      public override void Unexecute()
      {

      }
   }
}
