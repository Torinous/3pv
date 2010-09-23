namespace Pppv.Editor
{
   using System;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;

   public class EditorToolStrip : ToolStrip
   {
      public EditorToolStrip(params Command[] cmdList)
      {
         for (int i = 0; i < cmdList.Length; i++)
         {
            Items.Add(new EditorToolStripButton(cmdList[i]));
            Items[Items.Count - 1].DisplayStyle = ToolStripItemDisplayStyle.Image;
         }
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
   }
}