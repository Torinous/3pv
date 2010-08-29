﻿namespace Pppv.Editor.Tools
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
         get 
         { 
            return this.arc; 
         }

         private set
         {
            if (this.arc != null)
            {
               EditorApplication app = EditorApplication.Instance;
               this.arc.ParentNet = app.ActiveNet;
            }

            this.arc = value;

            if (this.arc != null)
            {
               EditorApplication app = EditorApplication.Instance;
               this.arc.ParentNet = null;
            }
         }
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
                  Arc = new Arc(clicked);
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
                  Arc = null;
               }
            }

            canvas.Invalidate();
         }

         base.HandleMouseDown(canvas, args);
      }

      protected override void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         if (Arc != null)
         {
            Arc.TargetPilon = new Point(args.X, args.Y);
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
         if (args.KeyCode == Keys.Escape)
         {
            if (Arc.Target == null)
            {
               Arc = null;
               canvas.Invalidate(); // TODO: полный Invalidate это нехорошо!!!
            }
         }
      }
   }
}