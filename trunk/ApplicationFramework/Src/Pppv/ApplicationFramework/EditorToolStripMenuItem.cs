namespace Pppv.ApplicationFramework
{
   using System;
   using System.ComponentModel;
   using System.Globalization;
   using System.Windows.Forms;

   using Pppv.ApplicationFramework.Commands;
   using Pppv.ApplicationFramework.Utils;

   public class CommandToolStripMenuItem : ToolStripMenuItem
   {
      private const int MapVkVkToChar = 2;

      private IInterfaceCommand associatedCommand;

      public CommandToolStripMenuItem(InterfaceCommand command) : base(command.Name, command.Pictogram)
      {
         this.AssociatedCommand = command;
         this.ToolTipText = this.AssociatedCommand.Description;
         this.ShortcutKeys = this.AssociatedCommand.ShortcutKeys;
         this.Click += this.ClickHandler;
         this.SetShortcutString();
      }

      public CommandToolStripMenuItem() : base()
      {
         this.Click += this.ClickHandler;
      }

      public IInterfaceCommand AssociatedCommand
      {
         get
         {
            return this.associatedCommand;
         }

         set
         {
            this.associatedCommand = value;
            this.AssociatedCommand.ParentItem = this as ToolStripItem;
         }
      }

      protected void SetShortcutString()
      {
         // testToolStripMenuItem имеет хоткей вида Ctrl+Oem5, я хочу сделать из него Ctrl+\

         ////Получаем только код клавиши, без модификаторов.
         Keys k = this.ShortcutKeys & Keys.KeyCode;
         ////Конвертим его в строку, тут будет строка "Oem5"
         string s = TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(k);
         string s1 = string.Empty;
         ////Проверяем, что у нас oem`ное значение enum Keys
         if (s.StartsWith("oem", StringComparison.OrdinalIgnoreCase))
         {
            ////Та самая магия. Получаем значение char по кейкоду
            char c = (char)NativeMethods.MapVirtualKey((int)k, MapVkVkToChar);
            if (c != 0)
            {
               s1 = c.ToString(CultureInfo.CurrentCulture);
            }
         }

         if (!String.IsNullOrEmpty(s1))
         {
            ////Получаем полный текст хоткея
            string t = TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(this.ShortcutKeys);
            t = t.Replace(s, s1);
            this.ShortcutKeyDisplayString = t;
         }
      }

      private void ClickHandler(object sender, EventArgs e)
      {
         if (this.AssociatedCommand != null)
         {
            DebugAssistant.LogTrace(this.AssociatedCommand.ToString());
            this.AssociatedCommand.Execute();
         }
      }
   }
}