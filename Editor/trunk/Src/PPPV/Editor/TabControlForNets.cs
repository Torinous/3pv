using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PPPv.Net;

namespace PPPv.Editor{

   public class TabControlForNets : TabControl{
      private Control oldParent;

      /*Конструкторы*/
      public TabControlForNets() {
         this.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)| AnchorStyles.Left)| AnchorStyles.Right)));
         this.Location = new Point(0, 0);
         this. ShowToolTips = true;
         this.SelectedIndex = 0;
         this.Size = new Size(599, 228);
         this.TabIndex = 3;
         this.DrawMode = TabDrawMode.OwnerDrawFixed;
      }

      /*Акцессоры доступа*/
      /*События*/
      public event RemoveTabPageEventHandler RemovingTabPage;
      public event RemoveTabPageEventHandler RemovedTabPage;
      
      protected override void OnDrawItem(DrawItemEventArgs e){
         Graphics dc = e.Graphics;
         Rectangle r = e.Bounds;
         Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));;
         /*Кисти*/
         SolidBrush greenBrush, redBrush;
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(255,0,0,0));
         if(this.SelectedIndex == e.Index){
            greenBrush = new SolidBrush(Color.FromArgb(200,100,240,100));
            redBrush = new SolidBrush(Color.FromArgb(200,240,100,100));
         }else{
            greenBrush = new SolidBrush(Color.FromArgb(75,100,240,100));
            redBrush = new SolidBrush(Color.FromArgb(75,240,100,100));
         }

         r = this.GetTabRect(e.Index);
         r.Offset(2, 2);
         r.Width = 10;
         r.Height = 10;

         GraphicsPath tmpPath = new GraphicsPath();
         tmpPath.AddEllipse(r.X, r.Y, r.Width, r.Height);
         Region fillRegion = new Region(tmpPath);
         if((this.TabPages[e.Index] as TabPageForNet).UnderlyingNetSaved){
            dc.FillRegion(greenBrush, fillRegion);
         }else{
            dc.FillRegion(redBrush, fillRegion);
         }

         dc.DrawEllipse(blackPen, r.X, r.Y, r.Width, r.Height);

         dc.DrawLine(blackPen, r.X+2, r.Y+2, r.X + r.Width-2, r.Y + r.Height-2);
         dc.DrawLine(blackPen, r.X + r.Width-2, r.Y+2, r.X+2, r.Y + r.Height-2);

         string titel = this.TabPages[e.Index].Text;
         Font f = this.Font;
         e.Graphics.DrawString(titel, f, blackBrush, new PointF(r.X + 12, r.Y));
        }

      protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e){
         Point p = e.Location;
         for (int i = 0; i < TabCount; i++){
            Rectangle r = GetTabRect(i);
            r.Offset(2, 2);
            r.Width = 10;
            r.Height = 10;
            if (r.Contains(p)){
               CloseTab(i);
            }
         }
      }

      private void CloseTab(int i){
         if(OnRemovingTabPage(new RemoveTabPageEventArgs(i)))
            TabPages.Remove(TabPages[i]);
      }

      private bool OnRemovingTabPage(RemoveTabPageEventArgs args){
         if (RemovingTabPage != null){
            bool closeIt = OnRemovingTabPage(args);
            return closeIt;
         }
         return true;
      }

      public NetCanvas AddNewTab(PetriNet _net) {
         TabPageForNet tmpTabPage  = new TabPageForNet(_net);
         this.SuspendLayout();
         this.TabPages.Add(tmpTabPage);
         this.SelectTab(tmpTabPage);
         this.ResumeLayout(false);
         this.PerformLayout();
         return tmpTabPage.NetCanvas;
      }
   }
}
