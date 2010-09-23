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
         EditorApplication application = EditorApplication.Instance;
         this.CheckNetAndDeleteCurrentAndSelectedElements(application.ActiveNet);
         Net.Canvas.Invalidate();
      }

      public override void Unexecute()
      {
      }

      private void CheckNetAndDeleteCurrentAndSelectedElements(PetriNetWrapper currentNet)
      {
         if (currentNet != null)
         {
            this.DeleteCurrentAndSelectedElements(currentNet);
         }
         else
         {
            throw new ArgumentNullException("currentNet", "Parameter currentNet is null in DeleteCommand");
         }
      }

      private void DeleteCurrentAndSelectedElements(PetriNetWrapper net)
      {
         net.DeleteElement(this.Element);

         foreach (NetElement ne in net.SelectedObjects)
         {
            net.DeleteElement(ne);
         }

         net.SelectedObjects.Clear();
      }
   }
}