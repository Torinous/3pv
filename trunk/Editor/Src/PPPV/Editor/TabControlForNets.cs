namespace Pppv.Editor
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;

   using Pppv.Net;

   public class TabControlForNets : TabControl
   {
      public TabControlForNets()
      {
         this.Dock = DockStyle.Fill;
         this.ShowToolTips = true;
         this.SelectedIndex = 0;
         this.TabIndex = 3;
      }

      public event EventHandler<RemoveTabPageEventArgs> RemoveTabPage;

      public void CloseTab(int indexOfTab)
      {
         if (this.OnRemoveTabPage(new RemoveTabPageEventArgs(indexOfTab)))
         {
            TabPages.Remove(TabPages[indexOfTab]);
         }
      }

      public NetCanvas AddNewTab(PetriNetWrapper net)
      {
         TabPageForNet tmpTabPage  = new TabPageForNet(net);
         this.SuspendLayout();
         this.TabPages.Add(tmpTabPage);
         this.SelectTab(tmpTabPage);
         this.ResumeLayout(false);
         this.PerformLayout();
         return tmpTabPage.NetCanvas;
      }

      public int TabId(PetriNet net)
      {
         for (int i = 0; i < this.TabCount; i++)
         {
            if ((this.TabPages[i] as TabPageForNet).Net == net)
            {
               return i;
            }
         }

         return -1;
      }

      private bool OnRemoveTabPage(RemoveTabPageEventArgs args)
      {
         if (this.RemoveTabPage != null)
         {
            this.OnRemoveTabPage(args);
            return args.BreakEvent;
         }

         return true;
      }
   }
}