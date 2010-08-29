namespace Pppv.Editor
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class TabPageForNet : TabPage
   {
      private NetCanvas canvas;
      private string netName;
      private bool netSaved;

      public TabPageForNet(PetriNetWrapper net) : base()
      {
         this.Location = new Point(45, 45);
         this.Padding  = new Padding(3);
         this.Size     = new Size(599, 228);
         this.TabIndex = 0;
         this.UseVisualStyleBackColor = true;
         this.netName = net.Id;
         this.netSaved = net.NetSaved;
         this.Text = String.IsNullOrEmpty(this.netName) ? "~" : this.netName + "   ";
         this.InitializeComponent(net);
         this.AutoScroll = true;
      }

      public NetCanvas NetCanvas
      {
         get
         {
            return this.canvas;
         }

         private set
         {
            if (this.canvas != null)
            {
               this.canvas.LinkedNetSave   -= this.LinkedNetSaveHandler;
               this.canvas.LinkedNetChange -= this.LinkedNetChangeHandler;
               this.canvas.Resize          -= this.CanvasResizeHandler;
            }

            this.canvas = value;
            if (this.canvas != null)
            {
               this.canvas.LinkedNetSave   += this.LinkedNetSaveHandler;
               this.canvas.LinkedNetChange += this.LinkedNetChangeHandler;
               this.canvas.Resize          += this.CanvasResizeHandler;
            }
         }
      }

      public bool NetSaved
      {
         get { return this.netSaved; }
         private set { this.netSaved = value; }
      }

      public PetriNetWrapper Net
      {
         get { return NetCanvas.Net; }
      }

      protected override void OnResize(EventArgs e)
      {
         this.AutoScroll = false;
         base.OnResize(e);
         this.AutoScroll = true;
      }

      protected override void OnParentChanged(EventArgs e)
      {
         if (this.Parent != null)
         {
         }

         base.OnParentChanged(e);
      }

      private void LinkedNetSaveHandler(object sender, SaveNetEventArgs args)
      {
         this.netName = args.NetId;
         this.ToolTipText = args.FilePath;
         this.NetSaved = true;
         this.Text = String.IsNullOrEmpty(this.netName) ? "~" : this.netName + "   ";
      }

      private void LinkedNetChangeHandler(object sender, EventArgs args)
      {
         if (this.NetSaved)
         {
            this.NetSaved = false;
            this.Text = String.IsNullOrEmpty(this.netName) ? "~" : this.netName + "   ";
         }
      }
      
      private void CanvasResizeHandler(object sender, EventArgs args)
      {
         this.AutoScrollMinSize = NetCanvas.Size;
      }

      private void InitializeComponent(PetriNetWrapper net)
      {
         this.SuspendLayout();
         this.Controls.Add(NetCanvas = new NetCanvas(net));
         this.ResumeLayout(false);
         this.PerformLayout();
      }
   }
}