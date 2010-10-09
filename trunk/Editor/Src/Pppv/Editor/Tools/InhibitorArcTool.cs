namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.Editor.Shapes;
   using Pppv.Net;

   public class InhibitorArcTool : ArcTool
   {
      private static string name  = "Ингибиторная дуга";
      private static string description = "Инструмент создание ингибиторных дуг сети";
      private static Keys shortcutKeys = Keys.Control | Keys.Shift | Keys.I;
      private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Inhibitor Arc.png"), true);

      public InhibitorArcTool(PetriNetGraphical net) : base(net)
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

      protected override ArcShape ArcFabric(INetElement clicked)
      {
         return (ArcShape)this.EventSourceNet.CreateShapeForNetElement(new Arc(clicked, ArcType.InhibitorArc));
      }
   }
}