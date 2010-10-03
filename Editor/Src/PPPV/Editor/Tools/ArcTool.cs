namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.Editor.Shapes;
   using Pppv.Net;
   using Pppv.Utils;

   public class ArcTool : Tool
   {
      private static string name  = "Дуга";
      private static string description = "Инструмент создание дуг сети";
      private static Keys shortcutKeys = Keys.Control | Keys.Shift | Keys.A;
      private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Arc.png"), true);
      private ArcShape arc;

      public ArcTool(PetriNetGraphical net) : base(net)
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

      public ArcShape Arc
      {
         get { return this.arc; }
         private set { this.arc = value; }
      }

      protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         if (args.Button == MouseButtons.Left)
         {
            IShape clicked = EventSourceNet.GetElementUnder(new Point(args.X, args.Y));
            if (this.Arc == null)
            {
               if (!(clicked is Arc) && clicked != null)
               {
                  this.Arc = this.ArcFabric(clicked.BaseElement);
                  EventSourceNet.Paint += this.Arc.ParentNetDrawHandler;
               }
            }
            else
            {
               if (clicked != null && this.Arc.Source.GetType() != clicked.GetType())
               {
                  this.Arc.TargetId = clicked.BaseElement.Id;
                  AddNetElementCommand c = new AddNetElementCommand(EventSourceNet);
                  c.Element = this.Arc.BaseElement;
                  c.Execute();
                  EventSourceNet.Paint -= this.Arc.ParentNetDrawHandler;
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
            EventSourceNet.Canvas.Invalidate();
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
            if (this.Arc != null && this.Arc.Target == null)
            {
               this.ClearTemporaryArc();
               canvas.Invalidate();
            }
         }

         base.HandleKeyDown(canvas, args);
      }

      protected virtual ArcShape ArcFabric(INetElement clicked)
      {
         return (ArcShape)EventSourceNet.CreateShapeForNetElement(new Arc(clicked, ArcType.BaseArc));
      }

      protected void ClearTemporaryArc()
      {
         this.Arc.SourceId = string.Empty;
         EventSourceNet.Paint -= this.Arc.ParentNetDrawHandler;
         this.Arc = null;
      }
   }
}