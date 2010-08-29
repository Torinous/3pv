namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Globalization;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Tools;
   using Pppv.Net;
   using Pppv.Utils;

   public class SelectToolCommand : NetCommand
   {
      private Type toolType;

      public SelectToolCommand(Type type)
      {
         this.toolType = type;
         FieldInfo[] fields;

         fields = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic);
         foreach (FieldInfo f in fields)
         {
            DebugAssistant.LogTrace(String.Format(CultureInfo.CurrentCulture, "{0}", f.Name));
            if (f.Name == "name")
            {
               Name = f.GetValue(null) as string;
            }

            if (f.Name == "description")
            {
               Description = f.GetValue(null) as string;
            }

            if (f.Name == "shortcutKeys")
            {
               ShortcutKeys = (Keys)f.GetValue(null);
            }

            if (f.Name == "pictogram")
            {
               Pictogram = (Image)f.GetValue(null);
            }
         }
      }

      public Type ToolType
      {
         get { return this.toolType; }
      }

      public override void Execute()
      {
         EditorApplication app = EditorApplication.Instance;
         app.ActiveNet.SelectToolByType(this.ToolType);
         app.MainFormInst.ToolToolStrip.CheckTool(this.ToolType);
      }

      public override void Unexecute()
      {
      }
   }
}