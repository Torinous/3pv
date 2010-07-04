using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Net {

  public class ResizeEventArgs
  {
    public Size oldSize, newSize;

    public ResizeEventArgs(Size old_, Size new_)
    {
      oldSize = old_;
      newSize = new_;
    }
  }
}
