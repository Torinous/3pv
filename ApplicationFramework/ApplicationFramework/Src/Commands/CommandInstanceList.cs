/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 5:57
 *
 *
 */

namespace Pppv.Commands
{
	using System;
	using System.Collections;
	using System.Collections.ObjectModel;
	
	public class CommandInstanceList : Collection<object>
	{
		private Command command;

		internal CommandInstanceList(Command command) : base()
		{
			this.command = command;
		}
		
		public new void Add(object instance)
		{
			base.Add(instance);
			this.command.Manager.GetCommandExecutor(instance).InstanceAdded(instance, this.command);
		}

		public void Add(object[] items)
		{
			foreach (object item in items)
			{
				this.Add(item);
			}
		}

		public new void Remove(object instance)
		{
			base.Remove(instance);
		}
	}
}
