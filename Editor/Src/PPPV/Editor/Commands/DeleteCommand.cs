namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class DeleteCommand : ElementCommand
   {
      public DeleteCommand()
      {
         this.Name = "Удалить";
         this.Description = "Удалить выделенный элемент сети";
         this.ShortcutKeys = Keys.Delete;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Delete.png"), true);
      }

      public DeleteCommand(PetriNet net, NetElement netElement) : this()
      {
         this.Net = net;
         this.Element = netElement;
      }

      public override void Execute()
      {
         EditorApplication ap = EditorApplication.Instance;
         PetriNetWrapper pn = ap.ActiveNet;
         foreach (NetElement ne in pn.SelectedObjects)
         {
            pn.DeleteElement(ne);
         }

         pn.SelectedObjects.Clear();
      }

      public override void Unexecute()
      {
      }
   }
}