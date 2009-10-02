using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor{

   public class TabPageForNet : TabPage {

      private NetCanvas canvas;
      private string netName;
      private bool underlyingNetSaved;

      /*Конструкторы*/
      public TabPageForNet(PetriNet net):base() {
         this.Location = new Point(45, 45);
         this.Padding  = new Padding(3);
         this.Size     = new Size(599, 228);
         this.TabIndex = 0;
         this.UseVisualStyleBackColor = true;
         this.netName = net.ID;
         this.underlyingNetSaved = net.Saved;
         this.Text = (netName==""?"~":netName+"   ");
         InitializeComponent(net);
      }

      /*Акцессоры доступа*/
      public NetCanvas NetCanvas {
         get{
            return canvas;
         }
         private set{
            if(canvas != null){
               canvas.LinkedNetSave   -= LinkedNetSaveHandler;
               canvas.LinkedNetChange -= LinkedNetChangeHandler;
            }
            canvas = value;
            if(canvas != null){
               canvas.LinkedNetSave   += LinkedNetSaveHandler;
               canvas.LinkedNetChange += LinkedNetChangeHandler;
            }
         }
      }

      public bool UnderlyingNetSaved{
         get{
            return underlyingNetSaved;
         }
         private set{
            underlyingNetSaved = value;
         }
      }

      protected override void OnParentChanged(EventArgs args){
         if(Parent != null){
         }
         base.OnParentChanged(args);
      }

      private void LinkedNetSaveHandler(object sender, SaveEventArgs args){
         netName = args.netID;
         this.ToolTipText = args.fileName;
         underlyingNetSaved = true;
         this.Text = (netName==""?"~":netName+"   ");
      }

      private void LinkedNetChangeHandler(object sender, EventArgs args){
         if(UnderlyingNetSaved){
            underlyingNetSaved = false;
            this.Text = (netName==""?"~":netName+"   ");
         }
      }

      private void InitializeComponent(PetriNet net) {
         this.SuspendLayout();
         this.Controls.Add(NetCanvas = new NetCanvas(net));
         this.ResumeLayout(false);
         this.PerformLayout();
      }

   }
}
