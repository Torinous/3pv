namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Diagnostics;
   using System.Windows.Forms;
   
   using Pppv.Net;
   using Pppv.Utils;

   public class DeleteCommand : ElementCommand
   {
      public DeleteCommand()
      {
         Name = "Удалить";
         Description = "Удалить выделенный элемент сети";
         ShortcutKeys = Keys.Delete;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Delete.png"), true);
      }

      public DeleteCommand(PetriNet net, NetElement netElement):this()
      {
         Net = net;
         Element = netElement;
      }
      public override void Execute()
      {
         EditorApplication ap = EditorApplication.Instance;
         PetriNetWrapper pn = ap.ActiveNet;
         foreach(NetElement ne in pn.SelectedObjects.List)
         {
            pn.ElementNullPortal = ne;
         }
         pn.SelectedObjects.Clear();
      }

      public override void Unexecute()
      {

      }
   }
}
