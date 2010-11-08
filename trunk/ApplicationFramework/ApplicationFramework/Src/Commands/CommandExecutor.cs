/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 6:14
 *
 *
 */

namespace Pppv.ApplicationFramework.Commands
{
	using System;
	using System.Collections;

	public abstract class CommandExecutor
	{
		private Hashtable hashInstances = new Hashtable();

		protected Hashtable HashInstances
		{
			get { return this.hashInstances; }
		}		

		public virtual void InstanceAdded(object item, Command cmd)
		{
			this.hashInstances.Add(item, cmd);
		}
		
		public abstract void Enable(object item, bool state);

		public abstract void Check(object item, bool state);
		
		protected Command GetCommandForInstance(object item)
		{
			return this.hashInstances[item] as Command;
		}
	}
}
