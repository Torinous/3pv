namespace PPPV.Editor
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using PPPV.Net;
   using PPPV.Utils;

   public class TabPageForNet : TabPage
   {
      private NetCanvas canvas;
      private string netName;
      private bool netSaved;

      public TabPageForNet(PetriNetWrapper net):base( )
      {
         this.Location = new Point(45, 45);
         this.Padding  = new Padding(3);
         this.Size     = new Size(599, 228);
         this.TabIndex = 0;
         this.UseVisualStyleBackColor = true;
         this.netName = net.Id;
         this.netSaved = net.NetSaved;
         this.Text = String.IsNullOrEmpty(netName)?"~":netName+"   ";
         this.InitializeComponent(net);
         this.AutoScroll = true;
      }

      public NetCanvas NetCanvas
      {
         get { return canvas; }
         private set
         {
            if(canvas != null)
            {
               canvas.LinkedNetSave   -= LinkedNetSaveHandler;
               canvas.LinkedNetChange -= LinkedNetChangeHandler;
               canvas.Resize          -= CanvasResizeHandler;
            }
            canvas = value;
            if(canvas != null)
            {
               canvas.LinkedNetSave   += LinkedNetSaveHandler;
               canvas.LinkedNetChange += LinkedNetChangeHandler;
               canvas.Resize          += CanvasResizeHandler;
            }
         }
      }

      public bool NetSaved {
         get { return netSaved; }
         private set { netSaved = value; }
      }

      public PetriNetWrapper Net {
         get { return NetCanvas.Net; }
      }

      protected override void OnResize(EventArgs e)
      {
         AutoScroll = false;
         base.OnResize(e);
         AutoScroll = true;
      }
      protected override void OnParentChanged(EventArgs e)
      {
         if(Parent != null)
         {
         }
         base.OnParentChanged(e);
      }

      private void LinkedNetSaveHandler(object sender, SaveEventArgs args)
      {
         netName = args.NetId;
         this.ToolTipText = args.FileName;
         NetSaved = true;
         this.Text = (String.IsNullOrEmpty(netName)?"~":netName+"   ");
      }

      private void LinkedNetChangeHandler(object sender, EventArgs args)
      {
         if(NetSaved)
         {
            NetSaved = false;
            this.Text = (String.IsNullOrEmpty(netName)?"~":netName+"   ");
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
