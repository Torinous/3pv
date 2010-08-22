namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Editor.Commands;

   public class AnnotationTool : Tool
   {
      static string name = "Аннотация";
      static string description = "Инструмент создания аннотация";
      static Keys shortcutKeys = Keys.Control|Keys.Shift|Keys.O;
      static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Annotation.png"), true);

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

      public override Keys ShortcutKeys{
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

      public AnnotationTool()
      {
      }

      /*Методы*/
      public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         if(args.Button == MouseButtons.Left)
         {
            Command c = new AddTransitionCommand((sender as Editor.NetCanvas).Net, new Point(args.X, args.Y));
            c.Execute();
         }
         base.HandleMouseDown(sender, args);
      }

      public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         if(args.Button == MouseButtons.Left)
         {
            //if(IsActive)
            {
               /*Point startPoint = new Point(selectFrom.X, selectFrom.Y);
               if(arg.X < selectFrom.X)
                  startPoint.X = arg.X;
               if(arg.Y < selectFrom.Y)
                  startPoint.Y = arg.Y;
               selectedRectangle.Location = startPoint;
               selectedRectangle.Size = new Size(Math.Abs(arg.X-selectFrom.X),Math.Abs(arg.Y-selectFrom.Y));
               ((NetCanvas)sender).Invalidate();
               selectedObjects = ((NetCanvas)sender).Net.NetElementUnder(SelectedRectangle);*/
            }
            //else
            {
               //Net.NetElement tmpEl;
               /*Point delta = new Point(arg.X - lastMouseDownPoint.X,arg.Y - lastMouseDownPoint.Y);

               for(int i=0;i<selectedObjects.Count;++i) {
                  ((NetElement)selectedObjects[i]).MoveBy(delta);
               }
               ((NetCanvas)sender).Invalidate();
               lastMouseDownPoint.X = arg.X;
               lastMouseDownPoint.Y = arg.Y;*/
            }
         }
         base.HandleMouseMove(sender, args);
      }

      public override void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         base.HandleMouseUp(sender, args);
      }

      public override void HandleMouseClick(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         base.HandleMouseClick(sender, args);
      }

      protected override void HandleKeyDown( object sender, KeyEventArgs args )
      {
         base.HandleKeyDown(sender, args);
      }
   }
}