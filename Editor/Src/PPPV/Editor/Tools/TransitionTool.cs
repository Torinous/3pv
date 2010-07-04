using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Editor.Commands;

namespace PPPV.Editor.Tools
{
  public class TransitionTool : Tool
  {
    /*Данные*/
    static string name;
    static string description;
    static Keys shortcutKeys;
    static Image pictogram;
    
    /*Акцессоры доступа*/
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
      set
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
      set
      {
        pictogram = value;
      }
    }

    //cons
    static TransitionTool()
    {
      name = "Переход";
      description = "Инструмент создания переходов сети";
      shortcutKeys = Keys.Control|Keys.Shift|Keys.T;
      pictogram   = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Transition.png"), true);
    }
    
    public TransitionTool()
    {
    }
    
    /*Методы*/
    public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      if(args.Button == MouseButtons.Left)
      {
        AddNetElementCommand c = new AddNetElementCommand((sender as Editor.NetCanvas).Net);
        c.Element = new Transition(new Point(args.X, args.Y));
        c.Execute();
      }
      base.HandleMouseDown(sender, args);
    }

    public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      base.HandleMouseMove(sender, args);
    }
    
    public override void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      base.HandleMouseUp(sender, args);
    }

    public override void HandleMouseClick(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      base.HandleMouseClick(sender, args);
    }

    public override void HandleKeyDown( object sender, KeyEventArgs args )
    {
      base.HandleKeyDown(sender, args);
    }
  }
}
