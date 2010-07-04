using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public abstract class Command
  {
    //Данные
    private string name;
    private string description;
    private Keys shortcutKeys;
    private Image pictogram;
    
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

    public Image Pictogram
    {
      get
      {
        return pictogram;
      }
      protected set
      {
        pictogram = value;
      }
    }
    
    //Конструктор
    public Command()
    {
      //По умолчанию команда не влияет на историю
      isHistorical = false;
      Name = "";
      Description = "";
      Pictogram = null;
    }
    
    public abstract void Execute();
    public virtual void UnExecute(){}
    public bool isHistorical;
  }
}
