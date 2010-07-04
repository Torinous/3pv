using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor
{
  public class TabControlForNets : TabControl
  {
    /*Конструкторы*/
    public TabControlForNets()
    {
      this.SetStyle( ControlStyles.AllPaintingInWmPaint |  ControlStyles.UserPaint |  ControlStyles.DoubleBuffer, true);
      this.Dock = DockStyle.Fill;
      this.ShowToolTips = true;
      this.SelectedIndex = 0;
      this.TabIndex = 3;
      //this.DrawMode = TabDrawMode.OwnerDrawFixed;
    }
    
    /*События*/
    public event RemoveTabPageEventHandler RemovingTabPage;
    public event RemoveTabPageEventHandler RemovedTabPage;

    public void CloseTab(int i)
    {
      if(OnRemovingTabPage(new RemoveTabPageEventArgs(i)))
        TabPages.Remove(TabPages[i]);
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

    public NetCanvas AddNewTab(PetriNetWrapper _net)
    {
      TabPageForNet tmpTabPage  = new TabPageForNet(_net);
      this.SuspendLayout();
      this.TabPages.Add(tmpTabPage);
      this.SelectTab(tmpTabPage);
      this.ResumeLayout(false);
      this.PerformLayout();
      return tmpTabPage.NetCanvas;
    }
    
    public int TabID(PetriNet n)
    {
      for(int i=0; i<this.TabCount; i++)
      {
        if((this.TabPages[i] as TabPageForNet).Net == n)
          return i;
      }
      return -1;
    }
  }
}
