namespace Pppv.Editor
{
   using System;
   using System.ComponentModel;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public class PlaceEditForm : Form
   {
      private Place place;
      private Label lableName;
      private TextBox textBoxName;
      private GroupBox groupBox;
      private Button bOK, bCancel;
      private TokensEditControl tokensEditControl;

      public PlaceEditForm(Place pl)
      {
         this.place = pl;
         this.Size = new Size(500, 450);
         this.StartPosition = FormStartPosition.CenterScreen;
         this.Text = "Редактирование места: " + pl.Name;
         this.InitializeComponent();
      }

      private void InitializeComponent()
      {
         this.groupBox = new GroupBox();
         this.groupBox.Location = new System.Drawing.Point(10, 5);
         this.groupBox.Name = "groupBox";
         this.groupBox.Size = new System.Drawing.Size(this.Width - 25, this.Height - 90);
         this.groupBox.TabIndex = 0;
         this.groupBox.TabStop = false;
         this.groupBox.Text = "Параметры места:";
         this.groupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

         this.lableName = new Label();
         this.lableName.Location = new Point(30, 40);
         this.lableName.Size = new System.Drawing.Size(35, 20);
         this.lableName.Text = "Имя:";

         this.textBoxName = new TextBox();
         this.textBoxName.Size = new Size(250, 25);
         this.textBoxName.Location = new Point(100, 40);

         this.textBoxName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;

         this.bOK = new Button();
         this.bOK.Name = this.bOK.Text = "Принять";
         this.bOK.Location = new Point(this.Width - 200, this.Height - 75);
         this.bOK.DialogResult = DialogResult.OK;
         this.bOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
         this.bOK.Click += this.OKButtonHandler;

         this.bCancel = new Button();
         this.bCancel.Name = this.bCancel.Text = "Отмена";
         this.bCancel.Location = new Point(this.bOK.Right + 10, this.bOK.Top);
         this.bCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
         this.bCancel.DialogResult = DialogResult.Cancel;

         this.tokensEditControl = new TokensEditControl(this.place.Tokens);
         this.tokensEditControl.Location = new Point(30, 80);

         this.AcceptButton = this.bOK;
         this.CancelButton = this.bCancel;

         this.textBoxName.Text = this.place.Name;

         this.SuspendLayout();
         this.groupBox.SuspendLayout();
         this.groupBox.Controls.Add(this.lableName);
         this.groupBox.Controls.Add(this.textBoxName);
         this.groupBox.Controls.Add(this.tokensEditControl);
         this.Controls.Add(this.groupBox);
         this.Controls.Add(this.bOK);
         this.Controls.Add(this.bCancel);
         this.groupBox.ResumeLayout(false);
         this.ResumeLayout(false);
         this.groupBox.PerformLayout();
         this.PerformLayout();
      }

      private void OKButtonHandler(object sender, EventArgs e)
      {
         this.place.Name = this.textBoxName.Text;
         this.tokensEditControl.ChangesApproved();
      }
   }
}