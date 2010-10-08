namespace Pppv.Editor
{
   using System;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;

   public class EditorToolStrip : ToolStrip
   {
      public EditorToolStrip()
      {
      }

      public void AddCommand(IInterfaceCommand command)
      {
         Items.Add(new EditorToolStripButton(command));
         Items[Items.Count - 1].DisplayStyle = ToolStripItemDisplayStyle.Image;
         Items[Items.Count - 1].Enabled = command.CheckEnabled();
      }

      public void CheckToolByType(Type type)
      {
         foreach (EditorToolStripButton b in Items)
         {
            if ((b.Command as SelectToolCommand).ToolType == type)
            {
               b.Checked = true;
            }
            else
            {
               b.Checked = false;
            }
         }
      }

      public void UncheckTool()
      {
         foreach (EditorToolStripButton b in Items)
         {
            b.Checked = false;
         }
      }

      protected override void OnVisibleChanged(EventArgs e)
      {
         MainForm mainForm = MainForm.Instance;
         if (mainForm != null)
         {
            mainForm.ActiveNetChange -= this.ActiveNetChangeHandler;
            mainForm.ActiveNetChange += this.ActiveNetChangeHandler;
         }

         base.OnVisibleChanged(e);
      }

      private void UpdateEnabledState()
      {
         foreach (EditorToolStripButton b in Items)
         {
            b.Enabled = b.Command.CheckEnabled();
         }
      }

      private void ActiveNetChangeHandler(object sender, EventArgs e)
      {
         this.UpdateEnabledState();
      }
   }
}