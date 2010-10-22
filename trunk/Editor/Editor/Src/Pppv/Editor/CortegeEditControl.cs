/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.10.2010
 * Time: 15:21
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Globalization;
	using System.Windows.Forms;
	
	using Pppv.ApplicationFramework;
	using Pppv.Net;
	
	public partial class CortegeEditControl : UserControl
	{
		private PredicatesList listPredicates;
		
		public CortegeEditControl()
		{
			this.InitializeComponent();
		}
		
		public PredicatesList ListPredicates
		{
			get
			{
				return this.listPredicates;
			}
			
			set
			{
				this.listPredicates = value;
				this.listPredicates = value;
				if (this.listPredicates != null)
				{
					this.FetchFromList();
				}
				else
				{
					this.listBoxPredicates.Text = String.Empty;
					this.textBoxCurrentPredicate.Text = String.Empty;
					this.textBoxCurrentPredicate.Enabled = false;
				}
			}
		}
		
		public void ChangesApproved()
		{
			this.listPredicates.List.Clear();
			foreach (Predicate predicate in this.listBoxPredicates.Items)
			{
				this.listPredicates.Add(predicate);
			}
		}
		
		private void FetchFromList()
		{
			this.listBoxPredicates.BeginUpdate();
			foreach (Predicate predicate in this.listPredicates.List)
			{
				this.listBoxPredicates.Items.Add(predicate);
			}

			this.listBoxPredicates.EndUpdate();
			this.SetCountText(this.listBoxPredicates.Items.Count);
		}
		
		private void SetCountText(int count)
		{
			this.groupBox.Text = "Предикаты в кортеже[" + count.ToString(CultureInfo.CurrentCulture) + "]";
		}
		
		private void ButtonAddClick(object sender, EventArgs e)
		{
			this.listBoxPredicates.Items.Add(new Predicate("X"));
			this.SetCountText(this.listBoxPredicates.Items.Count);
			this.listBoxPredicates.SelectedIndex = this.listBoxPredicates.Items.Count - 1;	
		}
		
		private void ButtonDeleteClick(object sender, EventArgs e)
		{
			int index = this.listBoxPredicates.SelectedIndex;
			try
			{
				this.listBoxPredicates.Items.RemoveAt(this.listBoxPredicates.SelectedIndex);
			}
			catch (PppvException)
			{
			}

			this.SetCountText(this.listBoxPredicates.Items.Count);
			if (index <= this.listBoxPredicates.Items.Count - 1)
			{
				this.listBoxPredicates.SelectedIndex = index;
			}
			else
			{
				this.listBoxPredicates.SelectedIndex = this.listBoxPredicates.Items.Count - 1;
			}
		}
		
		private void ListBoxPredicatesSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listBoxPredicates.SelectedIndex == -1)
			{
				this.textBoxCurrentPredicate.Text = String.Empty;
				this.textBoxCurrentPredicate.Enabled = false;
				this.buttonDelete.Enabled = false;
			}
			else
			{
				this.textBoxCurrentPredicate.Text = this.listBoxPredicates.SelectedItem.ToString();
				this.textBoxCurrentPredicate.Enabled = true;
				this.buttonDelete.Enabled = true;
			}
		}
		
		private void TextBoxCurrentPredicateTextChanged(object sender, EventArgs e)
		{
			if (this.listBoxPredicates.SelectedIndex != -1)
			{
				int p1 = this.textBoxCurrentPredicate.SelectionStart,
				p2 = this.textBoxCurrentPredicate.SelectionLength;
				(this.listBoxPredicates.Items[this.listBoxPredicates.SelectedIndex] as Predicate).Text = this.textBoxCurrentPredicate.Text;
				this.listBoxPredicates.RefreshItem(this.listBoxPredicates.SelectedIndex);
				this.textBoxCurrentPredicate.Focus();
				this.textBoxCurrentPredicate.SelectionStart = p1;
				this.textBoxCurrentPredicate.SelectionLength = p2;
			}
		}
	}
}
