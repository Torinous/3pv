namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class ZoomInCommand : Command
   {
      public ZoomInCommand()
      {
         this.Name = "Увеличить";
         this.Description = "Увеличить";
         this.ShortcutKeys = Keys.Control | Keys.Up;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Zoom in.png"), true);
      }

      public override void Execute()
      {
         PetriNetGraphical p = EditorApplication.Instance.ActiveNet;
         if (p != null)
         {
            if (p.Canvas.ScaleAmount < 10.0F)
            {
               p.Canvas.ScaleAmount += 0.1F;
            }

            p.Canvas.Refresh();
         }
      }

      public override void Unexecute()
      {
      }
   }
}