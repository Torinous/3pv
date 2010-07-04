using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class EditNetElementCommand : ElementCommand
  {
    //Данные
    
    //Конструктор
    public EditNetElementCommand(PetriNet n, NetElement p)
    {
      Net = n;
      Element = p;
      Name = "Редактаровать элемент сети";
      Description = "Команда редактирования свойств елемента сети";
    }
    //Методы
    public override void Execute()
    {
      EditorApplication a = EditorApplication.Instance;
      if(Element is Arc)
      {
        Form f = new ArcEditForm((Arc)Element);
        f.ShowDialog(a.MainFormInst);
        f.Dispose();
      }
      if(Element is Transition)
      {
        Form f = new GuardEditForm((Transition)Element);
        f.ShowDialog(a.MainFormInst);
        f.Dispose();
      }
      if(Element is Place)
      {
        Form f = new PlaceEditForm((Place)Element);
        f.ShowDialog(a.MainFormInst);
        f.Dispose();
      }
      Element.ParentNet.Canvas.Invalidate();
    }

    public override void UnExecute()
    {
    }
  }
}
