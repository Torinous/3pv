using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor {
   public class CortegeEditControl : System.Windows.Forms.UserControl{
      private GroupBox groupBox;
      private ListBox lbPredicates;
      private TextBox tbCurrentPredicate;
      private Button bAdd;
      private Button bDelete;
      private ArrayList listPredicates;

      /*Свойства*/
      public ArrayList ListPredicates{
         get{
            return listPredicates;
         }
         set{
            listPredicates = value;
         }
      }

      /*Методы*/
      public CortegeEditControl(CortegeList listPredicates_){
         listPredicates = listPredicates_;
         this.Size = new System.Drawing.Size( 400, 260 );
         InitializeComponent();
         FetchFromList();
      }

      private void InitializeComponent(){
         groupBox = new GroupBox();
         SetCountText(0);
         groupBox.Location = new Point( 0, 0 );
         groupBox.Size = new System.Drawing.Size( 400, 260 );

         lbPredicates =  new ListBox();
         lbPredicates.Location = new Point( 10, 20 );
         lbPredicates.Size = new System.Drawing.Size( 160, 230 );
         //lbPredicates.SelectionChanged += SelectionChangedHandler;
         lbPredicates.SelectedIndexChanged += SelectedIndexChangedHandler;

         tbCurrentPredicate = new TextBox();
         tbCurrentPredicate.Location = new Point( 230, 20 );
         tbCurrentPredicate.Size = new System.Drawing.Size( 160, 230 );
         tbCurrentPredicate.Multiline = true;
         tbCurrentPredicate.AcceptsReturn = true;
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

      private void bAdd_Click(object sender, EventArgs e)
      {
         lbPredicates.Items.Add((lbPredicates.Items.Count+1).ToString());
         SetCountText(lbPredicates.Items.Count);
         lbPredicates.SelectedIndex = lbPredicates.Items.Count-1;
         HaveSomeForDeletion();
      }

      private void bDelete_Click(object sender, EventArgs e)
      {
         try
         {
            lbPredicates.Items.RemoveAt(lbPredicates.SelectedIndex);
         }
         catch
         {
         }
         SetCountText(lbPredicates.Items.Count);

         if(HaveSomeForDeletion())
         {
            lbPredicates.SelectedIndex = lbPredicates.Items.Count-1;
         }
         else
         {
            tbCurrentPredicate.Clear();
         }
      }

      private bool HaveSomeForDeletion()
      {
         bool have = false;
         if (lbPredicates.Items.Count == 0)
         {
            bDelete.Enabled = false;
            have = false;
         }
         else
         {
            bDelete.Enabled = true;
            have = true;
         }
         return have;
      }

      private void FetchFromList()
      {
         lbPredicates.BeginUpdate();
         foreach( string value in listPredicates)
         {
            lbPredicates.Items.Add(value);
         }
         lbPredicates.EndUpdate();
         SetCountText(lbPredicates.Items.Count);
      }

      public void ChangesApproved()
      {
         
         listPredicates.Clear();
         foreach (string value in lbPredicates.Items)
         {
            listPredicates.Add(value);
         }
      }

      public void SelectedIndexChangedHandler(object sender, EventArgs e)
      {
         if(lbPredicates.SelectedItem != null)
         {
            tbCurrentPredicate.Text = lbPredicates.SelectedItem.ToString();
         }
      }

      private void SetCountText(int count)
      {
         groupBox.Text = "Предикаты в кортеже[" + count.ToString()+ "]";
      }
      
      private void textChangedEventHandler(object sender, EventArgs args)
      {
         lbPredicates.Items[lbPredicates.SelectedIndex] = tbCurrentPredicate.Text;
      } // end textChangedEventHandler
   } //class
} // namespace
