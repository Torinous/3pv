using System.Windows.Forms;
using System;

using PPPV.Editor.Commands;
using PPPV.Utils;

namespace PPPV.Editor
{
  public class EditorToolStripMenuItem : ToolStripMenuItem
  {
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
  }
}
