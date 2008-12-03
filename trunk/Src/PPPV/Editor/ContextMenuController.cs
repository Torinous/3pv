
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using PPPv.Net;

namespace PPPv.Editor{
   class ContextMenuController{
      private ContextMenuStrip contextMenuStrip;
      private NetCanvas canvas;
      private PetriNet netRef;
      private BaseNetElement contextMenuTarget;

      /*Конструктор*/
      public ContextMenuController(NetCanvas canvas){
         this.canvas = canvas;
         contextMenuStrip = new ContextMenuStrip();
         this.contextMenuStrip = contextMenuStrip;
         contextMenuStrip.Items.Add("Редактировать");
         contextMenuStrip.Items.Add("Удалить");
         contextMenuStrip.Items[0].Click += ContextMenuEditHandler;
         contextMenuStrip.Items[1].Click += ContextMenuDeleteHandler;

      }

      public void Show(Point point, BaseNetElement netElement, PetriNet netRef){
         if(netElement == null){
            contextMenuStrip.Items[0].Enabled = false;
            contextMenuStrip.Items[1].Enabled = false;
         }else{
            contextMenuStrip.Items[0].Enabled = true;
            contextMenuStrip.Items[1].Enabled = true;
         }
         contextMenuTarget = netElement;
         this.netRef = netRef;
         contextMenuStrip.Show(point);
      }

      private void ContextMenuEditHandler(object sender, EventArgs e){
         if(contextMenuTarget is Arc){
            Form f = new VariableEditForm((Arc)contextMenuTarget);
            f.ShowDialog(canvas.FindForm());
            f.Dispose();
         }
         if(contextMenuTarget is Transition){
            Form f = new GuardEditForm((Transition)contextMenuTarget);
            f.ShowDialog(canvas.FindForm());
            f.Dispose();
         }
         if(contextMenuTarget is Place){
         }
         canvas.Invalidate();

      }

      private void ContextMenuDeleteHandler(object sender, EventArgs e){
         if(contextMenuTarget is Arc){
            netRef.Delete((Arc)contextMenuTarget);
         }
         if(contextMenuTarget is Transition){
            netRef.Delete((Transition)contextMenuTarget);
         }
         if(contextMenuTarget is Place){
            netRef.Delete((Place)contextMenuTarget);
         }
         canvas.Invalidate();
      }
   }
}
