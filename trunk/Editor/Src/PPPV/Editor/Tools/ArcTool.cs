namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.Net;
   using Pppv.Utils;

   public class ArcTool : Tool
   {
      private static string name  = "Дуга";
      private static string description = "Инструмент создание дуг сети";
      private static Keys shortcutKeys = Keys.Control | Keys.Shift | Keys.A;
      private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Arc.png"), true);
      private Arc arc;

      public ArcTool()
      {
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

      public Arc Arc
      {
         get { return this.arc; }
         private set { this.arc = value; }
      }

      protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         if (args.Button == MouseButtons.Left)
         {
            PetriNet pn = canvas.Net;
            NetElement clicked = pn.NetElementUnder(new Point(args.X, args.Y));
            if (Arc == null)
            {
               if (!(clicked is Arc) && clicked != null)
               {
                  Arc = this.ArcFabric(clicked);
               }
            }
            else
            {
               if (clicked != null && Arc.Source.GetType() != clicked.GetType())
               {
                  Arc.Target = clicked;
                  AddNetElementCommand c = new AddNetElementCommand(pn);
                  c.Element = Arc;
                  c.Execute();
                  this.Arc.UpdateConnectPoints();
                  this.Arc = null;
               }
            }

            canvas.Invalidate();
         }

         base.HandleMouseDown(canvas, args);
      }

      protected override void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         if (this.Arc != null)
         {
            this.Arc.UpdateConnectPoints();
            this.Arc.ParentNet.Canvas.Invalidate();
         }

         base.HandleMouseMove(canvas, args);
      }

      protected override void HandleMouseUp(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         base.HandleMouseUp(canvas, args);
      }

      protected override void HandleMouseClick(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         base.HandleMouseClick(canvas, args);
      }

      protected override void HandleKeyDown(NetCanvas canvas, KeyEventArgs args)
      {
         if (args.KeyCode == Keys.Escape)
         {
            if (Arc != null && Arc.Target == null)
            {
               this.ClearTemporaryArc();
               canvas.Invalidate();
            }
         }

         base.HandleKeyDown(canvas, args);
      }

      protected virtual Arc ArcFabric(NetElement clicked)
      {
         return new Arc(clicked);
      }

      protected void ClearTemporaryArc()
      {
         Arc.Source = null;
         Arc.ParentNet = null;
         Arc = null;
      }
   }
}