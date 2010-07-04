using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;
using PPPV.Editor.Tools;

namespace PPPV.Editor.Commands
{
  public class SelectToolCommand : NetCommand
  {
    //Данные
    private Type toolType;

    //Акцессоры доступа
    public Type ToolType
    {
      get
      {
        return toolType;
      }
    }
    //Конструктор
    public SelectToolCommand(Type t)
    {
      toolType = t;
      FieldInfo[] fields;

      fields = t.GetFields( BindingFlags.Static | BindingFlags.NonPublic );
      foreach(FieldInfo f in fields)
      {
        DebugAssistant.LogTrace(String.Format("{0}",f.Name));
        if(f.Name == "name")
          Name = f.GetValue(null) as string;
        if(f.Name == "description")
          Description = f.GetValue(null) as string;
        if(f.Name == "shortcutKeys")
          ShortcutKeys = (Keys)f.GetValue(null);
        if(f.Name == "pictogram")
          Pictogram = (Image)f.GetValue(null);
      }
    }

    public override void Execute()
    {
      EditorApplication app = EditorApplication.Instance;
      //Установим инструмент для текущей сети
      app.ActiveNet.SelectToolByType(ToolType);
      //Отметим его на панели инструментов
      app.MainFormInst.ToolToolStrip.CheckTool(ToolType);
    }

    public override void UnExecute()
    {
    }
  }
}
