﻿using System;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using PPPV.Net;

namespace PPPV.Editor.Tools
{
  public class PointerTool : Tool
  {
    /*Данные*/
    static string name;
    static string description;
    static Keys shortcutKeys;
    static Image pictogram;
    
    private Point lastMouseDownPoint;
    private bool isActive = false;
    private Rectangle selectedRectangle;
    private Point selectFrom;

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

    public Rectangle SelectedRectangle
    {
      get
      {
        return selectedRectangle;
      }
      private set
      {
        selectedRectangle = value;
      }
    }

    //cons
    static PointerTool()
    {
      name = "Указатель";
      description = "Инструмент выбора и перемещения элементов сети";
      shortcutKeys = Keys.Control|Keys.Shift|Keys.M;
      pictogram  = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pointer.png"), true);
    }
    
    public PointerTool()
    {
      SelectedRectangle = new Rectangle( new Point(0,0), new Size(0,0));
    }
    
    /*Методы*/
    public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      PetriNetWrapper pnw = ((NetCanvas)sender).Net;
      lastMouseDownPoint = new Point(args.X, args.Y);
      if(args.Button == MouseButtons.Left)
      {
        NetElement tmp = pnw.NetElementUnder(new Point(args.X, args.Y));
        if(tmp!=null)
        {
           if(!pnw.SelectedObjects.Contains(tmp))
           {
              pnw.SelectedObjects.Clear();
              pnw.SelectedObjects.Add(tmp);
           }
        }
        else
        {
           pnw.SelectedObjects.Clear();
           isActive = true;
           selectFrom = new Point(args.X, args.Y);
        }
        ((NetCanvas)sender).Invalidate();
      }
      ((NetCanvas)sender).Paint += DrawSelectionRegion;
      base.HandleMouseDown(sender, args);
    }

    public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      PetriNetWrapper pnw = ((NetCanvas)sender).Net;
      if(args.Button == MouseButtons.Left)
      {
        if(isActive)
        {
           Point startPoint = new Point(selectFrom.X, selectFrom.Y);
           if(args.X < selectFrom.X)
              startPoint.X = args.X;
           if(args.Y < selectFrom.Y)
              startPoint.Y = args.Y;
           selectedRectangle.Location = startPoint;
           selectedRectangle.Size = new Size(Math.Abs(args.X-selectFrom.X), System.Math.Abs(args.Y-selectFrom.Y));
           pnw.SelectedObjects.Clear();
           pnw.SelectedObjects.AddRange(((NetCanvas)sender).Net.NetElementUnder(SelectedRectangle));
           ((NetCanvas)sender).Invalidate();
        }
        else
        {
           Net.NetElement tmpEl;
           Point delta = new Point(args.X - lastMouseDownPoint.X, args.Y - lastMouseDownPoint.Y);
        
           for(int i=0;i<pnw.SelectedObjects.Count;++i)
           {
             ((NetElement)pnw.SelectedObjects[i]).MoveBy(delta);
           }
           ((NetCanvas)sender).Invalidate();
           lastMouseDownPoint.X = args.X;
           lastMouseDownPoint.Y = args.Y;
        }
      }
      base.HandleMouseMove(sender, args);
    }
    
    public override void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      selectedRectangle.Size = new Size(0, 0);
      ((NetCanvas)sender).Paint -= DrawSelectionRegion;
      isActive = false;
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

    public void DrawSelectionRegion(object sender, PaintEventArgs e)
    {
      if(isActive)
      {
        Pen RedPen = new Pen(Color.Red, 1);
        Graphics dc = e.Graphics;
        dc.SmoothingMode = SmoothingMode.HighQuality;
        dc.DrawRectangle(RedPen, SelectedRectangle);
      }
    }
  }
}