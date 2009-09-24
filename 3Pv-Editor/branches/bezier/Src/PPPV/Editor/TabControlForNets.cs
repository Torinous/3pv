using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor{

   public class TabControlForNets : TabControl{
   private NetToolStrip ToolController;


      public TabControlForNets(NetToolStrip ToolController_) {
         this.ToolController = ToolController_;
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
         TabPageForNet tmpTabPage  = new TabPageForNet(ToolController,_net);
         this.SuspendLayout();
         this.TabPages.Add(tmpTabPage);
         //this.Controls.Add(tmpTabPage);
         return tmpTabPage.netCanvas;
         this.ResumeLayout(false);
         this.PerformLayout();
      }
   }
}
