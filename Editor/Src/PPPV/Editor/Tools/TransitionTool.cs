namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Editor.Commands;

   public class TransitionTool : Tool
   {
      private static string name  = "Переход";
      private static string description = "Инструмент создания переходов сети";
      private static Keys shortcutKeys = Keys.Control|Keys.Shift|Keys.T;
      private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Transition.png"), true);

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
         get { return pictogram; }
         set{ pictogram = value; }
      }

      public TransitionTool()
      {
      }

      public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         if(args.Button == MouseButtons.Left)
         {
            AddNetElementCommand c = new AddNetElementCommand((sender as Editor.NetCanvas).Net);
            c.Element = new Transition(new Point(args.X, args.Y));
            c.Execute();
         }
         base.HandleMouseDown(sender, args);
      }

      public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
      {
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