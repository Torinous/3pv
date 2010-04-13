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
      }
    }
  }
}
