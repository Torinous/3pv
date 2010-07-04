using System.Reflection;
using System.Windows.Forms;

using PPPV.Editor.Commands;

namespace PPPV.Editor
{
  public class EditorToolStripButton : System.Windows.Forms.ToolStripButton
  {
    //Данные
    private Command command;
    public Command Command
    {
      get
      {
        return command;
      }
    }
    
    public EditorToolStripButton(Command c)
    {
      this.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.ImageScaling = ToolStripItemImageScaling.SizeToFit;
      this.Name = c.Name;
      this.Text = c.Description;
      this.Image = c.Pictogram;
      this.command = c;
      this.Click += new System.EventHandler(this.ClickHandler);
    }

    protected virtual void ClickHandler(object sender, System.EventArgs e)
    {
      command.Execute();
    }
  }
}
