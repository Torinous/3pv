using System.Reflection;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Tools
{
  public class InhibitorArcTool : Tool
  {
    /*Данные*/
    private static InhibitorArcTool instance;

    /*Акцессоры доступа*/
    public static InhibitorArcTool Instance
    {
      get
      {
         if (instance == null)
         {
            instance = new InhibitorArcTool();
         }
         return instance;
      }
    }
    
    //cons
    private InhibitorArcTool()
    {
      Name = "Ингибиторная дуга";
      Description = "Инструмент создание ингибиторных дуг сети";
      ShortcutKeys = Keys.Control|Keys.Shift|Keys.I;
    }
    
    /*Методы*/
    public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      if(args.Button == MouseButtons.Left)
      {
        Net.NetElement tmp = ((NetCanvas)sender).Net.NetElementUnder(new Point(args.X, args.Y));
        if(tmp == null)
        {
           //((NetCanvas)sender).OnCanvasRegionSelectionStart();
        }
        ((NetCanvas)sender).Invalidate();
      }
      //from selection controller
      //lastMouseDownPoint = new Point(args.X, args.Y);
      if(args.Button == MouseButtons.Left)
      {
        /*NetElement tmp = ((NetCanvas)sender).Net.NetElementUnder(new Point(arg.X,arg.Y));
        if(tmp!=null)
        {
           if(!selectedObjects.Contains(tmp))
           {
              selectedObjects.Clear();
              selectedObjects.Add(tmp);
           }
        }
        else
        {
           selectedObjects.Clear();
           IsActive = true;
           selectFrom = new Point(arg.X,arg.Y);
        }
        ((NetCanvas)sender).Invalidate();*/
      }
      base.HandleMouseDown(sender, args);
    }

    public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      if(args.Button == MouseButtons.Left)
      {
        PetriNet pn = (sender as Editor.NetCanvas).Net;
        NetElement clicked = pn.NetElementUnder(new Point(args.X,args.Y));
        clicked = (clicked is Arc) ? null : clicked;
        //if(clicked != null && !pn.HaveUnfinishedArcs())
          //pn.AddArc(clicked);
        (sender as Editor.NetCanvas).Invalidate();
      }
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
    
    public override Image GetPictogram()
    {
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Inhibitor arc.png"), true);  
    }
  }
}
