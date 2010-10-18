namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Globalization;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.ApplicationFramework.Commands;
   using Pppv.Editor.Tools;
   using Pppv.Net;
   using Pppv.Utils;

   public class SelectToolCommand : EditorInterfaceCommand
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
         MainForm mainForm = MainForm.Instance;
         if (mainForm != null)
         {
            if (mainForm.ActiveNet != null)
            {
               mainForm.ActiveNet.SelectToolByType(this.ToolType);
               mainForm.ToolToolStrip.CheckToolByType(this.ToolType);
            }
         }
      }

      public override void Unexecute()
      {
      }

      public override bool CheckEnabled()
      {
         return CheckFormAndActiveNet();
      }
   }
}