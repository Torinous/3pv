/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 3:36
 *
 *
 */
namespace Pppv.Editor
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Text;
	using System.Windows.Forms;
	
	using Pppv.Net;
	
	public partial class TransitionEditControl : UserControl
	{
		private ITransition transition;

		public TransitionEditControl()
		{
			this.InitializeComponent();
		}

		public ITransition Transition
		{
			get
			{
				return this.transition;
			}
			
			set
			{
				this.transition = value;
				if (this.transition != null)
				{
					this.FillFieldsFromTransition();
				}
				else
				{
					this.ClearFields();
				}
			}
		}

		public void ApproveChanges()
		{
			this.Transition.Name = this.nameTextBox.Text;
			this.Transition.GuardFunction = this.guardFunctionTextBox.Text;
		}		
		
		private void ClearFields()
		{
			this.idTextBox.Text = String.Empty;
			this.nameTextBox.Text = String.Empty;
			this.guardFunctionTextBox.Text = String.Empty;
		}

		private void FillFieldsFromTransition()
		{
			this.idTextBox.Text = this.Transition.Id;
			this.nameTextBox.Text = this.Transition.Name;
			this.guardFunctionTextBox.Text = this.Transition.GuardFunction;
		}
		
		private void AddArcsParametersButtonClick(object sender, EventArgs e)
		{
			List<Predicate> predicates = new List<Predicate>();

			foreach (IArc arc in this.Transition.ParentNet.Arcs)
			{
				if (arc.SourceId == this.Transition.Id || arc.TargetId == this.Transition.Id)
				{
					foreach (Predicate predicate in arc.Cortege.List)
					{
						predicates.Add(predicate);
					}
				}
			}

			StringBuilder arcParams = new StringBuilder();
			arcParams.Append("(");
			for (int i = 0; i < predicates.Count; i++)
			{
				arcParams.Append(predicates[i].Text);
				if (i < predicates.Count - 1)
				{
					arcParams.Append(", ");
				}
			}

			arcParams.Append(")");
			this.nameTextBox.Text = this.nameTextBox.Text + arcParams.ToString();
		}
		
		private bool IsFormInvalid()
		{
			bool isInvalid = false;
			if (this.Transition != null)
			{
				if (Char.IsUpper(this.nameTextBox.Text[0]))
				{
					errorProvider.SetError(this.nameTextBox, "Согласно синтаксису Prolog`а, терм не может начинаться с большой буквы");
					isInvalid = true;
				}
				else
				{
					errorProvider.SetError(this.nameTextBox, "");
					isInvalid = false;
				}
			}
			
			return isInvalid;
		}
		
		void TransitionEditControlValidating(object sender, CancelEventArgs e)
		{
			e.Cancel = this.IsFormInvalid();
		}
	}
}
