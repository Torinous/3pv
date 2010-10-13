/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 3:42
 *
 *
 */
 
namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Verificator;

   public class AnalyzeCommand : NetInterfaceCommand
   {
      public AnalyzeCommand()
      {
         this.Name = "Анализ";
         this.Description = "Запуск средства анализа над текущей сетью";
         this.ShortcutKeys = Keys.Control | Keys.A;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save.png"), true);
         this.IsHistorical = false;
      }

      public override void Execute()
      {
         MainForm mainForm = MainForm.Instance;
         if (mainForm.ActiveNet != null)
         {
            PetriNetVerificator verificator = PetriNetVerificator.Instance;
            verificator.LoadNetToPrologEngine(mainForm.ActiveNet);
            verificator.StartInterface(mainForm);
            verificator.Cleanup();
         }
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