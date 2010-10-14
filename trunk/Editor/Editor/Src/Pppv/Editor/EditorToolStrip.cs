namespace Pppv.Editor
{
   using System;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.ApplicationFramework;
   using Pppv.ApplicationFramework.Commands;

   public class EditorToolStrip : CommandToolStrip
   {
      public EditorToolStrip()
      {
      }

      public void CheckToolByType(Type type)
      {
         foreach (CommandToolStripButton b in Items)
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
         foreach (CommandToolStripButton b in Items)
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

      private void ActiveNetChangeHandler(object sender, EventArgs e)
      {
         this.UpdateEnabledState();
      }
   }
}