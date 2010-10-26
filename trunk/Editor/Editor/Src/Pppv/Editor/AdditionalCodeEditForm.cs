namespace Pppv.Editor
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	using Pppv.Net;

	public class AdditionalCodeEditForm : Form
	{
		private PetriNet net;
		private TextBox tb;
		private Button bOK, bCancel;

		public AdditionalCodeEditForm(PetriNet net)
		{
			this.net = net;
			this.Size = new Size(505, 335);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Дополнительный код сети: " + net.Id;
			this.InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.tb = new TextBox();
			this.tb.Multiline = true;
			this.tb.AcceptsReturn = true;
			this.tb.AcceptsTab = true;
			this.tb.Size = new Size(480, 250);
			this.tb.Location = new Point(10, 10);
			this.tb.Text = this.net.AdditionalCode;
			this.tb.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			
			this.bOK = new Button();
			this.bOK.Name = this.bOK.Text = "OK";
			this.bOK.Location = new Point(300, 270);
			this.bOK.DialogResult = DialogResult.OK;
			this.bOK.Click += this.OKButtonHandler;
			this.bOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			
			this.bCancel = new Button();
			this.bCancel.Name = this.bCancel.Text = "Cancel";
			this.bCancel.Location = new Point(this.bOK.Right + 10, this.bOK.Top);
			this.bCancel.DialogResult = DialogResult.Cancel;
			this.bCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;

			this.AcceptButton = this.bOK;
			this.CancelButton = this.bCancel;

			this.SuspendLayout();
			this.Controls.Add(this.tb);
			this.Controls.Add(this.bOK);
			this.Controls.Add(this.bCancel);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private void OKButtonHandler(object sender, EventArgs e)
		{
			this.net.AdditionalCode = this.tb.Text;
		}
	}
}
