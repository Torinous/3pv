namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Shapes;
   using Pppv.Net;

   public class EditNetElementCommand : ElementCommand
   {
      public EditNetElementCommand(IShape netElement)
      {
         this.Element = netElement;
         this.Name = "Редактаровать элемент сети";
         this.Description = "Команда редактирования свойств елемента сети";
      }

      public override void Execute()
      {
         EditorApplication a = EditorApplication.Instance;
         if (Element is ArcShape)
         {
            Form f = new ArcEditForm((IArc)Element);
            f.ShowDialog(a.MainFormInst);
            f.Dispose();
         }

         if (Element is TransitionShape)
         {
            Form f = new GuardEditForm((ITransition)Element);
            f.ShowDialog(a.MainFormInst);
            f.Dispose();
         }

         if (Element is PlaceShape)
         {
            Form f = new PlaceEditForm((IPlace)Element);
            f.ShowDialog(a.MainFormInst);
            f.Dispose();
         }

         Element.ParentNetGraphical.Canvas.Invalidate();
      }

      public override void Unexecute()
      {
      }
   }
}