namespace PPPV.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Diagnostics;
   using System.Windows.Forms;

   using PPPV.Net;
   using PPPV.Utils;

   public class CutCommand : Command
   {

      public CutCommand()
      {
         Name = "Вырезать";
         Description = "Вырезать выделенный элемент сети";
         ShortcutKeys = Keys.Control | Keys.X;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Cut.png"), true);
      }

      public override void Execute()
      {

      }

      public override void Unexecute()
      {

      }
   }
}
