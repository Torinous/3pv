using System;
using System.Drawing;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor{

   public class TabPageForNet : TabPage {

      private NetCanvas canvas;
      private string netName;
      private bool underlyingNetSaved;

      /*Акцессоры доступа*/
      public NetCanvas NetCanvas {
         get{
            return canvas;
         }
         private set{
            if(canvas != null){
               canvas.LinkedNetSave -= LinkedNetSaveHandler;
            }
            canvas = value;
            if(canvas != null){
               canvas.LinkedNetSave += LinkedNetSaveHandler;
            }
         }
      }

      public string SavedMark{
         get{
            if(underlyingNetSaved)
               return "";
            else
               return "*";
         }
      }

      public TabPageForNet(PetriNet net):base() {
         this.Location = new Point(45, 45);
         this.Padding  = new Padding(3);
         this.Size     = new Size(599, 228);
         this.TabIndex = 0;
         this.UseVisualStyleBackColor = true;
         this.netName = net.ID;
         this.underlyingNetSaved = net.Saved;
         CompileShowedName();
         InitializeComponent(net);
      }

      protected override void OnParentChanged(EventArgs args){
         base.OnParentChanged(args);
      }

      private void LinkedNetSaveHandler(object sender, SaveEventArgs args){
         netName = args.netID;
         this.ToolTipText = args.fileName;
         underlyingNetSaved = true;
         CompileShowedName();
      }

      private void LinkedNetChangeHandler(object sender, SaveEventArgs args){
         underlyingNetSaved = false;
         CompileShowedName();
      }

      private string CompileShowedName(){
         return this.Text = (netName==""?"~~~":netName) + this.SavedMark;
      }

      private void InitializeComponent(PetriNet net) {
         this.SuspendLayout();
         this.Controls.Add(NetCanvas = new NetCanvas(net));
         this.ResumeLayout(false);
         this.PerformLayout();
      }
   }
}
