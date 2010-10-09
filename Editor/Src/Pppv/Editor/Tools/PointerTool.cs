namespace Pppv.Editor.Tools
{
   using System;
   using System.Collections;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Shapes;
   using Pppv.Net;

   public class PointerTool : Tool
   {
      private static string name = "Указатель";
      private static string description = "Инструмент выбора и перемещения элементов сети";
      private static Keys shortcutKeys = Keys.Control | Keys.Shift | Keys.M;
      private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Pointer.png"), true);

      private Point lastMouseDownPoint;
      private bool isActive;
      private Rectangle selectedRectangle;
      private Point selectFrom;

      public PointerTool(PetriNetGraphical net) : base(net)
      {
         this.SelectedRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
      }

      public override string Name
      {
         get { return name; }
         set { name = value; }
      }
      
      public override string Description
      {
         get { return description; }
         set { description = value; }
      }

      public override Keys ShortcutKeys
      {
         get { return shortcutKeys; }
         set { shortcutKeys = value; }
      }

      public override Image Pictogram
      {
         get { return pictogram; }
         set { pictogram = value; }
      }

      public Rectangle SelectedRectangle
      {
         get { return this.selectedRectangle; }
         private set { this.selectedRectangle = value; }
      }

      protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         PetriNetGraphical net = canvas.Net;
         this.lastMouseDownPoint = new Point(args.X, args.Y);
         if (args.Button == MouseButtons.Left)
         {
            IShape tmp = net.GetElementUnder(new Point(args.X, args.Y));
            if (tmp != null)
            {
               if (!net.SelectedObjects.Contains(tmp))
               {
                  net.SelectedObjects.Clear();
                  net.SelectedObjects.Add(tmp);
               }
            }
            else
            {
               net.SelectedObjects.Clear();
               this.isActive = true;
               this.selectFrom = new Point(args.X, args.Y);
            }

            canvas.Invalidate();
         }

         canvas.Paint += this.DrawSelectionRegion;
         base.HandleMouseDown(canvas, args);
      }

      protected override void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         PetriNetGraphical pnw = canvas.Net;
         if (args.Button == MouseButtons.Left)
         {
            if (this.isActive)
            {
               Point startPoint = new Point(this.selectFrom.X, this.selectFrom.Y);
               if (args.X < this.selectFrom.X)
               {
                  startPoint.X = args.X;
               }

               if (args.Y < this.selectFrom.Y)
               {
                  startPoint.Y = args.Y;
               }

               this.selectedRectangle.Location = startPoint;
               this.selectedRectangle.Size = new Size(Math.Abs(args.X - this.selectFrom.X), System.Math.Abs(args.Y - this.selectFrom.Y));
               pnw.SelectedObjects.Clear();
               pnw.SelectedObjects.AddRange(canvas.Net.GetElementUnder(this.SelectedRectangle));
               canvas.Invalidate();
            }
            else
            {
               Point delta = new Point(args.X - this.lastMouseDownPoint.X, args.Y - this.lastMouseDownPoint.Y);

               for (int i = 0; i < pnw.SelectedObjects.Count; ++i)
               {
                  pnw.SelectedObjects[i].MoveBy(delta);
               }

               canvas.Invalidate();
               this.lastMouseDownPoint.X = args.X;
               this.lastMouseDownPoint.Y = args.Y;
            }
         }

         base.HandleMouseMove(canvas, args);
      }

      protected override void HandleMouseUp(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         this.selectedRectangle.Size = new Size(0, 0);
         canvas.Paint -= this.DrawSelectionRegion;
         this.isActive = false;
         base.HandleMouseUp(canvas, args);
         canvas.Invalidate();
      }

      protected override void HandleMouseClick(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         base.HandleMouseClick(canvas, args);
      }

      protected override void HandleKeyDown(NetCanvas canvas, KeyEventArgs args)
      {
         base.HandleKeyDown(canvas, args);
      }

      private void DrawSelectionRegion(object sender, PaintEventArgs e)
      {
         if (this.isActive)
         {
            Pen redPen = new Pen(Color.Red, 1);
            Graphics dc = e.Graphics;
            dc.SmoothingMode = SmoothingMode.HighQuality;
            dc.DrawRectangle(redPen, this.SelectedRectangle);
         }
      }
   }
}