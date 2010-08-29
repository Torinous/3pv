namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class ZoomOutCommand : Command
   {
      public ZoomOutCommand()
      {
         this.Name = "Уменьшить";
         this.Description = "Уменьшение";
         this.ShortcutKeys = Keys.Control | Keys.Down;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Zoom out.png"), true);
      }

      public override void Execute()
      {
         PetriNet p = EditorApplication.Instance.ActiveNet;
         if (p != null)
         {
            if (p.Canvas.ScaleAmount > 0.11F)
            {
               p.Canvas.ScaleAmount -= 0.1F;
            }

            p.Canvas.Refresh();
         }
      }

      public override void Unexecute()
      {
      }
   }
}