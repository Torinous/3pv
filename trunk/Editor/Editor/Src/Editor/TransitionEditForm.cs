/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 4:40
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	
	using Pppv.Net;

	public partial class TransitionEditForm : Form
	{
		public TransitionEditForm(ITransition transition)
		{
			this.InitializeComponent();
			this.transitionEditControl.Transition = transition;
		}
		
		private void OkButtonClick(object sender, EventArgs e)
		{
			this.transitionEditControl.ApproveChanges();
		}
	}
}
