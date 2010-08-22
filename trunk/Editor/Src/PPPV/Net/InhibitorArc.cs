/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.08.2010
 * Time: 1:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */


namespace PPPV.Net
{
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	
	public class InhibitorArc : Arc
	{
		public InhibitorArc(NetElement startElement):base(startElement)
		{
		}

		protected override Pen PenFactory()
		{
			Pen p = new Pen(Color.Black,1);
			GraphicsPath hPath = new GraphicsPath();
			hPath.AddEllipse(-4, -8, 8, 8);
			GraphicsPath hPath2 = new GraphicsPath();
			hPath2.AddLine(new Point(0, -8), new Point(0, 0));
			CustomLineCap ArrowCap = new CustomLineCap(null, hPath);
			//ArrowCap.SetStrokeCaps(LineCap.Round, LineCap.Round);
			p.CustomEndCap = ArrowCap;
			p.CustomStartCap = ArrowCap;
			return p;
		}
	}
}
