/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 29.10.2010
 * Time: 19:03
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	public partial class AdditionalCodeEditForm2 : Form
	{
		public AdditionalCodeEditForm2(PetriNetGraphical net)
		{
			this.InitializeComponent();
			this.additionalCodeControl1.Net = net;
		}
		
		private void OkButtonClick(object sender, EventArgs e)
		{
			this.additionalCodeControl1.ApproveChanges();
		}
	}
}
