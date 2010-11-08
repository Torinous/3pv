/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 5:43
 *
 *
 */
namespace Pppv.ApplicationFramework.Commands
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	public class Command
	{
		private CommandInstanceList commandInstances;
		private CommandManager manager;
		private string name;
		private string description;
		private Keys shortcutKeys;
		private Image pictogram;
		private bool enabled;
		private bool check;

		public Command(string strTag, EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate)
		{
			this.commandInstances = new CommandInstanceList(this);
			this.name = strTag;
			this.OnUpdate += handlerUpdate;
			this.OnExecute += handlerExecute;
		}
		
		public event EventHandler<EventArgs> OnUpdate;

		public event EventHandler<EventArgs> OnExecute;

		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}		

		public Keys ShortcutKeys
		{
			get { return this.shortcutKeys; }
			set { this.shortcutKeys = value; }
		}
	
		public Image Pictogram
		{
			get { return this.pictogram; }
			set { this.pictogram = value; }
		}
		
		public CommandInstanceList CommandInstances
		{
			get {	return this.commandInstances;	}
		}

		public string Name
		{
			get { return this.name; }
		}

		public bool Enabled
		{
			get
			{
				return this.enabled;
			}

			set
			{
				this.enabled = value;
				foreach (object instance in this.commandInstances)
				{
					this.Manager.GetCommandExecutor(instance).Enable(instance, this.enabled);
				}
			}
		}

		public bool Checked
		{
			get
			{
				return this.check;
			}

			set
			{
				this.check = value;
				foreach (object instance in this.commandInstances)
				{
					this.Manager.GetCommandExecutor(instance).Check(instance, this.check);
				}
			}
		}

		internal CommandManager Manager
		{
			get {	return this.manager; }
			set {	this.manager = value;	}
		}		

		public override string ToString()
		{
			return this.Name;
		}

		public virtual void Execute()
		{
			if (this.OnExecute != null)
			{
				this.OnExecute(this, EventArgs.Empty);
			}
		}
		
		internal void ProcessUpdates()
		{
			if (this.OnUpdate != null)
			{
				this.OnUpdate(this, EventArgs.Empty);
			}
		}
	}
}
