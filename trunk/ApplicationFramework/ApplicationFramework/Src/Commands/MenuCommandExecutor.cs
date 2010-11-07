/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 6:19
 *
 *
 */
namespace Pppv.Commands
{
	using System;
	using System.Windows.Forms;

	public class ToolStripMenuItemCommandExecutor : CommandExecutor
	{
		public override void InstanceAdded(object item, Command cmd)
		{
			ToolStripMenuItem mi = (ToolStripMenuItem)item;
			mi.Click += this.ToolStripMenuItemClickHandler;
			mi.Image = cmd.Pictogram;
			mi.Text = cmd.Name;
			mi.ToolTipText = cmd.Description;
			mi.ShortcutKeys = cmd.ShortcutKeys;
			base.InstanceAdded(item, cmd);
		}

		public override void Enable(object item, bool state)
		{
			ToolStripMenuItem mi = (ToolStripMenuItem)item;
			mi.Enabled = state;
		}

		public override void Check(object item, bool state)
		{
			ToolStripMenuItem mi = (ToolStripMenuItem)item;
			mi.Checked = state;
		}

		private void ToolStripMenuItemClickHandler(object sender, EventArgs e)
		{
			Command cmd = GetCommandForInstance(sender);
			cmd.Execute();
		}
	}
}
