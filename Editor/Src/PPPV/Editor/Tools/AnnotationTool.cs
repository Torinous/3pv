namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.Net;

   public class AnnotationTool : Tool
   {
      private static string name = "Аннотация";
      private static string description = "Инструмент создания аннотация";
      private static Keys shortcutKeys = Keys.Control | Keys.Shift | Keys.O;
      private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Annotation.png"), true);

      public AnnotationTool(PetriNetGraphical net) : base(net)
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

      protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         if (args.Button == MouseButtons.Left)
         {
         }

         base.HandleMouseDown(canvas, args);
      }

      protected override void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
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