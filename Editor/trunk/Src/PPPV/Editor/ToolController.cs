/// Класс Singleton

using PPPV.Utils;
using PPPV.Editor.Tools;

namespace PPPV.Editor
{
  public class ToolController : Singleton<ToolController>
  {
    private Tool currentTool;
    
    private ToolController() { }

    public Tool CurrentTool
    {
      get
      {
        return currentTool;
      }
      set
      {
        currentTool = value;
      }
    }
  }
}
