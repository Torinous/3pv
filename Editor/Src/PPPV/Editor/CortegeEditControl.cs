namespace PPPV.Editor
{
	using System;
	using System.Data;
	using System.ComponentModel;
	using System.Collections;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Diagnostics;
	using System.Threading;
	using System.Reflection;
	using System.IO;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using System.Text;
	using System.Globalization;
	
	using PPPV.Utils;
	using PPPV.Net;
	using PPPV.Editor.Commands;
	using PPPV.Editor.Tools;
	
	public class CortegeEditControl : System.Windows.Forms.UserControl{
		GroupBox groupBox;
		RefreshingListBox lbPredicates;
		TextBox tbCurrentPredicate;
		Button bAdd;
		Button bDelete;
		PredicateList listPredicates;

		public PredicateList ListPredicates{
			get{
				return listPredicates;
			}
		}

		/*Методы*/
		public CortegeEditControl(PredicateList listPredicates)
		{
			this.listPredicates = listPredicates;
			this.Size = new System.Drawing.Size( 400, 260 );
			InitializeComponent();
			FetchFromList();
		}

		private void InitializeComponent(){
			groupBox = new GroupBox();
			SetCountText(0);
			groupBox.Location = new Point( 0, 0 );
			groupBox.Size = new System.Drawing.Size( 400, 260 );

			lbPredicates =  new RefreshingListBox();
			lbPredicates.Location = new Point( 10, 20 );
			lbPredicates.Size = new System.Drawing.Size( 160, 230 );
			//lbPredicates.SelectionChanged += SelectionChangedHandler;
			lbPredicates.SelectedIndexChanged += SelectedIndexChangedHandler;

			tbCurrentPredicate = new TextBox();
			tbCurrentPredicate.Location = new Point( 230, 20 );
			tbCurrentPredicate.Size = new System.Drawing.Size( 160, 230 );
			tbCurrentPredicate.Multiline = true;
			tbCurrentPredicate.AcceptsReturn = true;
			tbCurrentPredicate.Enabled = false;
			tbCurrentPredicate.TextChanged += textChangedEventHandler;

			bAdd = new Button();
			bAdd.Location = new Point( 180, 20 );
			bAdd.Size = new System.Drawing.Size( 40, 20 );
			bAdd.Text = "+";
			bAdd.Click += bAdd_Click;

			bDelete = new Button();
			bDelete.Location = new Point( 180, 50 );
			bDelete.Size = new System.Drawing.Size( 40, 20 );
			bDelete.Text = "-";
			bDelete.Click += bDelete_Click;
			bDelete.Enabled = false;
			
			this.SuspendLayout();
			this.groupBox.SuspendLayout();
			this.groupBox.Controls.Add(lbPredicates);
			this.groupBox.Controls.Add(tbCurrentPredicate);
			this.groupBox.Controls.Add(bAdd);
			this.groupBox.Controls.Add(bDelete);
			this.Controls.Add(groupBox);
			this.groupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.PerformLayout();
		}

		private void bAdd_Click(object sender, EventArgs e){
			lbPredicates.Items.Add(new Predicate((lbPredicates.Items.Count+1).ToString(CultureInfo.CurrentCulture)));
			SetCountText(lbPredicates.Items.Count);
			lbPredicates.SelectedIndex = lbPredicates.Items.Count-1;
		}

		private void bDelete_Click(object sender, EventArgs e){
			int index = lbPredicates.SelectedIndex;
			try {
				lbPredicates.Items.RemoveAt(lbPredicates.SelectedIndex);
			}catch(NetException){
			}catch(EditorException){
			}
			SetCountText(lbPredicates.Items.Count);
			if(index <= lbPredicates.Items.Count-1)
				lbPredicates.SelectedIndex = index;
			else
				lbPredicates.SelectedIndex = lbPredicates.Items.Count-1;
		}

		private void FetchFromList()
		{
			lbPredicates.BeginUpdate();
			foreach( Predicate predicate in listPredicates.List){
				lbPredicates.Items.Add(predicate);
			}
			lbPredicates.EndUpdate();
			SetCountText(lbPredicates.Items.Count);
		}

		public void ChangesApproved()
		{
			listPredicates.List.Clear();
			foreach (Predicate predicate in lbPredicates.Items){
				listPredicates.Add(predicate);
			}
		}

		void SelectedIndexChangedHandler(object sender, EventArgs e){
			if(lbPredicates.SelectedIndex == -1){
				tbCurrentPredicate.Text = "";
				tbCurrentPredicate.Enabled = false;
				bDelete.Enabled = false;
			}else{
				tbCurrentPredicate.Text = lbPredicates.SelectedItem.ToString();
				tbCurrentPredicate.Enabled = true;
				bDelete.Enabled = true;
			}
		}

		private void SetCountText(int count){
			groupBox.Text = "Предикаты в кортеже[" + count.ToString(CultureInfo.CurrentCulture)+ "]";
		}
		
		private void textChangedEventHandler(object sender, EventArgs args){
			if(lbPredicates.SelectedIndex != -1){
				int p1 = tbCurrentPredicate.SelectionStart,
					 p2 = tbCurrentPredicate.SelectionLength;
				(lbPredicates.Items[lbPredicates.SelectedIndex] as Predicate).Text = tbCurrentPredicate.Text;
				lbPredicates.RefreshItem(lbPredicates.SelectedIndex);
				tbCurrentPredicate.Focus();
				tbCurrentPredicate.SelectionStart  = p1;
				tbCurrentPredicate.SelectionLength = p2;
			}
		} // end textChangedEventHandler
	} //class
} // namespace
