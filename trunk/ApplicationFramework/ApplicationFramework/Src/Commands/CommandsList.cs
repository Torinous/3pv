/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 6:04
 *
 *
 */
namespace Pppv.Commands
{
	using System;
	using System.Collections;

	public class CommandsList : ICollection, IEnumerable
	{
		private SortedList commands;
		private CommandManager commandManager;
		
		internal CommandsList(CommandManager manager)
		{
			this.commandManager = manager;
			this.commands = new SortedList();
		}

		public object SyncRoot
		{
			get { return this.commands.SyncRoot; }
		}

		public bool IsSynchronized
		{
			get {	return this.commands.IsSynchronized; }
		}

		public int Count
		{
			get {	return this.commands.Count;	}
		}
		
		internal CommandManager Manager
		{
			get { return this.commandManager; }
		}
		
		public Command this[string name]
		{
			get { return this.commands[name] as Command; }
		}
		
		public void CopyTo(System.Array array, int index)
		{
			this.commands.CopyTo(array, index);
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			return this.commands.GetEnumerator();
		}

		public void Add(Command command)
		{
			command.Manager = this.Manager;
			this.commands.Add(command.ToString(), command);
		}

		public void Remove(string name)
		{
			this.commands.Remove(name);
		}

		public bool Contains(string name)
		{
			return this.commands.Contains(name);
		}
	}
}
