namespace Pppv.Editor.Tools
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Collections;
   using System.Windows.Forms;
   using System.Drawing.Drawing2D;

   using Pppv.Net;

   public class PointerTool : Tool
   {
      private static string name = "Указатель";
      private static string description = "Инструмент выбора и перемещения элементов сети";
      private static Keys shortcutKeys = Keys.Control|Keys.Shift|Keys.M;
      private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Pointer.png"), true);
      
      private Point lastMouseDownPoint;
      private bool isActive;
      private Rectangle selectedRectangle;
      private Point selectFrom;

      /*Акцессоры доступа*/
      public override string Name{
         get{
            return name;
         }
         set{
            name = value;
         }
      }
      
      public override string Description{
         get{
            return description;
         }
         set{
            description = value;
         }
      }

      public override Keys ShortcutKeys
      {
         get{
            return shortcutKeys;
         }
         set{
            shortcutKeys = value;
         }
      }

      public override Image Pictogram{
         get{
            return pictogram;
         }
         set{
            pictogram = value;
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

      public PointerTool()
      {
         SelectedRectangle = new Rectangle( new Point(0,0), new Size(0,0));
      }

      public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         NetCanvas someCanvas = (NetCanvas)sender;
         PetriNetWrapper pnw = someCanvas.Net;
         lastMouseDownPoint = new Point(args.X, args.Y);
         if(args.Button == MouseButtons.Left)
         {
            NetElement tmp = pnw.NetElementUnder(new Point(args.X, args.Y));
            if(tmp!=null)
            {
               if(!pnw.SelectedObjects.List.Contains(tmp))
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
            someCanvas.Invalidate();
         }
         someCanvas.Paint += DrawSelectionRegion;
         base.HandleMouseDown(sender, args);
      }

      public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         NetCanvas someCanvas = sender as NetCanvas;
         PetriNetWrapper pnw = someCanvas.Net;
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
               pnw.SelectedObjects.List.AddRange(someCanvas.Net.NetElementUnder(SelectedRectangle));
               someCanvas.Invalidate();
            }
            else
            {
               Point delta = new Point(args.X - lastMouseDownPoint.X, args.Y - lastMouseDownPoint.Y);

               for(int i=0;i<pnw.SelectedObjects.List.Count;++i)
               {
                  ((NetElement)pnw.SelectedObjects.List[i]).MoveBy(delta);
               }
               someCanvas.Invalidate();
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

      protected override void HandleKeyDown( object sender, KeyEventArgs arg )
      {
         base.HandleKeyDown(sender, arg);
      }

      void DrawSelectionRegion(object sender, PaintEventArgs e)
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