/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 10:36
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Drawing;
	using System.Globalization;
	using System.Windows.Forms;
	
	using Pppv.ApplicationFramework;
	using Pppv.Net;

	public partial class TokensEditControl : UserControl
	{
		private Collection<Token> listTokens;
		
		public TokensEditControl()
		{
			this.InitializeComponent();
		}

		public Collection<Token> ListTokens
		{
			get
			{
				return this.listTokens;
			}
			
			set
			{
				this.listTokens = value;
				if (this.ListTokens != null)
				{
					this.FetchFromList();
				}
				else
				{
					this.listBoxTokens.Text = String.Empty;
					this.textBoxCurrentToken.Text = String.Empty;
					this.textBoxCurrentToken.Enabled = false;
				}
			}
		}
		
		public void ChangesApproved()
		{
			this.listTokens.Clear();
			foreach (Token value in this.listBoxTokens.Items)
			{
				this.listTokens.Add(value);
			}
		}
		
		private void FetchFromList()
		{
			this.listBoxTokens.BeginUpdate();
			foreach (Token value in this.listTokens)
			{
				this.listBoxTokens.Items.Add(value);
			}

			this.listBoxTokens.EndUpdate();
			this.SetCountText(this.listBoxTokens.Items.Count);
		}
		
		private void ListBoxTokensSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listBoxTokens.SelectedIndex == -1)
			{
				this.textBoxCurrentToken.Text = String.Empty;
				this.textBoxCurrentToken.Enabled = false;
				this.buttonDelete.Enabled = false;
			}
			else
			{
				this.textBoxCurrentToken.Text = this.listBoxTokens.SelectedItem.ToString();
				this.textBoxCurrentToken.Enabled = true;
				this.buttonDelete.Enabled = true;
			}
		}
		
		private void ButtonAddClick(object sender, EventArgs e)
		{
			this.listBoxTokens.Items.Add(new Token((this.listBoxTokens.Items.Count + 1).ToString(CultureInfo.CurrentCulture)));
			this.SetCountText(this.listBoxTokens.Items.Count);
			this.listBoxTokens.SelectedIndex = this.listBoxTokens.Items.Count - 1;
		}
		
		private void ButtonDeleteClick(object sender, EventArgs e)
		{
			int index = this.listBoxTokens.SelectedIndex;
			try
			{
				this.listBoxTokens.Items.RemoveAt(this.listBoxTokens.SelectedIndex);
			}
			catch (PppvException)
			{
			}

			this.SetCountText(this.listBoxTokens.Items.Count);

			if (index <= this.listBoxTokens.Items.Count - 1)
			{
				this.listBoxTokens.SelectedIndex = index;
			}
			else
			{
				this.listBoxTokens.SelectedIndex = this.listBoxTokens.Items.Count - 1;
			}
		}
		
		private void SetCountText(int count)
		{
			this.groupBox.Text = "Метки[" + count.ToString(CultureInfo.CurrentCulture) + "]";
		}
		
		private void TextBoxCurrentTokenTextChanged(object sender, EventArgs e)
		{
			if (this.listBoxTokens.SelectedIndex != -1)
			{
				int p1 = this.textBoxCurrentToken.SelectionStart,
				p2 = this.textBoxCurrentToken.SelectionLength;
				(this.listBoxTokens.Items[this.listBoxTokens.SelectedIndex] as Token).Text = this.textBoxCurrentToken.Text;
				this.listBoxTokens.RefreshItem(this.listBoxTokens.SelectedIndex);
				this.textBoxCurrentToken.Focus();
				this.textBoxCurrentToken.SelectionStart  = p1;
				this.textBoxCurrentToken.SelectionLength = p2;
			}
		}
	}
}
