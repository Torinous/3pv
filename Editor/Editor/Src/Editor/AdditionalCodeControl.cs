/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 29.10.2010
 * Time: 18:52
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	
	public partial class AdditionalCodeControl : UserControl
	{
		private PetriNetGraphical net;
			
		public AdditionalCodeControl()
		{
			this.InitializeComponent();
		}

		public PetriNetGraphical Net
		{
			get
			{
				return this.net;
			}
			
			set
			{
				this.net = value;
				this.RenewFields();
			}
		}
		
		public void ApproveChanges()
		{
			this.Net.AdditionalCode = this.codeTextBox.Text;
		}
		
		private void RenewFields()
		{
			if (this.Net != null)
			{
				this.codeTextBox.Text = this.Net.AdditionalCode;
			}
			else
			{
				this.codeTextBox.Text = String.Empty;
			}
		}
	}
}
