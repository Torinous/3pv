using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using PPPV.Net;

namespace PPPV.Editor {
   public class SelectionController {
      private Rectangle selectedRectangle;
      private ArrayList selectedObjects;
      private Point lastMouseDownPoint;
      private bool isActive = false;
      private Point selectFrom;
      /*Акцессоры доступа*/
      public bool IsActive{
         get{
            return isActive;
         }
         set{
         isActive = value;
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

      /*Конструктор*/
      public SelectionController(){
         SelectedRectangle = new Rectangle( new Point(0,0), new Size(0,0));
         selectedObjects = new ArrayList(20);
      }

      public void Draw(object sender, PaintEventArgs e) {
         Pen RedPen = new Pen(Color.Red, 1);
         Graphics dc = e.Graphics;
         if(IsActive){
            dc.SmoothingMode = SmoothingMode.HighQuality;
            dc.DrawRectangle(RedPen, SelectedRectangle);
         }else{
            /*int i;
            for(i=0;i<selectedObjects.Count;++i) {
               RectangleF tmp = ((NetElement)selectedObjects[i]).HitRegion.GetBounds(dc);
               dc.DrawRectangle(RedPen, new Rectangle((int)tmp.X, (int)tmp.Y, (int)tmp.Width, (int)tmp.Height) );
            }
            */
         }
      }

      public void CanvasMouseMoveHandler(object sender, CanvasMouseEventArgs arg){
         if(arg.Button == MouseButtons.Left){
            switch(arg.currentTool){
               case ToolEnum.Pointer:
                  if(IsActive){
                     /*Point startPoint = new Point(selectFrom.X, selectFrom.Y);
                     if(arg.X < selectFrom.X)
                        startPoint.X = arg.X;
                     if(arg.Y < selectFrom.Y)
                        startPoint.Y = arg.Y;
                     selectedRectangle.Location = startPoint;
                     selectedRectangle.Size = new Size(Math.Abs(arg.X-selectFrom.X),Math.Abs(arg.Y-selectFrom.Y));
                     ((NetCanvas)sender).Invalidate();
                     selectedObjects = ((NetCanvas)sender).Net.NetElementUnder(SelectedRectangle);*/
                  }else{
                     /* Сместим всех выбранных*/
                     NetElement tmpEl;
                     /*Point delta = new Point(arg.X - lastMouseDownPoint.X,arg.Y - lastMouseDownPoint.Y);

                     for(int i=0;i<selectedObjects.Count;++i) {
                        ((NetElement)selectedObjects[i]).MoveBy(delta);
                     }
                     ((NetCanvas)sender).Invalidate();
                     lastMouseDownPoint.X = arg.X;
                     lastMouseDownPoint.Y = arg.Y;*/
                  }
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

      public void CanvasMouseDownHandler(object sender, CanvasMouseEventArgs arg){
         lastMouseDownPoint = new Point(arg.X,arg.Y);
         if(arg.Button == MouseButtons.Left){
            switch(arg.currentTool){
               case ToolEnum.Pointer:
                  /*NetElement tmp = ((NetCanvas)sender).Net.NetElementUnder(new Point(arg.X,arg.Y));
                  if(tmp!=null){
                     if(!selectedObjects.Contains(tmp)){
                        selectedObjects.Clear();
                        selectedObjects.Add(tmp);
                     }
                  }else{
                     selectedObjects.Clear();
                     IsActive = true;
                     selectFrom = new Point(arg.X,arg.Y);
                  }
                  ((NetCanvas)sender).Invalidate();*/
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

      public void CanvasMouseUpHandler(object sender, CanvasMouseEventArgs arg){
         if(arg.Button == MouseButtons.Left){
            switch(arg.currentTool){
               case ToolEnum.Pointer:
                  /*selectedRectangle.Location = new Point(0,0);
                  selectedRectangle.Size = new Size(0,0);
                  ((NetCanvas)sender).Invalidate();
                  IsActive = false;*/
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
   }
}

