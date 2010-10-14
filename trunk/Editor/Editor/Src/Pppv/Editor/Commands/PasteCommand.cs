namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.ApplicationFramework.Commands;

   public class PasteCommand : EditorInterfaceCommand
   {
      public PasteCommand()
      {
         this.Name = "Вставить";
         this.Description = "Вставить элемент сети из буфера обмена";
         this.ShortcutKeys = Keys.Control | Keys.P;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Paste.png"), true);
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