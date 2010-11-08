/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 6:21
 *
 *
 */

namespace Pppv.ApplicationFramework.Commands
{
	using System;
	using System.Windows.Forms;

	public class ToolStripButtonCommandExecutor : CommandExecutor
	{
		public override void InstanceAdded(object item, Command cmd)
		{
			ToolStripButton button = (ToolStripButton)item;
			button.Click += this.ToolStripButtonClickHandler;
			button.Image = cmd.Pictogram;
			button.Text = cmd.Name;
			button.ToolTipText = cmd.Description;
			base.InstanceAdded(item, cmd);
		}

		public override void Enable(object item, bool state)
		{
			ToolStripButton button = (ToolStripButton)item;
			button.Enabled = state;
		}

		public override void Check(object item, bool state)
		{
			ToolStripButton button = (ToolStripButton)item;
			if (state)
			{
				button.CheckState = CheckState.Checked;
			}
			else
			{
				button.CheckState = CheckState.Unchecked;
			}
		}
		
		private void ToolStripButtonClickHandler(object sender, EventArgs args)
		{
			Command cmd = GetCommandForInstance(sender);
			cmd.Execute();
		}
	}
}
