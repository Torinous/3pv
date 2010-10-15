namespace Pppv.ApplicationFramework
{
   using System;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.ApplicationFramework.Commands;

   public class CommandToolStrip : ToolStrip
   {
      public CommandToolStrip()
      {
      }

      public void AddCommand(IInterfaceCommand command)
      {
         Items.Add(new CommandToolStripButton(command));
         Items[Items.Count - 1].DisplayStyle = ToolStripItemDisplayStyle.Image;
         Items[Items.Count - 1].Enabled = command.CheckEnabled();
      }

      protected void UpdateEnabledState()
      {
         foreach (CommandToolStripButton b in Items)
         {
            b.Enabled = b.Command.CheckEnabled();
         }
      }
   }
}