using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;


using PPPV.Editor.Commands;
using PPPV.Utils;

namespace PPPV.Editor
{
  public class EditorContextToolStripMenuItem : EditorToolStripMenuItem
  {
    public EditorContextToolStripMenuItem():base()
    {
      this.ShortcutKeys = Keys.None;
      this.ShortcutKeyDisplayString = null;
    }
    
    public EditorContextToolStripMenuItem(Command c):base(c)
    {
      if(c is ElementCommand)
        Enabled = (c as ElementCommand).Element != null;
    }
  }
}

