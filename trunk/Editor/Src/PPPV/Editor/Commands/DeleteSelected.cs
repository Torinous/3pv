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
   using Pppv.Utils;

   public class DeleteSelectedCommand : Command
   {
      private PetriNetGraphical net;

      public DeleteSelectedCommand()
      {
         this.Name = "Удалить выделенное";
         this.Description = "Удалить выделенные элементы сети";
         this.ShortcutKeys = Keys.Delete;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Delete.png"), true);
      }

      public DeleteSelectedCommand(PetriNetGraphical petriNet) : this()
      {
         this.net = petriNet;
      }

      public PetriNetGraphical Net
      {
         get { return this.net; }
         set { this.net = value; }
      }

      public override void Execute()
      {
         if (this.Net != null)
         {
            this.DetermineAndDeleteElements();
         }
         else
         {
            throw new EditorException("Не определена целевая сеть для команды DeleteSelected");
         }
      }

      public override void Unexecute()
      {
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