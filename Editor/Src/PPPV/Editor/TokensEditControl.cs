namespace Pppv.Editor
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Globalization;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;
   
   public class TokensEditControl : System.Windows.Forms.UserControl
   {
      private GroupBox groupBox;
      private RefreshingListBox listBoxTokens;
      private TextBox textBoxCurrentToken;
      private Button buttonAdd;
      private Button buttonDelete;
      private List<Token> listTokens;

      public TokensEditControl(List<Token> listTokens)
      {
         this.listTokens = listTokens;
         this.Size = new System.Drawing.Size(400, 260);
         this.InitializeComponent();
         this.FetchFromList();
      }

      public List<Token> ListTokens
      {
         get { return this.listTokens; }
      }

      public void ChangesApproved()
      {
         this.listTokens.Clear();
         foreach (Token value in this.listBoxTokens.Items)
         {
            this.listTokens.Add(value);
         }
      }

      private void SelectedIndexChangedHandler(object sender, EventArgs e)
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

      private void InitializeComponent()
      {
         this.groupBox = new GroupBox();
         this.SetCountText(0);
         this.groupBox.Location = new Point(0, 0);
         this.groupBox.Size = new System.Drawing.Size(400, 260);

         this.listBoxTokens = new RefreshingListBox();
         this.listBoxTokens.Location = new Point(10, 20);
         this.listBoxTokens.Size = new System.Drawing.Size(160, 230);
         this.listBoxTokens.SelectedIndexChanged += this.SelectedIndexChangedHandler;

         this.textBoxCurrentToken = new TextBox();
         this.textBoxCurrentToken.Location = new Point(230, 20);
         this.textBoxCurrentToken.Size = new System.Drawing.Size(160, 230);
         this.textBoxCurrentToken.Multiline = true;
         this.textBoxCurrentToken.AcceptsReturn = true;
         this.textBoxCurrentToken.Enabled = false;
         this.textBoxCurrentToken.TextChanged += this.TextChangedEventHandler;

         this.buttonAdd = new Button();
         this.buttonAdd.Location = new Point(180, 20);
         this.buttonAdd.Size = new System.Drawing.Size(40, 20);
         this.buttonAdd.Text = "+";
         this.buttonAdd.Click += this.AddButtonClick;

         this.buttonDelete = new Button();
         this.buttonDelete.Location = new Point(180, 50);
         this.buttonDelete.Size = new System.Drawing.Size(40, 20);
         this.buttonDelete.Text = "-";
         this.buttonDelete.Click += this.DeleteButtonClick;
         this.buttonDelete.Enabled = false;

         this.SuspendLayout();
         this.groupBox.SuspendLayout();
         this.groupBox.Controls.Add(this.listBoxTokens);
         this.groupBox.Controls.Add(this.textBoxCurrentToken);
         this.groupBox.Controls.Add(this.buttonAdd);
         this.groupBox.Controls.Add(this.buttonDelete);
         this.Controls.Add(this.groupBox);
         this.groupBox.ResumeLayout(false);
         this.ResumeLayout(false);
         this.groupBox.PerformLayout();
         this.PerformLayout();
      }

      private void AddButtonClick(object sender, EventArgs e)
      {
         this.listBoxTokens.Items.Add(new Token((this.listBoxTokens.Items.Count + 1).ToString(CultureInfo.CurrentCulture)));
         this.SetCountText(this.listBoxTokens.Items.Count);
         this.listBoxTokens.SelectedIndex = this.listBoxTokens.Items.Count - 1;
      }

      private void DeleteButtonClick(object sender, EventArgs e)
      {
         int index = this.listBoxTokens.SelectedIndex;
         try
         {
            this.listBoxTokens.Items.RemoveAt(this.listBoxTokens.SelectedIndex);
         }
         catch (NetException)
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

      private void SetCountText(int count)
      {
         this.groupBox.Text = "Метки[" + count.ToString(CultureInfo.CurrentCulture) + "]";
      }
      
      private void TextChangedEventHandler(object sender, EventArgs args)
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