﻿using System;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

using Pppv.Net;
using Pppv.Utils;

namespace Pppv.Editor
{
  public class NetCanvas : UserControl
  {
    private PetriNetWrapper net;
    private int _gridStep;
    //private SelectionController selectionController;
    private Rectangle selectedRectangle;
    //private Point selectFrom; TODO Удалить?
    //private bool isSelectionActive = false; TODO Удалить?
    private Matrix scaleMatrix;
    private Single scaleAmount;

    /**/
    public NetCanvas()
    {
      this.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
      this.Name = "NetCanvas";
      _gridStep = 30;
      this.BackColor = Color.FromArgb(0,50,50,50);
      this.BorderStyle = BorderStyle.FixedSingle;
      
      ScaleMatrix = new Matrix();
      ScaleAmount = 1.0F;
      ScaleMatrix.Scale(ScaleAmount, ScaleAmount);
      /**/
      this.SetStyle( ControlStyles.AllPaintingInWmPaint |  ControlStyles.UserPaint |  ControlStyles.DoubleBuffer, true);

      /**/
      this.CanvasMouseClick += CanvasMouseClickHandler;
      this.CanvasMouseMove  += CanvasMouseMoveHandler;
      //this.CanvasMouseMove  += selectionController.CanvasMouseMoveHandler;
      this.CanvasMouseDown  += CanvasMouseDownHandler;
      //this.CanvasMouseDown  += selectionController.CanvasMouseDownHandler;
      this.CanvasMouseUp    += CanvasMouseUpHandler;
      //this.CanvasMouseUp    += selectionController.CanvasMouseUpHandler;
      this.ParentChanged    += ParentChangedHandler;
    }

    public NetCanvas(PetriNetWrapper net):this()
    {
      Net = net;
      Net.Canvas = this;
    }

    public Matrix ScaleMatrix
    {
      get
      {
        return scaleMatrix;
      }
      set
      {
        scaleMatrix = value;
      }
    }
    
    public Single ScaleAmount
    {
      get
      {
        return scaleAmount;
      }
      set
      {
        scaleAmount = value;
        ScaleMatrix = new Matrix();
        ScaleMatrix.Scale(ScaleAmount, ScaleAmount);
      }
    }

    public PetriNetWrapper Net
    {
      get
      {
        return net;
      }
      private set
      {
        if(net != null)
        {
          net.Save -= LinkedNetSaveHandler;
          net.Change -= LinkedNetChangeHandler;
        }
        net = value;
        if(net != null)
        {
          net.Save += LinkedNetSaveHandler;
          net.Change += LinkedNetChangeHandler;
        }
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

    /**/
    public event EventHandler<CanvasMouseEventArgs> CanvasMouseClick;
    public event EventHandler<CanvasMouseEventArgs> CanvasMouseMove;
    public event EventHandler<CanvasMouseEventArgs> CanvasMouseDown;
    public event EventHandler<CanvasMouseEventArgs> CanvasMouseUp;
  
    public event RegionSelectionEventHandler RegionSelectionStart;
    public event RegionSelectionEventHandler RegionSelectionEnd;
    public event RegionSelectionEventHandler RegionSelectionUpdate;
  
    public event EventHandler<SaveEventArgs> LinkedNetSave;
    public event EventHandler     LinkedNetChange;
    
    protected override void OnLoad( 	EventArgs e)
    {
      base.OnLoad(e);
      (Parent as TabPageForNet).Resize += ParentResizeHandler;
      SetSize();
    }
  
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      //Don't allow the background to paint
      using(PreciseTimer pr = new PreciseTimer("NetCanvas.OnPaintBackground"))
      {
        Point[] p = {new Point( e.ClipRectangle.Left, e.ClipRectangle.Top),
                     new Point( e.ClipRectangle.Right, e.ClipRectangle.Bottom)};
        Matrix mi = (Matrix)ScaleMatrix.Clone();
        mi.Invert();
        mi.TransformPoints(p);
        Rectangle newClipRectangle = new Rectangle(p[0].X, p[0].Y, p[1].X-p[0].X,p[1].Y-p[0].Y);
        Graphics dc = e.Graphics;
        e.Graphics.Transform = ScaleMatrix;
        Pen GrayPen = new Pen(Color.FromArgb(255,170,170,170), 1);
        dc.SmoothingMode = SmoothingMode.HighQuality;
        dc.Clear(Color.White);
        int x,y;
        y = _gridStep *((newClipRectangle.Top-1)/_gridStep) + _gridStep;
        x = _gridStep *((newClipRectangle.Left-1)/_gridStep) + _gridStep;
        for(; y<=newClipRectangle.Bottom; y+=_gridStep)
        {
          dc.DrawLine(GrayPen, newClipRectangle.Left, y, newClipRectangle.Right, y);
        }
        for(;x<=newClipRectangle.Right; x+=_gridStep)
        {
          dc.DrawLine(GrayPen, x, newClipRectangle.Top, x, newClipRectangle.Bottom);
        }
      }
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.Transform = ScaleMatrix;
      base.OnPaint(e);
    }

    private void ParentChangedHandler(object sender, EventArgs arg){
      Form f = this.FindForm();
      if(f != null)
      {
        f.KeyDown += CanvasKeyDownHandler;
      }
    }

    protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
    {
      OnCanvasMouseClick(e);
      base.OnMouseClick(e);
    }

    protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
    {
      OnCanvasMouseMove(e);
      base.OnMouseMove(e);
    }

    protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
    {
      OnCanvasMouseDown(e);
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
    {
      OnCanvasMouseUp(e);
      base.OnMouseUp(e);
    }

    public void OnCanvasMouseClick(System.Windows.Forms.MouseEventArgs arg)
    {
      if(CanvasMouseClick != null)
      {
        Point[] p = {arg.Location};
        Matrix mi = (Matrix)ScaleMatrix.Clone();
        mi.Invert();
        mi.TransformPoints(p);
        CanvasMouseEventArgs canvasArg = new CanvasMouseEventArgs(arg, p[0]);
        CanvasMouseClick(this, canvasArg);
      }
    }

    public void OnCanvasMouseMove(System.Windows.Forms.MouseEventArgs arg)
    {
      if(CanvasMouseMove != null)
      {
        Point[] p = {arg.Location};
        Matrix mi = (Matrix)ScaleMatrix.Clone();
        mi.Invert();
        mi.TransformPoints(p);
        CanvasMouseEventArgs canvasArg = new CanvasMouseEventArgs(arg, p[0]);
        CanvasMouseMove(this, canvasArg);
      }
    }

    public void OnCanvasMouseDown(System.Windows.Forms.MouseEventArgs arg)
    {
      if(CanvasMouseDown != null)
      {
        Point[] p = {arg.Location};
        Matrix mi = (Matrix)ScaleMatrix.Clone();
        mi.Invert();
        mi.TransformPoints(p);
        CanvasMouseEventArgs canvasArg = new CanvasMouseEventArgs(arg, p[0]);
        CanvasMouseDown(this, canvasArg);
      }
    }

    public void OnCanvasMouseUp(System.Windows.Forms.MouseEventArgs _arg)
    {
      if(CanvasMouseUp != null)
      {
        Point[] p = {_arg.Location};
        Matrix mi = (Matrix)ScaleMatrix.Clone();
        mi.Invert();
        mi.TransformPoints(p);
        CanvasMouseEventArgs arg = new CanvasMouseEventArgs(_arg, p[0]);
        CanvasMouseUp(this,arg);
      }
    }


    private void OnCanvasRegionSelectionStart()
    {
      if(RegionSelectionStart != null)
      {
        RegionSelectionEventArgs args = new RegionSelectionEventArgs();
        args.SelectionRectangle = SelectedRectangle;
        RegionSelectionStart(this,args);
      }
    }

    private void OnCanvasRegionSelectionEnd(){
      if(RegionSelectionEnd != null)
      {
        RegionSelectionEventArgs args = new RegionSelectionEventArgs();
        args.SelectionRectangle = SelectedRectangle;
        RegionSelectionEnd(this,args);
      }
    }

    private void OnCanvasRegionSelectionUpdate(){
      if(RegionSelectionUpdate != null)
      {
        RegionSelectionEventArgs args = new RegionSelectionEventArgs();
        args.SelectionRectangle = SelectedRectangle;
        RegionSelectionUpdate(this,args);
      }
    }

    private void OnLinkedNetSave(SaveEventArgs args){
      if(LinkedNetSave != null)
      {
        LinkedNetSave(this, args);
      }
    }

    private void OnLinkedNetChange(EventArgs args){
      if(LinkedNetChange != null)
      {
        LinkedNetChange(this, args);
      }
    }

    private void CanvasMouseClickHandler(object sender, CanvasMouseEventArgs arg)
    {
      //Передать событие текущему инструменту ToolController`а.
      if(this.Net.CurrentTool != null)
        this.Net.CurrentTool.HandleMouseClick(sender, arg);
      this.Invalidate();
    }

    private void CanvasMouseMoveHandler(object sender, CanvasMouseEventArgs arg)
    {
      //Передать событие текущему инструменту ToolController`а.
      
      if(this.Net.CurrentTool != null)
        this.Net.CurrentTool.HandleMouseMove(sender, arg);
      this.Invalidate();
    }

    private void CanvasMouseDownHandler(object sender, CanvasMouseEventArgs arg)
    {
      //Передать событие текущему инструменту ToolController`а.

      if(this.Net.CurrentTool != null)
        this.Net.CurrentTool.HandleMouseDown(sender, arg);
      this.Invalidate();
    }

    private void CanvasMouseUpHandler(object sender, CanvasMouseEventArgs arg)
    {
      //Передать событие текущему инструменту ToolController`а.
      if(this.Net.CurrentTool != null)
        this.Net.CurrentTool.HandleMouseUp(sender, arg);
      this.Invalidate();
    }

    private void CanvasKeyDownHandler(object sender, KeyEventArgs arg)
    {
      OnKeyDown(arg);
    }

    private void LinkedNetSaveHandler(object sender,Net.SaveEventArgs args)
    {
      OnLinkedNetSave(args);
    }

    private void LinkedNetChangeHandler(object sender, EventArgs args)
    {
      //PetriNet net = sender as PetriNet;
      SetSize();
      OnLinkedNetChange(args);
    }
    
    protected void SetSize()
    {
      int w_ = 0, h_ = 0;
      Point[] p = {new Point(net.Width, net.Height)};
      Matrix mi = (Matrix)ScaleMatrix.Clone();
      //mi.Invert();
      mi.TransformPoints(p);
          
      if(p[0].X >= Parent.ClientRectangle.Width)
        w_ = p[0].X;
      else
        w_ = Parent.ClientRectangle.Width;
      
      if(p[0].Y >= Parent.ClientRectangle.Height)
        h_ =  p[0].Y;
      else
        h_ = Parent.ClientRectangle.Height;
      this.Size = new Size(w_,h_);
    }
    
    private void ParentResizeHandler(object sender, EventArgs args)
    {
      SetSize();
    }
    
    protected override void OnParentChanged(EventArgs e)
    {
      if(Parent != null)
      {
      }
      base.OnParentChanged(e);
    }

    public override void Refresh()
    {
      SetSize();
      base.Refresh();
    }
  }
}

