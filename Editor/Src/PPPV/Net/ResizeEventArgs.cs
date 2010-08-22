using System;
using System.Drawing;
using System.Windows.Forms;

namespace PPPV.Net {

  public class ResizeEventArgs
  {
    public Size oldSize, newSize;

    public ResizeEventArgs(Size oldSize, Size newSize)
    {
      this.oldSize = oldSize;
      this.newSize = newSize;
    }
  }
}
