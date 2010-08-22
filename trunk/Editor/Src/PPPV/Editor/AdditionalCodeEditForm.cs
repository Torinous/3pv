namespace PPPV.Editor{
	
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	using System.ComponentModel;
	
	using PPPV.Net;

   class AdditionalCodeEditForm : Form{

      private PetriNet net;
      private TextBox tb;
      private Button bOK, bCancel;

      public AdditionalCodeEditForm(PetriNet net){
         this.net = net;
         this.Size = new Size(505, 335);
         this.StartPosition = FormStartPosition.CenterScreen;
         //this.FormBorderStyle = FormBorderStyle.FixedDialog;
         this.Text = "Дополнительный код сети: " + net.Id;
         InitializeComponent();
      }

      private void InitializeComponent(){
         tb = new TextBox();
         tb.Multiline = true;
         tb.AcceptsReturn = true;
         tb.AcceptsTab = true;
         tb.Size = new Size(480, 250);
         tb.Location = new Point(10,10);
         tb.Text = net.AdditionalCode;
         tb.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
         
         bOK = new Button();
         bOK.Name = bOK.Text = "OK";
         bOK.Location = new Point(300,270);
         bOK.DialogResult = DialogResult.OK;
         bOK.Click += OKButtonHandler;
         bOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
         
         bCancel = new Button();
         bCancel.Name = bCancel.Text = "Cancel";
         bCancel.Location = new Point(bOK.Right+10 , bOK.Top);
         bCancel.DialogResult = DialogResult.Cancel;
         bCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;

         this.AcceptButton = bOK;
         this.CancelButton = bCancel;

         this.SuspendLayout();
         this.Controls.Add(tb);
         this.Controls.Add(bOK);
         this.Controls.Add(bCancel);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      private void OKButtonHandler(object sender, EventArgs e){
         net.AdditionalCode = tb.Text;
      }
   }
}
