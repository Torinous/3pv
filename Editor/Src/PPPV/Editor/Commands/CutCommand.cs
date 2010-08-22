namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Diagnostics;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class CutCommand : Command
   {

      public CutCommand()
      {
         Name = "Вырезать";
         Description = "Вырезать выделенный элемент сети";
         ShortcutKeys = Keys.Control | Keys.X;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Cut.png"), true);
      }

      public override void Execute()
      {

      }

      public override void Unexecute()
      {

      }
   }
}
