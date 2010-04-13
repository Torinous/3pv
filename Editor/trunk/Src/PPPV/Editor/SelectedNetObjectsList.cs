using System.Collections;

using PPPV.Editor.Tools;
using PPPV.Net;

namespace PPPV.Editor
{
  public class SelectedNetObjectsList : ArrayList
  {
    //Данные
    PointerTool t;
    public SelectedNetObjectsList(int a, PointerTool t_):base(a)
    {
      t = t_;
    }
    public override int Add(object value)
    {
      (value as NetElement).Paint += t.DrawSelectionMarker;
      return base.Add(value);
    }
    public override void AddRange(ICollection c)
    {
      foreach(object obj in c)
        (obj as NetElement).Paint += t.DrawSelectionMarker;
      base.AddRange(c);
    }
    public override void Clear()
    {
      foreach(object obj in this)
        (obj as NetElement).Paint -= t.DrawSelectionMarker;
      base.Clear();
    }
  }
}
