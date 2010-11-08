/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 08.11.2010
 * Time: 20:39
 *
 *
 */

namespace Pppv.Commands
{
	using System;
	using System.Windows.Forms;

	public class ButtonCommandExecutor : CommandExecutor
	{
		public override void InstanceAdded(object item, Command cmd)
		{
			Button button = (Button)item;
			button.Click += this.ButtonClickHandler;
			button.Text = cmd.Name;
			base.InstanceAdded(item, cmd);
		}

		public override void Enable(object item, bool state)
		{
			Button button = (Button)item;
			button.Enabled = state;
		}

		public override void Check(object item, bool state)
		{
		}
		
		private void ButtonClickHandler(object sender, EventArgs args)
		{
			Command cmd = GetCommandForInstance(sender);
			cmd.Execute();
		}
	}
}
