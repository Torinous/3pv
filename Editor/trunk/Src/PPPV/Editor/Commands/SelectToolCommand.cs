using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Editor.Tools;

namespace PPPV.Editor.Commands
{
  public class SelectToolCommand : Command
  {
    //�����
    private Tool tool;

    //������� ����㯠
    public Tool Tool
    {
      get
      {
        return tool;
      }
    }
    //���������
    public SelectToolCommand(Tool t)
    {
      tool = t;
      Name = tool.Name;
      Description = tool.Description;
      ShortcutKeys = tool.ShortcutKeys;
    }

    //��⮤�
    public override void Execute()
    {
      ToolController tc = PPPV.Editor.ToolController.Instance;
      tc.CurrentTool = tool;
      EditorApplication app = EditorApplication.Instance;
      app.MainFormInst.ToolToolStrip.CheckTool(this.tool.GetType());
    }

    public override void UnExecute()
    {
    }
    
    public override Image GetPictogram()
    {
      return tool.GetPictogram();
    }
  }
}
