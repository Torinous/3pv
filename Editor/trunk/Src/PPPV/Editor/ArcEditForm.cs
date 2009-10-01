using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using PPPv.Net;

namespace PPPv.Editor{
   class ArcEditForm : Form{
      private Arc arc;
      private Label lableName;
      private TextBox tbID;
      private GroupBox groupBox;
      private Button bOK, bCancel;
      private CortegeEditControl cortegeEditControl;

      public ArcEditForm(Arc arc){
         this.arc = arc;
         this.Size = new Size(500, 450);
         this.StartPosition = FormStartPosition.CenterScreen;
         //this.FormBorderStyle = FormBorderStyle.FixedDialog;
         this.Text = "Редактирование дуги: " + arc.Name;
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

         tbID = new TextBox();
         tbID.Size = new Size(250,25);
         tbID.Location = new Point(100,40);
         tbID.ReadOnly = true;
         
         tbID.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
         
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
         
         cortegeEditControl = new CortegeEditControl(arc.Cortege);
         cortegeEditControl.Location = new Point( 30, 80);
         
         this.AcceptButton = bOK;
         this.CancelButton = bCancel;
         
         /* Предварительное заполнение данных */
         tbID.Text = arc.ID;
         
         this.SuspendLayout();
         this.groupBox.SuspendLayout();
         this.groupBox.Controls.Add(lableName);
         this.groupBox.Controls.Add(tbID);
         this.groupBox.Controls.Add(cortegeEditControl);
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
         cortegeEditControl.ChangesApproved();
      }
   }
}
