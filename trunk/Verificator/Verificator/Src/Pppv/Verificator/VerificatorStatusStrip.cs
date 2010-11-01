/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 0:59
 *
 *
 */

namespace Pppv.Verificator
{
	using System;
	using System.Windows.Forms;

	public class VerificatorStatusStrip : StatusStrip
	{
		private ToolStripStatusLabel toolStripStatusLabel;

		public VerificatorStatusStrip()
		{
			this.toolStripStatusLabel = new ToolStripStatusLabel();
			this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.ToolStripStatusLabel });
		}

		public ToolStripStatusLabel ToolStripStatusLabel
		{
			get { return this.toolStripStatusLabel; }
			private set { this.toolStripStatusLabel = value; }
		}
		
		public void PostStatusMessage(string message)
		{
			this.ToolStripStatusLabel.Text = message;
		}
	}
}
