using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Pppv.Net;

namespace Pppv.Editor
{
  public class TabControlForNets : TabControl
  {
    /*Конструкторы*/
    public TabControlForNets()
    {
      this.Dock = DockStyle.Fill;
      this.ShowToolTips = true;
      this.SelectedIndex = 0;
      this.TabIndex = 3;
    }
    
    /*События*/
    public event RemoveTabPageEventHandler RemovingTabPage;
    //public event RemoveTabPageEventHandler RemovedTabPage;

    public void CloseTab(int indexOfTab)
    {
      if(OnRemovingTabPage(new RemoveTabPageEventArgs(indexOfTab)))
        TabPages.Remove(TabPages[indexOfTab]);
    }

    private bool OnRemovingTabPage(RemoveTabPageEventArgs args)
    {
      if (RemovingTabPage != null)
      {
        bool closeIt = OnRemovingTabPage(args);
        return closeIt;
      }
      return true;
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
      for(int i = 0; i<this.TabCount; i++)
      {
        if((this.TabPages[i] as TabPageForNet).Net == net)
          return i;
      }
      return -1;
    }
  }
}
