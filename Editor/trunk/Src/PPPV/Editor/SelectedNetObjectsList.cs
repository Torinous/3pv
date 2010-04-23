using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using PPPV.Editor.Tools;
using PPPV.Net;

namespace PPPV.Editor
{
  public class SelectedNetObjectsList : ArrayList
  {
    //Данные
    public SelectedNetObjectsList(int a):base(a)
    {
    }
    
    public override int Add(object value)
    {
      (value as NetElement).Paint += DrawSelectionMarker;
      return base.Add(value);
    }
    
    public override void AddRange(ICollection c)
    {
      foreach(object obj in c)
        (obj as NetElement).Paint += DrawSelectionMarker;
      base.AddRange(c);
    }
    
    public override void Clear()
    {
      foreach(object obj in this)
        (obj as NetElement).Paint -= DrawSelectionMarker;
      base.Clear();
    }
    
    public void DrawSelectionMarker(object sender, PaintEventArgs e)
    {
      Pen RedPen = new Pen(Color.Red, 1);
      Graphics dc = e.Graphics;
      RectangleF tmp = ((NetElement)sender).HitRegion.GetBounds(dc);
      dc.DrawRectangle(RedPen, new Rectangle((int)tmp.X, (int)tmp.Y, (int)tmp.Width, (int)tmp.Height) );
    }
  }
}
