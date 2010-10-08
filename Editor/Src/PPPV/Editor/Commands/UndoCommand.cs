namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;

   public class UndoCommand : InterfaceCommand
   {
      public UndoCommand()
      {
         this.Name = "Отмена";
         this.Description = "Отмена последнего выполненного действия";
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Undo.png"), true);
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