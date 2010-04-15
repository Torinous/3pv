using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;


using PPPV.Editor.Commands;
using PPPV.Utils;

namespace PPPV.Editor
{
  public class EditorToolStripMenuItem : ToolStripMenuItem
  {
    /*Мега хук чтобы избавиться от строк вида Ctrl-Oemplus*/
    [DllImport("User32.dll")]
    static extern int MapVirtualKey(int uCode, int uMapType);
    const int MAPVK_VK_TO_CHAR = 2;
  
    //Данные
    Command command;

    //Конструкторы
    public EditorToolStripMenuItem(Command c_)
    {
      command = c_;
      this.Text = command.Name;
      this.Image = command.GetPictogram();
      this.ToolTipText = command.Description;
      this.Size = new System.Drawing.Size(25,20);
      this.ShortcutKeys = command.ShortcutKeys;
      this.Click += ClickHandler;
      this.SetShortcutString();
    }

    public EditorToolStripMenuItem():base()
    {
      this.Size = new System.Drawing.Size(25,20);
      this.Click += ClickHandler;
    }
    
    private void ClickHandler(object sender, EventArgs e)
    {
      if(command != null)
      {
        DebugAssistant.LogTrace(command.ToString());
        command.Execute();
      }
    }

    private void SetShortcutString()
    {
      // testToolStripMenuItem имеет хоткей вида Ctrl+Oem5, я хочу сделать из него Ctrl+\

      //Получаем только код клавиши, без модификаторов.
      Keys k = this.ShortcutKeys & Keys.KeyCode;
      //Конвертим его в строку, тут будет строка "Oem5"
      string s = TypeDescriptor.GetConverter(typeof (Keys)).ConvertToString(k);
      string s1 = string.Empty;
      //Проверяем, что у нас oem`ное значение enum Keys
      if (s.StartsWith("oem", StringComparison.InvariantCultureIgnoreCase))
      {
        //Та самая магия. Получаем значение char по кейкоду
        char c = (char)MapVirtualKey((int)k, MAPVK_VK_TO_CHAR);
        if (c != 0)
          s1 = c.ToString();
      }
      if (s1 != string.Empty)
      {
        //Получаем полный текст хоткея
        string t = TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(this.ShortcutKeys);
        t = t.Replace(s, s1);
        this.ShortcutKeyDisplayString = t;
      }
    }
  }
}

