/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 14.10.2010
 * Time: 12:52
 *
 *
 */
namespace Pppv.Verificator.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;
   using Pppv.ApplicationFramework.Commands;

   public class QuitCommand : InterfaceCommand
   {
      private Form formToClose;

      public QuitCommand()
      {
         this.Name = "Выход";
         this.Description = "Завершение работы приложения 3PV:Verificator";
         this.ShortcutKeys = Keys.Control | Keys.Q;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
      }

      public QuitCommand(Form formToClose)
      {
         this.FormToClose = formToClose;
      }

      public Form FormToClose
      {
         get { return formToClose; }
         set { formToClose = value; }
      }

      public override void Execute()
      {
         PetriNetVerificator verificator = PetriNetVerificator.Instance;
         verificator.StopInterface();
      }

      public override void Unexecute()
      {
      }
   }
}