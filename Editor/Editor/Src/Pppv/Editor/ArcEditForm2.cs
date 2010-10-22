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

	public partial class ArcEditForm2 : Form
	{
		public ArcEditForm2(IArc arc)
		{
			InitializeComponent();
			this.arcEditControl.Arc = arc;
		}
		
		
		void OkButtonClick(object sender, EventArgs e)
		{
			this.arcEditControl.ChangesApproved();
		}
	}
}
