using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;


namespace PPPV.Editor{

  public class NetToolStrip : ToolStrip{
    public ToolStripButton toolStripButtonPointer;
    public ToolStripButton toolStripButtonPlace;
    public ToolStripButton toolStripButtonTransition;
    public ToolStripButton toolStripButtonArc;
    public ToolStripButton toolStripButtonTest;

    public NetToolStrip() {
      //this.Location = new System.Drawing.Point(0, 0);
      this.Name = "_toolStrip";
      //this.Size = new System.Drawing.Size(599, 64);
      this.TabIndex = 2;
      this.Text = "_toolStrip";
      this.AutoSize = true;
      InitializeComponent();
    }

    private void OnlyOneToolController(object sender, EventArgs e){
      if(((ToolStripButton)sender).CheckState == CheckState.Checked){
        if((ToolStripButton)sender != toolStripButtonPointer){
          toolStripButtonPointer.CheckState = CheckState.Unchecked;
        }
        if((ToolStripButton)sender != toolStripButtonPlace){
          toolStripButtonPlace.CheckState = CheckState.Unchecked;
        }
        if((ToolStripButton)sender != toolStripButtonTransition){
          toolStripButtonTransition.CheckState = CheckState.Unchecked;
        }
        if((ToolStripButton)sender != toolStripButtonArc){
          toolStripButtonArc.CheckState = CheckState.Unchecked;
        }
      }
    }

    public ToolEnum CurrentTool(){
      if(toolStripButtonPointer.CheckState == CheckState.Checked){
        return ToolEnum.Pointer;
      }
      else if(toolStripButtonPlace.CheckState == CheckState.Checked){
        return ToolEnum.Place;
      }
      else if(toolStripButtonTransition.CheckState == CheckState.Checked){
        return ToolEnum.Transition;
      }
      else if(toolStripButtonArc.CheckState == CheckState.Checked){
        return ToolEnum.Arc;
      }else{
        return ToolEnum.Pointer;
      }
    }

    private void InitializeComponent() {
      this.toolStripButtonPointer = new ToolStripButton();
      this.toolStripButtonPointer.CheckOnClick = true;
      this.toolStripButtonPointer.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButtonPointer.Image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("pointer.png"), true);
      //this.toolStripButtonPointer.ImageTransparentColor = Color.White;
      this.toolStripButtonPointer.ImageScaling = ToolStripItemImageScaling.SizeToFit;
      this.toolStripButtonPointer.Name = this.toolStripButtonPointer.Text = "toolStripButtonPlace";
      this.toolStripButtonPointer.CheckStateChanged += OnlyOneToolController;
      //this.toolStripButtonPointer.Size = new Size(32, 32);

      this.toolStripButtonPlace = new ToolStripButton();
      this.toolStripButtonPlace.CheckOnClick = true;
      this.toolStripButtonPlace.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButtonPlace.Image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("place.png"), true);
      //this.toolStripButtonPlace.ImageTransparentColor = Color.Magenta;
      this.toolStripButtonPlace.ImageScaling = ToolStripItemImageScaling.SizeToFit;
      this.toolStripButtonPlace.Name = this.toolStripButtonPlace.Text = "toolStripButtonPlace";
      this.toolStripButtonPlace.CheckStateChanged += OnlyOneToolController;
      //this.toolStripButtonPlace.Size = new Size(32, 32);

      this.toolStripButtonTransition = new ToolStripButton();
      this.toolStripButtonTransition.CheckOnClick = true;
      this.toolStripButtonTransition.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButtonTransition.Image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("transition.png"), true);
      //this.toolStripButtonTransition.ImageTransparentColor = Color.Magenta;
      this.toolStripButtonTransition.ImageScaling = ToolStripItemImageScaling.SizeToFit;
      this.toolStripButtonTransition.Name = this.toolStripButtonTransition.Text = "toolStripButtonPlace";
      this.toolStripButtonTransition.CheckStateChanged += OnlyOneToolController;
      //this.toolStripButtonTransition.Size = new Size(32, 32);

      this.toolStripButtonArc = new ToolStripButton();
      this.toolStripButtonArc.CheckOnClick = true;
      this.toolStripButtonArc.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButtonArc.Image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("arc.png"), true);
      //this.toolStripButtonArc.ImageTransparentColor = Color.Magenta;
      this.toolStripButtonArc.ImageScaling = ToolStripItemImageScaling.SizeToFit;
      this.toolStripButtonArc.Name = this.toolStripButtonArc.Text = "toolStripButtonPlace";
      this.toolStripButtonArc.CheckStateChanged += OnlyOneToolController;
      //this.toolStripButtonArc.Size = new Size(32, 32);

      this.toolStripButtonTest = new ToolStripButton();
      this.toolStripButtonTest.CheckOnClick = true;
      this.toolStripButtonTest.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButtonTest.Image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("todo.png"), true);
      //this.toolStripButtonTest.ImageTransparentColor = Color.Magenta;
      this.toolStripButtonTest.ImageScaling = ToolStripItemImageScaling.SizeToFit;
      this.toolStripButtonTest.Name = this.toolStripButtonTest.Text = "toolStripButtonTest";
      this.toolStripButtonTest.CheckStateChanged += OnlyOneToolController;
      this.toolStripButtonTest.Size = new Size(80, 80);

      this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolStripButtonPointer,
                                                                    this.toolStripButtonPlace,
                                                                    this.toolStripButtonTransition,
                                                                    this.toolStripButtonArc,
                                                                    this.toolStripButtonTest});
    }
  }
}
