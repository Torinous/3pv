/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 18:55
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

   using Pppv.ApplicationFramework.Commands;
   using Pppv.Net;

   public class StartPrologInterfaceCommand : InterfaceCommand
   {
      public StartPrologInterfaceCommand()
      {
         this.Name = "Prolog интерпретатор";
         this.Description = "Запустить Prolog интерпретатор";
         this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
      }

      public override void Execute()
      {
         PetriNetVerificator verificator = PetriNetVerificator.Instance;
         verificator.StartPrologInterface();
      }

      public override void Unexecute()
      {
      }
   }
}

