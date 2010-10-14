namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.ApplicationFramework.Commands;

   public class AboutCommand : InterfaceCommand
   {
      private Control sender;

      public AboutCommand(Control sender)
      {
         Name = "О программе";
         Description = "Вызов формы \"О программе\"";
         this.sender = sender;
      }

      public override void Execute()
      {
         Form f = new AboutForm();
         f.ShowDialog(this.sender.FindForm());
         f.Dispose();
      }

      public override void Unexecute()
      {
      }
   }
}
