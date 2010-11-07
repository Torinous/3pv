namespace Pppv.ApplicationFramework.Commands
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	public interface IInterfaceCommand : ICommand
	{
		string Description { get; }

		Keys ShortcutKeys { get; }

		Image Pictogram { get; }

		ToolStripItem ParentItem { get; set; }

		bool CheckEnabled();
	}
}
