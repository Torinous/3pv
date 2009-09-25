using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor{

   public class TabControlForNets : TabControl{
      private Control oldParent;

      public TabControlForNets() {
         this.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)| AnchorStyles.Left)| AnchorStyles.Right)));
         this.Location = new Point(0, 0);
         this. ShowToolTips = true;
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
         this.SelectTab(tmpTabPage);
         //this.Controls.Add(tmpTabPage);
         this.ResumeLayout(false);
         this.PerformLayout();
         return tmpTabPage.NetCanvas;
      }
   }
}
