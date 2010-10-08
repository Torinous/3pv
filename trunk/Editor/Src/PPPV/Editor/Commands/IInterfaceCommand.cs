namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public interface IInterfaceCommand : ICommand
   {
      string Description { get; }

      Keys ShortcutKeys { get; }

      Image Pictogram { get; }

      ToolStripItem ParentItem { get; set; }

      bool CheckEnabled();
   }
}
