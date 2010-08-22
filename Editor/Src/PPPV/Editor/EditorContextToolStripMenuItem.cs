using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;


using PPPV.Editor.Commands;
using PPPV.Utils;

namespace PPPV.Editor
{
	public class EditorContextToolStripMenuItem : EditorToolStripMenuItem
	{
		public EditorContextToolStripMenuItem():base()
		{
			this.ShortcutKeys = Keys.None;
			this.ShortcutKeyDisplayString = null;
		}
	
		public EditorContextToolStripMenuItem(Command command):base(command)
		{
		}

		public void CheckEnabled()
		{
			ElementCommand elementCommand = AssociatedCommand as ElementCommand;
			if(elementCommand != null)
				Enabled = elementCommand.Element != null;
		}
	}
}

