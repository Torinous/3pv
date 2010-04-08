﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Editor{

  public class CanvasMouseEventArgs : MouseEventArgs
  {
    public CanvasMouseEventArgs(MouseEventArgs _arg):base(_arg.Button,_arg.Clicks,_arg.X,_arg.Y,_arg.Delta)
    {
    }
  }
}
