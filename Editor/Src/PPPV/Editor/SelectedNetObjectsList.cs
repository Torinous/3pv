using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

using Pppv.Editor.Tools;
using Pppv.Net;

namespace Pppv.Editor
{
  public class SelectedNetObjectsList
  {
  	private List<NetElement> list;
  	
		public List<NetElement> List {
			get { return list; }
		}
    //Данные
    public SelectedNetObjectsList(int size)
    {
    	list = new List<NetElement>(size);
    }
    
    public void Add(NetElement value)
    {
      (value as NetElement).Paint += DrawSelectionMarker;
      list.Add(value);
    }
    
    /*public void AddRange(IEnumarable c)
    {
      foreach(object obj in c)
        (obj as NetElement).Paint += DrawSelectionMarker;
      list.AddRange(c);
    }*/
    
    public void Clear()
    {
      foreach(object obj in list)
        (obj as NetElement).Paint -= DrawSelectionMarker;
      list.Clear();
    }
    
    private void DrawSelectionMarker(object sender, PaintEventArgs e)
    {
      /*Pen RedPen = new Pen(Color.Red, 1);
      Graphics dc = e.Graphics;
      RectangleF tmp = ((NetElement)sender).HitRegion.GetBounds(dc);
      dc.DrawRectangle(RedPen, new Rectangle((int)tmp.X, (int)tmp.Y, (int)tmp.Width, (int)tmp.Height) );*/
      Graphics dc = e.Graphics;
      dc.SmoothingMode = SmoothingMode.HighQuality;
      SolidBrush b = new SolidBrush(Color.FromArgb(125, 245, 0, 0));
      dc.FillRegion(b,((NetElement)sender).HitRegion);
    }
  }
}
