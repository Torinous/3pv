/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 06.10.2010
 * Time: 17:16
 */

namespace Pppv.ApplicationFramework.Commands
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	public abstract class InterfaceCommand : Command, IInterfaceCommand
	{
		private string description;
		private Keys shortcutKeys;
		private Image pictogram;
		private ToolStripItem parent;

		protected InterfaceCommand()
		{
		}

		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public Keys ShortcutKeys
		{
			get { return this.shortcutKeys; }
			protected set { this.shortcutKeys = value; }
		}

		public Image Pictogram
		{
			get { return this.pictogram; }
			protected set { this.pictogram = value; }
		}

		public ToolStripItem ParentItem
		{
			get { return this.parent; }
			set { this.parent = value; }
		}

		public virtual bool CheckEnabled()
		{
			return false;
		}
	}
}