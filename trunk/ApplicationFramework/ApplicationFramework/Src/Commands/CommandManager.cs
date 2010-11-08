/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 6:01
 *
 *
 */

namespace Pppv.ApplicationFramework.Commands
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Windows.Forms;

	public class CommandManager : Component
	{
		private CommandsList commands;
		private Hashtable hashCommandExecutors;

		public CommandManager()
		{
			this.commands = new CommandsList(this);
			this.hashCommandExecutors = new Hashtable();

			Application.Idle += this.OnIdle;

			this.RegisterCommandExecutor("System.Windows.Forms.ToolStripMenuItem", new ToolStripMenuItemCommandExecutor());
			this.RegisterCommandExecutor("System.Windows.Forms.ToolStripButton", new ToolStripButtonCommandExecutor());
			this.RegisterCommandExecutor("System.Windows.Forms.Button", new ButtonCommandExecutor());
		}

		public CommandsList Commands
		{
			get { return this.commands; }
		}

		internal void RegisterCommandExecutor(string strType, CommandExecutor executor)
		{
			this.hashCommandExecutors.Add(strType, executor);
		}

		internal CommandExecutor GetCommandExecutor(object instance)
		{
			return this.hashCommandExecutors[instance.GetType().ToString()] as CommandExecutor;
		}

		private void OnIdle(object sender, System.EventArgs args)
		{
			IDictionaryEnumerator myEnumerator = (IDictionaryEnumerator)this.commands.GetEnumerator();
			while (myEnumerator.MoveNext())
			{
				Command cmd = myEnumerator.Value as Command;
				if (cmd != null)
				{
					cmd.ProcessUpdates();
				}
			}
		}
	}
}
