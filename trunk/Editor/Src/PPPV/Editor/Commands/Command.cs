using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public abstract class Command
  {
    string name;
    string description;
    Keys shortcutKeys;
    Image pictogram;
    bool isHistorical;
    
		public bool IsHistorical {
			get { return isHistorical; }
			set { isHistorical = value; }
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
    
    protected Command()
    {
      Name = "";
      Description = "";
      Pictogram = null;
    }
    
    public abstract void Execute();
    public virtual void UnExecute(){}
  }
}
