using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor{

   public class TabControlForNets : TabControl{
      private NetToolStrip toolController;
      private Control oldParent;
      
      public NetToolStrip ToolController {
         get{
            return toolController;
         }
         set{
            toolController = value;
         }
      }

      public TabControlForNets() {
         this.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)| AnchorStyles.Left)| AnchorStyles.Right)));
         this.Location = new Point(0, 0);
         this.Name = "_tabControl";
         this.SelectedIndex = 0;
         this.Size = new Size(599, 228);
         this.TabIndex = 3;
         InitializeComponent();
      }

      private void InitializeComponent() {
      }

      public NetCanvas AddNewTab(PetriNet _net) {
         TabPageForNet tmpTabPage  = new TabPageForNet(_net);
         this.SuspendLayout();
         this.TabPages.Add(tmpTabPage);
         //this.Controls.Add(tmpTabPage);
         return tmpTabPage.NetCanvas;
         this.ResumeLayout(false);
         this.PerformLayout();
      }

      /*Если у нас поменялся Parent подпишемся сами на все нужные собития*/
      protected override void OnParentChanged(EventArgs e) {
         toolController = (this.FindForm() as MainForm).ToolController;
         //Parent.GetMain
         oldParent = Parent;
         base.OnParentChanged(e);
      }
   }
}
