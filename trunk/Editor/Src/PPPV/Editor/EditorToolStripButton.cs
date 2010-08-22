using System.Reflection;
using System.Windows.Forms;

using PPPV.Editor.Commands;

namespace PPPV.Editor
{
	public class EditorToolStripButton : System.Windows.Forms.ToolStripButton
	{
		Command command;
		public Command Command
		{
		get
		{
			return command;
		}
	}
	
		public EditorToolStripButton(Command command):base(command.Description, command.Pictogram, null, command.Name)
		{
			this.ImageScaling = ToolStripItemImageScaling.SizeToFit;
			this.command = command;
		}

		protected override void OnClick( System.EventArgs e )
		{
			command.Execute();
			base.OnClick(e);
		}
	}
}