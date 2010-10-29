/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 11:00
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

	public partial class PlaceEditControl : UserControl
	{
		private IPlace place;
		
		public PlaceEditControl()
		{
			this.InitializeComponent();
		}
		
		public IPlace Place
		{
			get
			{
				return this.place;
			}

			set
			{
				this.place = value;
				if (this.place != null)
				{
					this.FillFieldsFromPlace();
				}
				else
				{
					this.ClearFields();
				}
			}
		}
		
		public void ApproveChanges()
		{
			this.Place.Name = this.nameTextBox.Text;
			this.tokensEditControl.ApproveChanges();
		}
		
		private void FillFieldsFromPlace()
		{
			this.idTextBox.Text = this.Place.Id;
			this.nameTextBox.Text = this.Place.Name;
			this.tokensEditControl.ListTokens = this.Place.Tokens;
		}
		
		private void ClearFields()
		{
			this.idTextBox.Text = String.Empty;
			this.nameTextBox.Text = String.Empty;
			this.tokensEditControl.ListTokens = null;
		}

		private bool IsPlaceDataInvalid()
		{
			bool invalid = false;
			if (this.Place != null)
			{
				if (Char.IsUpper(this.nameTextBox.Text[0]))
				{
					this.errorProvider.SetError(this.nameTextBox, "Согласно синтаксису Prolog`а, терм не может начинаться с большой буквы");
					invalid = true;
				}
				else
				{
					this.errorProvider.SetError(this.nameTextBox, String.Empty);
					invalid = false;
				}
			}
			
			return invalid;
		}
		
		private void NameTextBoxValidating(object sender, CancelEventArgs e)
		{
			e.Cancel = this.IsPlaceDataInvalid();
		}
	}
}
