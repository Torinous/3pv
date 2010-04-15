using System;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor
{
  public class NetCanvas : UserControl
  {
    private PetriNet net;
    private int _gridStep;
    //private SelectionController selectionController;
    private Rectangle selectedRectangle;
    private Point selectFrom;
    private bool isSelectionActive = false;
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

    public NetCanvas(PetriNet _net):this()
    {
      Net = _net;
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

    public PetriNet Net
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
    public event CanvasMouseEventHandler CanvasMouseClick;
    public event CanvasMouseEventHandler CanvasMouseMove;
    public event CanvasMouseEventHandler CanvasMouseDown;
    public event CanvasMouseEventHandler CanvasMouseUp;
  
    public event RegionSelectionEventHandler RegionSelectionStart;
    public event RegionSelectionEventHandler RegionSelectionEnd;
    public event RegionSelectionEventHandler RegionSelectionUpdate;
  
    public event SaveEventHandler LinkedNetSave;
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

    public void OnCanvasMouseClick(System.Windows.Forms.MouseEventArgs _arg)
    {
      if(CanvasMouseClick != null)
      {
        Point[] p = {_arg.Location};
        Matrix mi = (Matrix)ScaleMatrix.Clone();
        mi.Invert();
        mi.TransformPoints(p);
        CanvasMouseEventArgs arg = new CanvasMouseEventArgs(_arg, p[0]);
        CanvasMouseClick(this,arg);
      }
    }

    public void OnCanvasMouseMove(System.Windows.Forms.MouseEventArgs _arg)
    {
      if(CanvasMouseMove != null)
      {
        Point[] p = {_arg.Location};
        Matrix mi = (Matrix)ScaleMatrix.Clone();
        mi.Invert();
        mi.TransformPoints(p);
        CanvasMouseEventArgs arg = new CanvasMouseEventArgs(_arg, p[0]);
        CanvasMouseMove(this,arg);
      }
    }

    public void OnCanvasMouseDown(System.Windows.Forms.MouseEventArgs _arg)
    {
      if(CanvasMouseDown != null)
      {
        Point[] p = {_arg.Location};
        Matrix mi = (Matrix)ScaleMatrix.Clone();
        mi.Invert();
        mi.TransformPoints(p);
        CanvasMouseEventArgs arg = new CanvasMouseEventArgs(_arg, p[0]);
        CanvasMouseDown(this,arg);
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
        args.selectionRectangle = SelectedRectangle;
        RegionSelectionStart(this,args);
      }
    }

    private void OnCanvasRegionSelectionEnd(){
      if(RegionSelectionEnd != null)
      {
        RegionSelectionEventArgs args = new RegionSelectionEventArgs();
        args.selectionRectangle = SelectedRectangle;
        RegionSelectionEnd(this,args);
      }
    }

    private void OnCanvasRegionSelectionUpdate(){
      if(RegionSelectionUpdate != null)
      {
        RegionSelectionEventArgs args = new RegionSelectionEventArgs();
        args.selectionRectangle = SelectedRectangle;
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
      ToolController tc = ToolController.Instance;
      if(tc.CurrentTool != null)
        tc.CurrentTool.HandleMouseClick(sender, arg);
      this.Invalidate();
    }

    private void CanvasMouseMoveHandler(object sender, CanvasMouseEventArgs arg)
    {
      //Передать событие текущему инструменту ToolController`а.
      ToolController tc = ToolController.Instance;
      if(tc.CurrentTool != null)
        tc.CurrentTool.HandleMouseMove(sender, arg);
      this.Invalidate();
    }

    private void CanvasMouseDownHandler(object sender, CanvasMouseEventArgs arg)
    {
      //Передать событие текущему инструменту ToolController`а.
      ToolController tc = ToolController.Instance;
      if(tc.CurrentTool != null)
        tc.CurrentTool.HandleMouseDown(sender, arg);
      this.Invalidate();
    }

    private void CanvasMouseUpHandler(object sender, CanvasMouseEventArgs arg)
    {
      //Передать событие текущему инструменту ToolController`а.
      ToolController tc = ToolController.Instance;
      if(tc.CurrentTool != null)
        tc.CurrentTool.HandleMouseUp(sender, arg);
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

