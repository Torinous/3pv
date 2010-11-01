/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.10.2010
 * Time: 15:52
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	
	using Pppv.Net;
	
	public partial class ArcEditControl : UserControl
	{
		private IArc arc;
			
		public ArcEditControl()
		{
			this.InitializeComponent();
		}
		
		public IArc Arc
		{
			get
			{
				return this.arc;
			}
			
			set
			{
				this.arc = value;
				if (this.arc != null)
				{
					this.FillFieldsFromPlace();
				}
				else
				{
					this.ClearFields();
				}
			}
		}
		
		public void ChangesApproved()
		{
			this.cortegeEditControl.ChangesApproved();
		}
		
		private void FillFieldsFromPlace()
		{
			this.idTextBox.Text = this.Arc.Id;
			this.cortegeEditControl.ListPredicates = this.Arc.Cortege;
		}
		
		private void ClearFields()
		{
			this.idTextBox.Text = String.Empty;
			this.cortegeEditControl.ListPredicates = null;
		}
	}
}
