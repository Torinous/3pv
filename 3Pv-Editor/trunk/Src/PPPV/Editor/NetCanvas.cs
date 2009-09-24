using System;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

using PPPv.Net;
using PPPv.Utils;

namespace PPPv.Editor{

   public class NetCanvas : Panel{
      /*Поля*/
      private NetToolStrip toolController;
      private PetriNet net;
      private int _gridStep;
      private SelectionController selectionController;
      private ContextMenuStrip _contextMenuStrip;
      private ContextMenuController contextMenuController;
      /*Для контроля выбора рамкой*/
      private Rectangle selectedRectangle;
      private Point selectFrom;
      private bool isSelectionActive = false;

      /*Акцессоры доступа*/
      public NetToolStrip ToolController{
         get{
            return toolController;
         }
         private set {
            toolController = value;
         }
      }
      
      public PetriNet Net{
         get{
            return net;
         }
         private set{
            net = value;
         }
      }

      public Rectangle SelectedRectangle{
         get{
            return selectedRectangle;
         }
         private set{
            selectedRectangle = value;
         }
      }

      /*События*/
      public event CanvasMouseEventHandler CanvasMouseClick;
      public event CanvasMouseEventHandler CanvasMouseMove;
      public event CanvasMouseEventHandler CanvasMouseDown;
      public event CanvasMouseEventHandler CanvasMouseUp;

      public event RegionSelectionEventHandler RegionSelectionStart;
      public event RegionSelectionEventHandler RegionSelectionEnd;
      public event RegionSelectionEventHandler RegionSelectionUpdate;

      protected override void OnPaintBackground(PaintEventArgs e)
      {
        //Don't allow the background to paint
         using(PreciseTimer pr = new PreciseTimer("NetCanvas.OnPaintBackground")){
            Graphics dc = e.Graphics;
            Pen GrayPen = new Pen(Color.FromArgb(255,170,170,170), 1);
            dc.SmoothingMode = SmoothingMode.HighQuality;
            dc.Clear(Color.White);
            int x,y;
            y = _gridStep*((e.ClipRectangle.Top-1)/_gridStep) + _gridStep;
            x = _gridStep*((e.ClipRectangle.Left-1)/_gridStep) + _gridStep;
            for(;y<=e.ClipRectangle.Bottom;y+=_gridStep){
               dc.DrawLine(GrayPen,e.ClipRectangle.Left,y,e.ClipRectangle.Right,y);
            }
            for(;x<=e.ClipRectangle.Right;x+=_gridStep){
               dc.DrawLine(GrayPen,x,e.ClipRectangle.Top,x,e.ClipRectangle.Bottom);
            }
         }
      }

      /*Конструктор*/
      public NetCanvas() {
         this.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)| AnchorStyles.Left)| AnchorStyles.Right)));
         this.Location = new Point(1, 1);
         this.Name = "NetCanvas";
         this.Size = new Size(597, 226);
         //this.Size = new Size(Parent.ClientRectangle.Size.Height, Parent.ClientRectangle.Size.Width); Parent=null!!!
         _gridStep = 30;
         this.BackColor = Color.FromArgb(0,50,50,50);
         this.BorderStyle = BorderStyle.FixedSingle;
         
         InitializeComponent();

         /*Включим двойную буферизацию*/
         this.SetStyle( ControlStyles.AllPaintingInWmPaint |  ControlStyles.UserPaint |  ControlStyles.DoubleBuffer, true);

         selectionController = new SelectionController();

         contextMenuController = new ContextMenuController(this);

         /*Пристыкуем события*/
         this.Paint += Draw;
         this.CanvasMouseClick += CanvasMouseClickHandler;
         this.CanvasMouseMove += CanvasMouseMoveHandler;
         this.CanvasMouseMove += selectionController.CanvasMouseMoveHandler;
         this.CanvasMouseDown += CanvasMouseDownHandler;
         this.CanvasMouseDown += selectionController.CanvasMouseDownHandler;
         this.CanvasMouseUp += CanvasMouseUpHandler;
         this.CanvasMouseUp += selectionController.CanvasMouseUpHandler;
         this.ParentChanged += ParentChangedHandler;
      }

      public NetCanvas(PetriNet _net):this(){
         Net = _net;
      }

      private void ParentChangedHandler(object sender, EventArgs arg){
         Form f = this.FindForm();
         if(f != null){
            f.KeyDown += CanvasKeyDownHandler;
         }
      }

      public void Draw(object sender, PaintEventArgs e){
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         Pen RedPen = new Pen(Color.Red, 1);

         dc.DrawRectangle(RedPen, SelectedRectangle);
      }

      private void InitializeComponent() {
      }

      protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e){
         OnCanvasMouseClick(e);
         base.OnMouseClick(e);
      }

      protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e){
         OnCanvasMouseMove(e);
         base.OnMouseMove(e);
      }

      protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e){
         OnCanvasMouseDown(e);
         base.OnMouseDown(e);
      }

      protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e){
          OnCanvasMouseUp(e);
          base.OnMouseUp(e);
      }

      public void OnCanvasMouseClick(System.Windows.Forms.MouseEventArgs _arg){
         if(CanvasMouseClick != null){
            CanvasMouseEventArgs arg = new CanvasMouseEventArgs(_arg);
            arg.currentTool = ToolController.CurrentTool();
            CanvasMouseClick(this,arg);
         }
      }

      public void OnCanvasMouseMove(System.Windows.Forms.MouseEventArgs _arg){
         if(CanvasMouseMove != null){
            CanvasMouseEventArgs arg = new CanvasMouseEventArgs(_arg);
            arg.currentTool = ToolController.CurrentTool();
            CanvasMouseMove(this,arg);
         }
      }

      public void OnCanvasMouseDown(System.Windows.Forms.MouseEventArgs _arg){
         if(CanvasMouseDown != null){
            CanvasMouseEventArgs arg = new CanvasMouseEventArgs(_arg);
            arg.currentTool = ToolController.CurrentTool();
            CanvasMouseDown(this,arg);
         }
      }

      public void OnCanvasMouseUp(System.Windows.Forms.MouseEventArgs _arg){
         if(CanvasMouseUp != null){
             CanvasMouseEventArgs arg = new CanvasMouseEventArgs(_arg);
             arg.currentTool = ToolController.CurrentTool();
             CanvasMouseUp(this,arg);
         }
      }


      private void OnCanvasRegionSelectionStart(){
         if(RegionSelectionStart != null){
            RegionSelectionEventArgs args = new RegionSelectionEventArgs();
            args.selectionRectangle = SelectedRectangle;
            RegionSelectionStart(this,args);
         }
      }

      private void OnCanvasRegionSelectionEnd(){
         if(RegionSelectionEnd != null){
            RegionSelectionEventArgs args = new RegionSelectionEventArgs();
            args.selectionRectangle = SelectedRectangle;
            RegionSelectionEnd(this,args);
         }
      }

      private void OnCanvasRegionSelectionUpdate(){
         if(RegionSelectionUpdate != null){
            RegionSelectionEventArgs args = new RegionSelectionEventArgs();
            args.selectionRectangle = SelectedRectangle;
            RegionSelectionUpdate(this,args);
         }
      }

      private void CanvasMouseClickHandler(object sender, CanvasMouseEventArgs arg){
         if(arg.Button == MouseButtons.Right){
            NetElement contextMenuTarget = Net.NetElementUnder(new Point(arg.X,arg.Y));
            contextMenuController.Show( this.PointToScreen(arg.Location), contextMenuTarget, Net);
         }
      }

      private void CanvasMouseMoveHandler(object sender, CanvasMouseEventArgs arg){
         if(arg.Button == MouseButtons.Left){
            switch(arg.currentTool){
               case ToolEnum.Pointer:
                  break;
                case ToolEnum.Place:
                  break;
                case ToolEnum.Transition:
                  break;
                case ToolEnum.Arc:
                  break;
                default:
                  break;
            }
         }
      }

      private void CanvasMouseDownHandler(object sender, CanvasMouseEventArgs arg){
         if(arg.Button == MouseButtons.Left){
            switch(arg.currentTool){
               case ToolEnum.Pointer:
                  NetElement tmp = ((NetCanvas)sender).Net.NetElementUnder(new Point(arg.X,arg.Y));
                  if(tmp == null){
                     OnCanvasRegionSelectionStart();
                  }
                  ((NetCanvas)sender).Invalidate();
                  break;
               case ToolEnum.Place:
                  break;
               case ToolEnum.Transition:
                  break;
               case ToolEnum.Arc:
                  break;
               default:
                  break;
            }
         }
      }

      private void CanvasMouseUpHandler(object sender, CanvasMouseEventArgs arg){
         if(arg.Button == MouseButtons.Left){
            switch(arg.currentTool){
               case ToolEnum.Pointer:
                  break;
               case ToolEnum.Place:
                  break;
               case ToolEnum.Transition:
                  break;
               case ToolEnum.Arc:
                  break;
               default:
                  break;
            }
         }
      }

      private void CanvasKeyDownHandler(object sender, KeyEventArgs arg){
         OnKeyDown(arg);
      }

      protected override void OnParentChanged(EventArgs e)
      {
         this.toolController = (Parent as TabPageForNet).ToolController;
         Net.Canvas = this;
         base.OnParentChanged(e);
      }
   }
}
