namespace PPPV.Editor
{
	using System;
	using System.Windows.Forms;
	using System.ComponentModel;

	using System.Globalization;
	
	using PPPV.Editor.Commands;
	using PPPV.Utils;
	
  public class EditorToolStripMenuItem : ToolStripMenuItem
  {
    const int MAPVK_VK_TO_CHAR = 2;
  
    //Данные
    Command associatedCommand;
    
	public Command AssociatedCommand {
		get { return associatedCommand; }
		set { associatedCommand = value; }
	}

    //Конструкторы
    public EditorToolStripMenuItem(Command command):base(command.Name, command.Pictogram)
    {
      AssociatedCommand = command;
      this.ToolTipText = AssociatedCommand.Description;
      this.Size = new System.Drawing.Size(25,20);
      this.ShortcutKeys = AssociatedCommand.ShortcutKeys;
      this.Click += ClickHandler;
      this.SetShortcutString();
    }

    public EditorToolStripMenuItem():base()
    {
      //this.Size = new System.Drawing.Size(25,20);
      this.Click += ClickHandler;
    }
    
    protected void ClickHandler(object sender, EventArgs e)
    {
      if(AssociatedCommand != null)
      {
        DebugAssistant.LogTrace(AssociatedCommand.ToString());
        AssociatedCommand.Execute();
      }
    }

    protected void SetShortcutString()
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
        char c = (char)NativeMethods.MapVirtualKey((int)k, MAPVK_VK_TO_CHAR);
        if (c != 0)
          s1 = c.ToString(CultureInfo.CurrentCulture);
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

