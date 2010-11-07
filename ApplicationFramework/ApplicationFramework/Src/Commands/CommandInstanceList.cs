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
	
	public class CommandInstanceList : CollectionBase
	{
		private Command command;

		internal CommandInstanceList(Command command) : base()
		{
			this.command = command;
		}

		public object this[int index]
		{
			get
			{
				return this.List[index];
			}
		}
		
		public void Add(object instance)
		{
			this.List.Add(instance);
		}

		public void Add(object[] items)
		{
			foreach (object item in items)
			{
				this.Add(item);
			}
		}

		public void Remove(object instance)
		{
			this.List.Remove(instance);
		}

		protected override void OnInsertComplete(int index, object value)
		{
			this.command.Manager.GetCommandExecutor(value).InstanceAdded(value, this.command);
		}
	}
}
