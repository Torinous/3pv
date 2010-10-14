/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 9:16
 */
 
namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Shapes;
   using Pppv.Net;
   using Pppv.ApplicationFramework.Commands;

   public class DeleteSelectedCommand : NetInterfaceCommand
   {
      public DeleteSelectedCommand()
      {
         this.Name = "Удалить выделенное";
         this.Description = "Удалить выделенные элементы сети";
         this.ShortcutKeys = Keys.Delete;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Delete.png"), true);
      }

      public DeleteSelectedCommand(PetriNetGraphical petriNet) : this()
      {
         this.Net = petriNet;
      }

      public override void Execute()
      {
         this.DetermineTargetNetIfNeed();
         if (this.Net != null)
         {
            this.DetermineAndDeleteElements();
         }
      }

      public override void Unexecute()
      {
      }

      public override bool CheckEnabled()
      {
         return CheckFormAndActiveNet();
      }

      private void DetermineTargetNetIfNeed()
      {
         MainForm mainForm = MainForm.Instance;
         if (this.Net == null)
         {
            this.Net = mainForm.ActiveNet;
         }
      }

      private void DetermineAndDeleteElements()
      {
         if (this.Net.SelectedObjects.Count > 0)
         {
            MacroCommand mc = new MacroCommand();
            foreach (IShape netElement in this.Net.SelectedObjects)
            {
               mc.Add(new DeleteCommand(netElement));
            }

            mc.Execute();
         }
      }
   }
}