namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Shapes;
   using Pppv.Net;

   public class DeleteCommand : ElementInterfaceCommand
   {
      public DeleteCommand()
      {
         this.Name = "Удалить";
         this.Description = "Удалить элемент сети";
         this.ShortcutKeys = Keys.Delete;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Delete.png"), true);
      }

      public DeleteCommand(IShape netElement) : this()
      {
         this.Element = netElement;
      }

      public override void Execute()
      {
         this.DeleteCurrentElement();
      }

      public override void Unexecute()
      {
      }

      private void DeleteCurrentElement()
      {
         this.CheckAndDeleteCurrentElement();
      }

      private void CheckAndDeleteCurrentElement()
      {
         if (this.Element != null)
         {
            PetriNetGraphical net = this.Element.ParentNetGraphical;
            net.DeleteElement(this.Element);
            if (net.Canvas != null)
            {
               net.Canvas.Invalidate();
            }
         }
      }
   }
}