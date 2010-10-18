namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.ApplicationFramework.Commands;
   using Pppv.Net;

   public class CopyCommand : EditorInterfaceCommand
   {
      public CopyCommand()
      {
         this.Name = "Копировать";
         this.Description = "Копировать выделенный элемент сети";
         this.ShortcutKeys = Keys.Control | Keys.C;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Copy.png"), true);
      }

      public override void Execute()
      {
      }

      public override void Unexecute()
      {
      }

      public override bool CheckEnabled()
      {
         return CheckFormAndActiveNet();
      }
   }
}