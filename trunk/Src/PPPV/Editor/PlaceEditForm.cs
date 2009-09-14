using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using PPPv.Net;

namespace PPPv.Editor{
   class PlaceEditForm : Form{

      private Place place;
      private Label lableName;
      private TextBox tbName;
      private GroupBox groupBox;
      private Button bOK, bCancel;

      public PlaceEditForm(Place pl){
         this.place = pl;
         this.Size = new Size(400,300);
         this.StartPosition = FormStartPosition.CenterScreen;
         //this.FormBorderStyle = FormBorderStyle.FixedDialog;
         this.Text = "Редактирование места: " + pl.Name;
         InitializeComponent();
      }
      private void InitializeComponent(){
      	 groupBox = new GroupBox();
      	 groupBox.Location = new System.Drawing.Point(20, 20);
		 groupBox.Name = "groupBox";
		 groupBox.Size = new System.Drawing.Size( this.Width-40, this.Height-40 );
		 groupBox.TabIndex = 0;
		 groupBox.TabStop = false;
		 groupBox.Text = "Параметры места";
      	 groupBox.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)	| AnchorStyles.Left) | AnchorStyles.Right)));
		 
      	 lableName = new Label();
      	 lableName.Location = new Point(40, 40);
      	 lableName.Text = "Имя:";
      	 
         tbName = new TextBox();
         tbName.Size = new Size(280,25);
         tbName.Location = new Point(150,40);
         
         bOK = new Button();
         bOK.Name = bOK.Text = "Принять";
         bOK.Location = new Point(this.Width-40,this.Height-40);
         bOK.DialogResult = DialogResult.OK;
         bOK.Click += OKButtonHandler;
         bOK.Anchor = AnchorStyles.Bottom;
         
         bCancel = new Button();
         bCancel.Name = bCancel.Text = "Отмена";
         bCancel.Location = new Point(bOK.Right+10, bOK.Top);
         bCancel.DialogResult = DialogResult.Cancel;
         bCancel.Anchor = AnchorStyles.Bottom;
         
         this.AcceptButton = bOK;
         this.CancelButton = bCancel;
         
         tbName.Text = place.Name;
         
         this.SuspendLayout();
         this.Controls.Add(lableName);
         this.Controls.Add(tbName);
         this.Controls.Add(bOK);
         this.Controls.Add(bCancel);
         this.Controls.Add(groupBox);
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      private void OKButtonHandler(object sender, EventArgs e){
         place.Name = tbName.Text;
      }
   }
}
