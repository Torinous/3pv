/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 06.10.2010
 * Time: 17:47
 */

namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;

	public abstract class NetEditorInterfaceCommand : EditorInterfaceCommand
	{
		private PetriNetGraphical net;

		protected NetEditorInterfaceCommand(PetriNetGraphical net) : base()
		{
			this.Net = net;
		}

		protected NetEditorInterfaceCommand() : base()
		{
		}

		public PetriNetGraphical Net
		{
			get { return this.net; }
			set { this.net = value; }
		}
		
		public override bool CheckEnabled()
		{
			return CheckFormAndActiveNet();
		}
		
		protected void SetUpTargetNet()
		{
			if (this.Net == null)
			{
				MainForm mainForm = MainForm.Instance;
				if (mainForm != null)
				{
					if (mainForm.ActiveNet != null)
					{
						this.Net = mainForm.ActiveNet;
					}
				}
			}
		}
	}
}
