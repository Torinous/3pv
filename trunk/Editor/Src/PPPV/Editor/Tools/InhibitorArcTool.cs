namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.Net;

   public class InhibitorArcTool : Tool
   {
      private static string name  = "Ингибиторная дуга";
      private static string description = "Инструмент создание ингибиторных дуг сети";
      private static Keys shortcutKeys = Keys.Control | Keys.Shift | Keys.I;
      private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Inhibitor Arc.png"), true);
      private InhibitorArc arc;

      public InhibitorArcTool()
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
      
      public InhibitorArc Arc
      {
         get 
         { 
            return this.arc;
         }

         private set
         {
            if (this.arc != null)
            {
               EditorApplication app = EditorApplication.Instance;
               app.ActiveNet.Paint -= this.arc.Draw;
            }

            this.arc = value;

            if (this.arc != null)
            {
               EditorApplication app = EditorApplication.Instance;
               app.ActiveNet.Paint += this.arc.Draw;
            }
         }
      }

      protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         if (args.Button == MouseButtons.Left)
         {
            PetriNet pn = canvas.Net;
            NetElement clicked = pn.NetElementUnder(new Point(args.X, args.Y));
            if (this.Arc == null)
            {
               if (!(clicked is Arc) && clicked != null)
               {
                  this.Arc = new InhibitorArc(clicked);
               }
            }
            else
            {
               if (clicked != null && this.Arc.Source.GetType() != clicked.GetType())
               {
                  this.Arc.Target = clicked;
                  AddNetElementCommand c = new AddNetElementCommand(pn);
                  c.Element = this.Arc;
                  c.Execute();
                  this.Arc = null;
               }
            }

            canvas.Invalidate();
         }

         base.HandleMouseDown(canvas, args);
      }

      protected override void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         if (args.Button == MouseButtons.Left)
         {
            PetriNet pn = canvas.Net;
            NetElement clicked = pn.NetElementUnder(new Point(args.X, args.Y));
            clicked = (clicked is Arc) ? null : clicked;

            // if(clicked != null && !pn.HaveUnfinishedArcs())
            // pn.AddArc(clicked);
            canvas.Invalidate();
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
         base.HandleKeyDown(canvas, args);
      }
   }
}