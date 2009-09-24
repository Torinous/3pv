using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor{

   public class TabPageForNet : TabPage {

      private NetCanvas canvasRef;
      private NetToolStrip toolController;
      private PetriNet net;

      public NetCanvas netCanvas {
         get{
            return canvasRef;
         }
         private set{
            canvasRef = value;
         }
      }

      public TabPageForNet(NetToolStrip toolController, PetriNet net):base("SomeName") {
         this.Location = new Point(45, 45);
         this.Padding  = new Padding(3);
         this.Size     = new Size(599, 228);
         this.TabIndex = 0;
         this.UseVisualStyleBackColor = true;
         this.toolController = toolController;
         this.net = net;
         this.ParentChanged += ParentChangedHandler;
      }

      private void ParentChangedHandler(object sender, EventArgs arg){
         InitializeComponent();
      }

      private void InitializeComponent() {
         this.SuspendLayout();
         this.Controls.Add(netCanvas = new NetCanvas(toolController, net));
         this.ResumeLayout(false);
         this.PerformLayout();
      }
   }
}
