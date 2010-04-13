using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public abstract class Command
  {
    //Данные
    private PetriNet net;
    private string name;
    private string description;
    private Keys shortcutKeys;
    
    //Акцессоры
    public PetriNet Net
    {
      get
      {
        return net;
      }
      set
      {
        net = value;
      }
    }
    public string Name
    {
      get
      {
        return name;
      }
      set
      {
        name = value;
      }
    }
    
    public string Description
    {
      get
      {
        return description;
      }
      set
      {
        description = value;
      }
    }

    public Keys ShortcutKeys
    {
      get
      {
        return shortcutKeys;
      }
      protected set
      {
        shortcutKeys = value;
      }
    }
    
    //Конструктор
    public Command()
    {
      //По умолчанию команда не влияет на историю
      isHistorical = false;
      Name = "";
      Description = "";
    }
    public abstract void Execute();
    public virtual void UnExecute(){}
    public virtual Image GetPictogram(){return null;}
    public bool isHistorical;
  }
}
