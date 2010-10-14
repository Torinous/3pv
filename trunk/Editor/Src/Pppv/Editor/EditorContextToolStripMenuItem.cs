namespace Pppv.Editor
{
   using System;
   using System.ComponentModel;
   using System.Runtime.InteropServices;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.ApplicationFramework;
   using Pppv.ApplicationFramework.Commands;
   using Pppv.Utils;

   public class EditorContextToolStripMenuItem : CommandToolStripMenuItem
   {
      public EditorContextToolStripMenuItem() : base()
      {
         this.ShortcutKeys = Keys.None;
         this.ShortcutKeyDisplayString = null;
      }

      public EditorContextToolStripMenuItem(InterfaceCommand command) : base(command)
      {
      }

      public void CheckEnabled()
      {
         ElementCommand elementCommand = AssociatedCommand as ElementCommand;
         if (elementCommand != null)
         {
            Enabled = elementCommand.Element != null;
         }
      }
   }
}