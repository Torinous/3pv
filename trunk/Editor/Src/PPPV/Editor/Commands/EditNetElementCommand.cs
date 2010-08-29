namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;

   public class EditNetElementCommand : ElementCommand
   {
      public EditNetElementCommand(PetriNet net, NetElement netElement)
      {
         this.Net = net;
         this.Element = netElement;
         this.Name = "Редактаровать элемент сети";
         this.Description = "Команда редактирования свойств елемента сети";
      }

      public override void Execute()
      {
         EditorApplication a = EditorApplication.Instance;
         if (Element is Arc)
         {
            Form f = new ArcEditForm((Arc)Element);
            f.ShowDialog(a.MainFormInst);
            f.Dispose();
         }

         if (Element is Transition)
         {
            Form f = new GuardEditForm((Transition)Element);
            f.ShowDialog(a.MainFormInst);
            f.Dispose();
         }

         if (Element is Place)
         {
            Form f = new PlaceEditForm((Place)Element);
            f.ShowDialog(a.MainFormInst);
            f.Dispose();
         }

         Element.ParentNet.Canvas.Invalidate();
      }

      public override void Unexecute()
      {
      }
   }
}