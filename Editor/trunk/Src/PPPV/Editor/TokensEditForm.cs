using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using PPPv.Net;

namespace PPPv.Editor{
   class TokensEditForm : Form{

      private Transition tr;
      private TextBox tb;
      private Button bOK,bCancel;

      public TokensEditForm(Transition tr){
         this.tr = tr;
         this.Size = new Size(300,110);
         this.StartPosition = FormStartPosition.CenterScreen;
         this.FormBorderStyle = FormBorderStyle.FixedDialog;
         this.Text = "Редактирование меток позиции:";
         InitializeComponent();
      }
      private void InitializeComponent(){
         tb = new TextBox();
         tb.Size = new Size(280,25);
         bOK = new Button();
         bCancel = new Button();
         bOK.Name = bOK.Text = "OK";
         bCancel.Name = bCancel.Text = "Cancel";
         bOK.Location = new Point(115,50);
         bCancel.Location = new Point(bOK.Right+10,bOK.Top);
         bOK.DialogResult = DialogResult.OK;
         bCancel.DialogResult = DialogResult.Cancel;

         this.AcceptButton = bOK;
         this.CancelButton = bCancel;
         bOK.Click += OKButtonHandler;
         tb.Location = new Point(10,10);
         tb.Text = tr.GuardFunction;
         this.SuspendLayout();
         this.Controls.Add(tb);
         this.Controls.Add(bOK);
         this.Controls.Add(bCancel);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      private void OKButtonHandler(object sender, EventArgs e){
         tr.GuardFunction = tb.Text;
      }
   }
}
