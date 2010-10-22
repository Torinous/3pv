/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 11:25
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	
	using Pppv.Net;

	public partial class PlaceEditForm : Form
	{
		public PlaceEditForm(IPlace place)
		{
			this.InitializeComponent();
			this.placeEditControl1.Place = place;
		}
		
		private void OKbuttonClick(object sender, EventArgs e)
		{
			this.placeEditControl1.ChangesApproved();
		}
	}
}
