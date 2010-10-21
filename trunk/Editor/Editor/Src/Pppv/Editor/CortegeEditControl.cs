namespace Pppv.Editor
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Diagnostics;
   using System.Drawing;
   using System.Globalization;
   using System.IO;
   using System.Reflection;
   using System.Text;
   using System.Threading;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.ApplicationFramework;
   using Pppv.Editor.Commands;
   using Pppv.Editor.Tools;
   using Pppv.Net;

   public class CortegeEditControl : System.Windows.Forms.UserControl
   {
      private GroupBox groupBox;
      private RefreshingListBox listBoxPredicates;
      private TextBox textBoxCurrentPredicate;
      private Button buttonAdd;
      private Button buttonDelete;
      private PredicatesList listPredicates;

      public CortegeEditControl(PredicatesList listPredicates)
      {
         this.listPredicates = listPredicates;
         this.Size = new System.Drawing.Size(400, 260);
         this.InitializeComponent();
         this.FetchFromList();
      }

      public PredicatesList ListPredicates
      {
         get { return this.listPredicates; }
      }

      public void ChangesApproved()
      {
         this.listPredicates.List.Clear();
         foreach (Predicate predicate in this.listBoxPredicates.Items)
         {
            this.listPredicates.Add(predicate);
         }
      }

      private void InitializeComponent()
      {
         this.groupBox = new GroupBox();
         this.SetCountText(0);
         this.groupBox.Location = new Point(0, 0);
         this.groupBox.Size = new System.Drawing.Size(400, 260);

         this.listBoxPredicates = new RefreshingListBox();
         this.listBoxPredicates.Location = new Point(10, 20);
         this.listBoxPredicates.Size = new System.Drawing.Size(160, 230);
         this.listBoxPredicates.SelectedIndexChanged += this.SelectedIndexChangedHandler;

         this.textBoxCurrentPredicate = new TextBox();
         this.textBoxCurrentPredicate.Location = new Point(230, 20);
         this.textBoxCurrentPredicate.Size = new System.Drawing.Size(160, 230);
         this.textBoxCurrentPredicate.Multiline = true;
         this.textBoxCurrentPredicate.AcceptsReturn = true;
         this.textBoxCurrentPredicate.Enabled = false;
         this.textBoxCurrentPredicate.TextChanged += this.TextChangedEventHandler;

         this.buttonAdd = new Button();
         this.buttonAdd.Location = new Point(180, 20);
         this.buttonAdd.Size = new System.Drawing.Size(40, 20);
         this.buttonAdd.Text = "+";
         this.buttonAdd.Click += this.AddClick;

         this.buttonDelete = new Button();
         this.buttonDelete.Location = new Point(180, 50);
         this.buttonDelete.Size = new System.Drawing.Size(40, 20);
         this.buttonDelete.Text = "-";
         this.buttonDelete.Click += this.Delete_Click;
         this.buttonDelete.Enabled = false;

         this.SuspendLayout();
         this.groupBox.SuspendLayout();
         this.groupBox.Controls.Add(this.listBoxPredicates);
         this.groupBox.Controls.Add(this.textBoxCurrentPredicate);
         this.groupBox.Controls.Add(this.buttonAdd);
         this.groupBox.Controls.Add(this.buttonDelete);
         this.Controls.Add(this.groupBox);
         this.groupBox.ResumeLayout(false);
         this.ResumeLayout(false);
         this.groupBox.PerformLayout();
         this.PerformLayout();
      }

      private void AddClick(object sender, EventArgs e)
      {
         this.listBoxPredicates.Items.Add(new Predicate((this.listBoxPredicates.Items.Count + 1).ToString(CultureInfo.CurrentCulture)));
         this.SetCountText(this.listBoxPredicates.Items.Count);
         this.listBoxPredicates.SelectedIndex = this.listBoxPredicates.Items.Count - 1;
      }

      private void Delete_Click(object sender, EventArgs e)
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

      private void SelectedIndexChangedHandler(object sender, EventArgs e)
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

      private void SetCountText(int count)
      {
         this.groupBox.Text = "Предикаты в кортеже[" + count.ToString(CultureInfo.CurrentCulture) + "]";
      }
      
      private void TextChangedEventHandler(object sender, EventArgs args)
      {
         if (this.listBoxPredicates.SelectedIndex != -1)
         {
            int p1 = this.textBoxCurrentPredicate.SelectionStart,
            p2 = this.textBoxCurrentPredicate.SelectionLength;
            (this.listBoxPredicates.Items[this.listBoxPredicates.SelectedIndex] as Predicate).Text = this.textBoxCurrentPredicate.Text;
            this.listBoxPredicates.RefreshItem(this.listBoxPredicates.SelectedIndex);
            this.textBoxCurrentPredicate.Focus();
            this.textBoxCurrentPredicate.SelectionStart  = p1;
            this.textBoxCurrentPredicate.SelectionLength = p2;
         }
      }
   }
}