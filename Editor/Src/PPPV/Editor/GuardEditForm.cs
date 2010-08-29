namespace Pppv.Editor
{
   using System;
   using System.ComponentModel;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public class GuardEditForm : Form
   {
      private Transition transition;
      private TextBox textBoxGuard;
      private Button bOK, bCancel;

      public GuardEditForm(Transition transition)
      {
         this.transition = transition;
         this.Size = new Size(300, 110);
         this.StartPosition = FormStartPosition.CenterScreen;
         this.FormBorderStyle = FormBorderStyle.FixedDialog;
         this.Text = "Функция охраны перехода:";
         this.InitializeComponent();
      }

      private void InitializeComponent()
      {
         this.textBoxGuard = new TextBox();
         this.textBoxGuard.Size = new Size(280, 25);
         this.bOK = new Button();
         this.bCancel = new Button();
         this.bOK.Name = this.bOK.Text = "OK";
         this.bCancel.Name = this.bCancel.Text = "Cancel";
         this.bOK.Location = new Point(115, 50);
         this.bCancel.Location = new Point(this.bOK.Right + 10, this.bOK.Top);
         this.bOK.DialogResult = DialogResult.OK;
         this.bCancel.DialogResult = DialogResult.Cancel;

         this.AcceptButton = this.bOK;
         this.CancelButton = this.bCancel;
         this.bOK.Click += this.OKButtonHandler;
         this.textBoxGuard.Location = new Point(10, 10);
         this.textBoxGuard.Text = this.transition.GuardFunction;
         this.SuspendLayout();
         this.Controls.Add(this.textBoxGuard);
         this.Controls.Add(this.bOK);
         this.Controls.Add(this.bCancel);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      private void OKButtonHandler(object sender, EventArgs e)
      {
         this.transition.GuardFunction = this.textBoxGuard.Text;
      }
   }
}