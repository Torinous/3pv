using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor{

   public class TabPageForNet : TabPage {

      private NetCanvas canvasRef;
      private NetToolStrip toolController;
      private PetriNet net;

      /*Акцессоры доступа*/
      public NetCanvas NetCanvas {
         get{
            return canvasRef;
         }
         private set{
            canvasRef = value;
         }
      }
      public NetToolStrip ToolController{
         get {
            return toolController;
         }
         private set {
            toolController = value;
         }
      }

      public TabPageForNet(PetriNet net):base("SomeName") {
         this.Location = new Point(45, 45);
         this.Padding  = new Padding(3);
         this.Size     = new Size(599, 228);
         this.TabIndex = 0;
         this.UseVisualStyleBackColor = true;
         this.net = net;
      }

      protected override void OnParentChanged(EventArgs e){
         this.toolController = (Parent as TabControlForNets).ToolController;
         InitializeComponent(); 
         base.OnParentChanged(e);
      }

      private void InitializeComponent() {
         this.SuspendLayout();
         this.Controls.Add(NetCanvas = new NetCanvas(net));
         this.ResumeLayout(false);
         this.PerformLayout();
      }
   }
}
