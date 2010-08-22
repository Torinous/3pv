using System;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Editor.Commands;

namespace PPPV.Editor
{
  public class EditorToolStrip : ToolStrip
  {
    //Данные
    
    public EditorToolStrip(params Command[] cmdList)
    {
      for(int i = 0; i < cmdList.Length; i++)
      {
        Items.Add(new EditorToolStripButton(cmdList[i]));
        Items[Items.Count-1].DisplayStyle = ToolStripItemDisplayStyle.Image;
      }
    }

    public void CheckTool(Type type)
    {
      foreach(EditorToolStripButton b in Items)
      {
        if((b.Command as SelectToolCommand).ToolType.GetType() == type)
          b.Checked = true;
        else
          b.Checked = false;
      }
    }
  }
}