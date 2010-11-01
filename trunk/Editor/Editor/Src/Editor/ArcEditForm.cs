/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.10.2010
 * Time: 15:44
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	
	using Pppv.Net;

	public partial class ArcEditForm : Form
	{
		public ArcEditForm(IArc arc)
		{
			this.InitializeComponent();
			this.arcEditControl.Arc = arc;
		}
		
		private void OkButtonClick(object sender, EventArgs e)
		{
			this.arcEditControl.ChangesApproved();
		}
	}
}
