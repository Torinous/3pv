using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Editor;

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
        ContextMenuController contextMenuController = new ContextMenuController(sender as Editor.NetCanvas);
        PetriNet n = (sender as Editor.NetCanvas).Net;
        NetElement contextMenuTarget = n.NetElementUnder(new Point(args.X, args.Y));
        contextMenuController.Show( (sender as Editor.NetCanvas).PointToScreen(args.Location), contextMenuTarget, n);
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
