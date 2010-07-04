using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using PPPV.Net;

namespace PPPV.Editor{
   class PlaceEditForm : Form{

      private Place place;
      private Label lableName;
      private TextBox tbName;
      private GroupBox groupBox;
      private Button bOK, bCancel;
      private TokensEditControl tokensEditControl;

      public PlaceEditForm(Place pl){
         this.place = pl;
         this.Size = new Size(500, 450);
         this.StartPosition = FormStartPosition.CenterScreen;
         //this.FormBorderStyle = FormBorderStyle.FixedDialog;
         this.Text = "Редактирование места: " + pl.Name;
         InitializeComponent();
      }
      private void InitializeComponent(){
         groupBox = new GroupBox();
         groupBox.Location = new System.Drawing.Point(10, 5);
         groupBox.Name = "groupBox";
         groupBox.Size = new System.Drawing.Size( this.Width-25, this.Height-90 );
         groupBox.TabIndex = 0;
         groupBox.TabStop = false;
         groupBox.Text = "Параметры места:";
         groupBox.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
 
         lableName = new Label();
         lableName.Location = new Point(30, 40);
         lableName.Size = new System.Drawing.Size( 35, 20 );
         lableName.Text = "Имя:";

         tbName = new TextBox();
         tbName.Size = new Size(250,25);
         tbName.Location = new Point(100,40);
         
         tbName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
         
         bOK = new Button();
         bOK.Name = bOK.Text = "Принять";
         bOK.Location = new Point(this.Width-200,this.Height-75);
         bOK.DialogResult = DialogResult.OK;
         bOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
         bOK.Click += OKButtonHandler;
         
         bCancel = new Button();
         bCancel.Name = bCancel.Text = "Отмена";
         bCancel.Location = new Point(bOK.Right+10, bOK.Top);
         bCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
         bCancel.DialogResult = DialogResult.Cancel;
         
         tokensEditControl = new TokensEditControl(place.Tokens);
         tokensEditControl.Location = new Point( 30, 80);
         
         this.AcceptButton = bOK;
         this.CancelButton = bCancel;
         
         /* Предварительное заполнение данных */
         tbName.Text = place.Name;
         
         this.SuspendLayout();
         this.groupBox.SuspendLayout();
         this.groupBox.Controls.Add(lableName);
         this.groupBox.Controls.Add(tbName);
         this.groupBox.Controls.Add(tokensEditControl);
         this.Controls.Add(groupBox);
         this.Controls.Add(bOK);
         this.Controls.Add(bCancel);
         this.groupBox.ResumeLayout(false);
         this.ResumeLayout(false);
         this.groupBox.PerformLayout();
         this.PerformLayout();
      }

      private void OKButtonHandler(object sender, EventArgs e){
         /*Загрузим изменённые данные*/
         place.Name = tbName.Text;
         tokensEditControl.ChangesApproved();
      }
   }
}
