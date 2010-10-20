/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 19.10.2010
 * Time: 2:17
 *
 *
 */
namespace Pppv.Verificator
{
   partial class StateSpaceViewer
   {
      /// <summary>
      /// Designer variable used to keep track of non-visual components.
      /// </summary>
      private System.ComponentModel.IContainer components = null;
      
      /// <summary>
      /// Disposes resources used by the control.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing) {
            if (components != null) {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }
      
      /// <summary>
      /// This method is required for Windows Forms designer support.
      /// Do not change the method contents inside the source code editor. The Forms designer might
      /// not be able to load this method if it was changed manually.
      /// </summary>
      private void InitializeComponent()
      {
      	this.picViewer1 = new Pppv.Verificator.PicViewer();
      	this.button1 = new System.Windows.Forms.Button();
      	this.label1 = new System.Windows.Forms.Label();
      	this.graphvizToolPicker = new System.Windows.Forms.ComboBox();
      	this.label2 = new System.Windows.Forms.Label();
      	this.graphvizShapePicker = new System.Windows.Forms.ComboBox();
      	this.label3 = new System.Windows.Forms.Label();
      	this.graphvizEdgeLengthPicker = new System.Windows.Forms.NumericUpDown();
      	this.useMarkingInStateName = new System.Windows.Forms.CheckBox();
      	((System.ComponentModel.ISupportInitialize)(this.graphvizEdgeLengthPicker)).BeginInit();
      	this.SuspendLayout();
      	// 
      	// picViewer1
      	// 
      	this.picViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
      	      	      	| System.Windows.Forms.AnchorStyles.Left) 
      	      	      	| System.Windows.Forms.AnchorStyles.Right)));
      	this.picViewer1.AutoScroll = true;
      	this.picViewer1.BackColor = System.Drawing.Color.Black;
      	this.picViewer1.Image = null;
      	this.picViewer1.ImageSizeMode = Pppv.Verificator.SizeMode.Scrollable;
      	this.picViewer1.Location = new System.Drawing.Point(0, 0);
      	this.picViewer1.Name = "picViewer1";
      	this.picViewer1.Size = new System.Drawing.Size(366, 433);
      	this.picViewer1.TabIndex = 0;
      	// 
      	// button1
      	// 
      	this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      	this.button1.Location = new System.Drawing.Point(376, 393);
      	this.button1.Name = "button1";
      	this.button1.Size = new System.Drawing.Size(100, 24);
      	this.button1.TabIndex = 1;
      	this.button1.Text = "Регенерация";
      	this.button1.UseVisualStyleBackColor = true;
      	this.button1.Click += new System.EventHandler(this.Button1Click);
      	// 
      	// label1
      	// 
      	this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      	this.label1.Location = new System.Drawing.Point(376, 0);
      	this.label1.Name = "label1";
      	this.label1.Size = new System.Drawing.Size(100, 23);
      	this.label1.TabIndex = 2;
      	this.label1.Text = "Инструмент:";
      	// 
      	// graphvizToolPicker
      	// 
      	this.graphvizToolPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      	this.graphvizToolPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      	this.graphvizToolPicker.FormattingEnabled = true;
      	this.graphvizToolPicker.Items.AddRange(new object[] {
      	      	      	"neato",
      	      	      	"dot",
      	      	      	"twopi",
      	      	      	"fdp",
      	      	      	"circo"});
      	this.graphvizToolPicker.Location = new System.Drawing.Point(376, 26);
      	this.graphvizToolPicker.Name = "graphvizToolPicker";
      	this.graphvizToolPicker.Size = new System.Drawing.Size(100, 21);
      	this.graphvizToolPicker.TabIndex = 3;
      	this.graphvizToolPicker.TextChanged += new System.EventHandler(this.GraphvizToolPickerTextChanged);
      	// 
      	// label2
      	// 
      	this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      	this.label2.Location = new System.Drawing.Point(376, 50);
      	this.label2.Name = "label2";
      	this.label2.Size = new System.Drawing.Size(100, 23);
      	this.label2.TabIndex = 4;
      	this.label2.Text = "Форма:";
      	// 
      	// graphvizShapePicker
      	// 
      	this.graphvizShapePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      	this.graphvizShapePicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      	this.graphvizShapePicker.FormattingEnabled = true;
      	this.graphvizShapePicker.Items.AddRange(new object[] {
      	      	      	"rectangle",
      	      	      	"circle",
      	      	      	"box",
      	      	      	"poligon",
      	      	      	"point",
      	      	      	"triangle",
      	      	      	"dimond",
      	      	      	"octagon",
      	      	      	"hexagon"});
      	this.graphvizShapePicker.Location = new System.Drawing.Point(376, 76);
      	this.graphvizShapePicker.Name = "graphvizShapePicker";
      	this.graphvizShapePicker.Size = new System.Drawing.Size(100, 21);
      	this.graphvizShapePicker.TabIndex = 5;
      	this.graphvizShapePicker.SelectedValueChanged += new System.EventHandler(this.GraphvizShapePickerSelectedValueChanged);
      	// 
      	// label3
      	// 
      	this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      	this.label3.Location = new System.Drawing.Point(376, 100);
      	this.label3.Name = "label3";
      	this.label3.Size = new System.Drawing.Size(100, 23);
      	this.label3.TabIndex = 6;
      	this.label3.Text = "Длина рёбер:";
      	// 
      	// graphvizEdgeLengthPicker
      	// 
      	this.graphvizEdgeLengthPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      	this.graphvizEdgeLengthPicker.Location = new System.Drawing.Point(376, 126);
      	this.graphvizEdgeLengthPicker.Minimum = new decimal(new int[] {
      	      	      	1,
      	      	      	0,
      	      	      	0,
      	      	      	0});
      	this.graphvizEdgeLengthPicker.Name = "graphvizEdgeLengthPicker";
      	this.graphvizEdgeLengthPicker.Size = new System.Drawing.Size(100, 20);
      	this.graphvizEdgeLengthPicker.TabIndex = 7;
      	this.graphvizEdgeLengthPicker.Value = new decimal(new int[] {
      	      	      	1,
      	      	      	0,
      	      	      	0,
      	      	      	0});
      	this.graphvizEdgeLengthPicker.ValueChanged += new System.EventHandler(this.GraphvizEdgeLengthPickerValueChanged);
      	// 
      	// useMarkingInStateName
      	// 
      	this.useMarkingInStateName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      	this.useMarkingInStateName.Location = new System.Drawing.Point(376, 152);
      	this.useMarkingInStateName.Name = "useMarkingInStateName";
      	this.useMarkingInStateName.Size = new System.Drawing.Size(104, 24);
      	this.useMarkingInStateName.TabIndex = 8;
      	this.useMarkingInStateName.Text = "маркировка";
      	this.useMarkingInStateName.UseVisualStyleBackColor = true;
      	this.useMarkingInStateName.CheckedChanged += new System.EventHandler(this.UseMarkingInStateNameCheckedChanged);
      	// 
      	// StateSpaceViewer
      	// 
      	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      	this.Controls.Add(this.useMarkingInStateName);
      	this.Controls.Add(this.graphvizEdgeLengthPicker);
      	this.Controls.Add(this.label3);
      	this.Controls.Add(this.graphvizShapePicker);
      	this.Controls.Add(this.label2);
      	this.Controls.Add(this.graphvizToolPicker);
      	this.Controls.Add(this.label1);
      	this.Controls.Add(this.button1);
      	this.Controls.Add(this.picViewer1);
      	this.Name = "StateSpaceViewer";
      	this.Size = new System.Drawing.Size(486, 436);
      	((System.ComponentModel.ISupportInitialize)(this.graphvizEdgeLengthPicker)).EndInit();
      	this.ResumeLayout(false);
      }
      private System.Windows.Forms.CheckBox useMarkingInStateName;
      private System.Windows.Forms.NumericUpDown graphvizEdgeLengthPicker;
      private System.Windows.Forms.ComboBox graphvizShapePicker;
      private System.Windows.Forms.ComboBox graphvizToolPicker;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button button1;
      public Pppv.Verificator.PicViewer picViewer1;
   }
}
