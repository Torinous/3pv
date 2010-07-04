using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor {
   public class CortegeEditControl : System.Windows.Forms.UserControl{
      private GroupBox groupBox;
      private RefreshingListBox lbPredicates;
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
      public CortegeEditControl(PredicateList listPredicates_){
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
         lbPredicates.Items.Add(new Predicate((lbPredicates.Items.Count+1).ToString()));
         SetCountText(lbPredicates.Items.Count);
         lbPredicates.SelectedIndex = lbPredicates.Items.Count-1;
      }

      private void bDelete_Click(object sender, EventArgs e){
         int index = lbPredicates.SelectedIndex;
         try {
            lbPredicates.Items.RemoveAt(lbPredicates.SelectedIndex);
         }catch{
         }
         SetCountText(lbPredicates.Items.Count);
         if(index <= lbPredicates.Items.Count-1)
            lbPredicates.SelectedIndex = index;
         else
            lbPredicates.SelectedIndex = lbPredicates.Items.Count-1;
      }

      private void FetchFromList(){
         lbPredicates.BeginUpdate();
         foreach( Predicate predicate in listPredicates){
            lbPredicates.Items.Add(predicate);
         }
         lbPredicates.EndUpdate();
         SetCountText(lbPredicates.Items.Count);
      }

      public void ChangesApproved(){
         listPredicates.Clear();
         foreach (Predicate predicate in lbPredicates.Items){
            listPredicates.Add(predicate);
         }
      }

      public void SelectedIndexChangedHandler(object sender, EventArgs e){
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
         groupBox.Text = "Предикаты в кортеже[" + count.ToString()+ "]";
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
