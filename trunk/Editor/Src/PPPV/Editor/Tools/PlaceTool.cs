namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Editor.Commands;

   public class PlaceTool : Tool
   {
      /*Данные*/
      static string name = "Позиция";
      static string description = "Инструмент создания позиций сети";
      static Keys shortcutKeys = Keys.Control|Keys.Shift|Keys.P;
      static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Place.png"), true);
      
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

      public PlaceTool()
      {
      }

      /*Методы*/
      public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         NetCanvas someCanvas = sender as Editor.NetCanvas;
         if(args.Button == MouseButtons.Left)
         {
            AddNetElementCommand c = new AddNetElementCommand(someCanvas.Net);
            c.Element = new Place(new Point(args.X, args.Y));
            c.Execute();
            someCanvas.Invalidate();
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