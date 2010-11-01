/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 19.10.2010
 * Time: 2:04
 *
 *
 */

namespace Pppv.ApplicationFramework
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	
	using Pppv.ApplicationFramework.Commands;

	public partial class CommandButton : UserControl
	{
		private IInterfaceCommand command;
		
		public CommandButton(IInterfaceCommand command)
		{
			this.InitializeComponent();
			this.Command = command;
		}

		public IInterfaceCommand Command
		{
			get { return this.command; }
			private set { this.command = value; }
		}
	}
}
