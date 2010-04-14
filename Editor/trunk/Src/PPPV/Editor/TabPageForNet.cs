using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor
{
  public class TabPageForNet : TabPage
  {
    private NetCanvas canvas;
    private string netName;
    private bool netSaved;

    /*Конструктор*/
    public TabPageForNet(PetriNet net):base()
    {
      this.SetStyle( ControlStyles.AllPaintingInWmPaint |  ControlStyles.UserPaint |  ControlStyles.DoubleBuffer, true);
      this.Location = new Point(45, 45);
      this.Padding  = new Padding(3);
      this.Size     = new Size(599, 228);
      this.TabIndex = 0;
      this.UseVisualStyleBackColor = true;
      this.netName = net.ID;
      this.netSaved = net.Saved;
      this.Text = (netName==""?"~":netName+"   ");
      InitializeComponent(net);
      this.AutoScroll = true;
    }

    /*Акцессоры доступа*/
    public NetCanvas NetCanvas
    {
      get
      {
        return canvas;
      }
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

    public bool NetSaved
    {
      get
      {
        return netSaved;
      }
      private set
      {
        netSaved = value;
      }
    }
  
    public PetriNet Net
    {
      get
      {
        return NetCanvas.Net;
      }
  
    }
  
    protected override void OnResize(EventArgs args)
    {
      AutoScroll = false;
      base.OnResize(args);
      AutoScroll = true;
    }
    protected override void OnParentChanged(EventArgs args)
    {
      if(Parent != null)
      {
      }
      base.OnParentChanged(args);
    }

    private void LinkedNetSaveHandler(object sender, SaveEventArgs args)
    {
      netName = args.netID;
      this.ToolTipText = args.fileName;
      NetSaved = true;
      this.Text = (netName==""?"~":netName+"   ");
    }

    private void LinkedNetChangeHandler(object sender, EventArgs args)
    {
      if(NetSaved)
      {
        NetSaved = false;
        this.Text = (netName==""?"~":netName+"   ");
      }
    }
    
    private void CanvasResizeHandler(object sender, EventArgs args)
    {
      this.AutoScrollMinSize = NetCanvas.Size;
    }
    

    private void InitializeComponent(PetriNet net)
    {
      this.SuspendLayout();
      this.Controls.Add(NetCanvas = new NetCanvas(net));
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
