using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Editor;
using PPPV.Editor.Commands;

namespace PPPV.Editor.Tools
{
  public abstract class Tool
  {
    /*Данные*/
    private string name;
    private string description;
    private Keys shortcutKeys;
    
    //Акцессоры
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
    public Tool()
    {
    }

    /*Методы*/
    public virtual void HandleMouseDown( object sender, System.Windows.Forms.MouseEventArgs args )
    {
    }

    public virtual void HandleMouseMove( object sender, System.Windows.Forms.MouseEventArgs args )
    {
    }

    public virtual void HandleMouseUp( object sender, System.Windows.Forms.MouseEventArgs args )
    {
    }

    public virtual void HandleMouseClick( object sender, System.Windows.Forms.MouseEventArgs args )
    {
      /*Контекстное меню по умолчанию показывают все инструменты*/
      if(args.Button == MouseButtons.Right)
      {
        ShowContextMenuCommand c = new ShowContextMenuCommand(sender as Editor.NetCanvas, args.Location);
        c.Execute();
      }
    }

    public virtual void HandleKeyDown( object sender, KeyEventArgs arg )
    {
      
    }
    
    public virtual Image GetPictogram()
    {
      return null;
    }
  }
}
