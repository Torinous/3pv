using System;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor {
   public class TokensEditControl : System.Windows.Forms.UserControl{
      private GroupBox groupBox;
      private ListBox lbTokens;
      private TextBox tbCurrentToken;
      private Button bAdd;
      private Button bDelete;
      private ArrayList listTokens;

      /*Свойства*/
      public ArrayList ListTokens{
         get{
            return listTokens;
         }
         set{
            listTokens = value;
         }
      }

      /*Методы*/
      public TokensEditControl(ArrayList listTokens_){
         listTokens = listTokens_;
         this.Size = new System.Drawing.Size( 400, 260 );
         InitializeComponent();
         FetchFromList();
      }

      private void InitializeComponent(){
         groupBox = new GroupBox();
         SetCountText(0);
         groupBox.Location = new Point( 0, 0 );
         groupBox.Size = new System.Drawing.Size( 400, 260 );

         lbTokens =  new ListBox();
         lbTokens.Location = new Point( 10, 20 );
         lbTokens.Size = new System.Drawing.Size( 160, 230 );
         //lbTokens.SelectionChanged += SelectionChangedHandler;
         lbTokens.SelectedIndexChanged += SelectedIndexChangedHandler;

         tbCurrentToken = new TextBox();
         tbCurrentToken.Location = new Point( 230, 20 );
         tbCurrentToken.Size = new System.Drawing.Size( 160, 230 );
         tbCurrentToken.Multiline = true;
         tbCurrentToken.AcceptsReturn = true;
         tbCurrentToken.TextChanged += textChangedEventHandler;

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
         this.groupBox.Controls.Add(lbTokens);
         this.groupBox.Controls.Add(tbCurrentToken);
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
         lbTokens.Items.Add("("+(lbTokens.Items.Count+1).ToString()+")");
         SetCountText(lbTokens.Items.Count);
         lbTokens.SelectedIndex = lbTokens.Items.Count-1;
         HaveSomeForDeletion();
      }

      private void bDelete_Click(object sender, EventArgs e)
      {
         try
         {
            lbTokens.Items.RemoveAt(lbTokens.SelectedIndex);
         }
         catch
         {
         }
         SetCountText(lbTokens.Items.Count);

         if(HaveSomeForDeletion())
         {
            lbTokens.SelectedIndex = lbTokens.Items.Count-1;
         }
         else
         {
            tbCurrentToken.Clear();
         }
      }

      private bool HaveSomeForDeletion()
      {
         bool have = false;
         if (lbTokens.Items.Count == 0)
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
         lbTokens.BeginUpdate();
         foreach( Token value in listTokens)
         {
            lbTokens.Items.Add(value.Text);
         }
         lbTokens.EndUpdate();
         SetCountText(lbTokens.Items.Count);
      }

      public void ChangesApproved()
      {
         
         listTokens.Clear();
         foreach (string value in lbTokens.Items)
         {
            listTokens.Add(new Token(value));
         }
      }

      public void SelectedIndexChangedHandler(object sender, EventArgs e)
      {
         if(lbTokens.SelectedItem != null)
         {
            tbCurrentToken.Text = lbTokens.SelectedItem.ToString();
         }
      }

      private void SetCountText(int count)
      {
         groupBox.Text = "Метки[" + count.ToString()+ "]";
      }
      
      private void textChangedEventHandler(object sender, EventArgs args)
      {
         lbTokens.Items[lbTokens.SelectedIndex] = tbCurrentToken.Text;
      } // end textChangedEventHandler
   } //class
} // namespace
