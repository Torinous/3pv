using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor.Commands
{
  public class DeleteCommand : ElementCommand
  {
    //Данные

    //Конструктор
    public DeleteCommand()
    {
      Name = "Удалить";
      Description = "Удалить выделенный элемент сети";
      ShortcutKeys = Keys.Delete;
      Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Delete.png"), true);
    }
    
    public DeleteCommand(PetriNet p, NetElement n):this()
    {
      Net = p;
      Element = n;
    }
    
    //Методы
    public override void Execute()
    {
      EditorApplication ap = EditorApplication.Instance;
      PetriNetWrapper pn = ap.ActiveNet;
      foreach(NetElement ne in pn.SelectedObjects)
      {
        pn.ElementNullPortal = ne;
        
      }
      pn.SelectedObjects.Clear();
    }

    public override void UnExecute()
    {
      
    }
  }
}
