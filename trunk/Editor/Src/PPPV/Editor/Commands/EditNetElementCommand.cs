namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Shapes;
   using Pppv.Net;

   public class EditNetElementCommand : ElementInterfaceCommand
   {
      public EditNetElementCommand(IShape netElement)
      {
         this.Element = netElement;
         this.Name = "Редактаровать элемент сети";
         this.Description = "Команда редактирования свойств елемента сети";
      }

      public override void Execute()
      {
         MainForm mainForm = this.Element.ParentNetGraphical.Canvas.FindForm() as MainForm;

         if (Element is ArcShape)
         {
            Form f = new ArcEditForm((IArc)Element);
            f.ShowDialog(mainForm);
            f.Dispose();
         }

         if (Element is TransitionShape)
         {
            Form f = new GuardEditForm((ITransition)Element);
            f.ShowDialog(mainForm);
            f.Dispose();
         }

         if (Element is PlaceShape)
         {
            Form f = new PlaceEditForm((IPlace)Element);
            f.ShowDialog(mainForm);
            f.Dispose();
         }

         Element.ParentNetGraphical.Canvas.Invalidate();
      }

      public override void Unexecute()
      {
      }
   }
}