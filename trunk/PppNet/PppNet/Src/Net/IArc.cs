namespace Pppv.Net
{
	using System;
	using System.Collections;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Globalization;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	public interface IArc : INetElement
	{
		ArcType ArcType { get; set; }

		PredicatesList Cortege { get; }

		string TargetId { get; set; }

		string SourceId { get; set; }

		bool Unfinished { get; }

		string ArcTypeName { get; }
		
		List<Point> Points { get; }
		
		void AddPoint(Point point);
		
		void DeletePoint(int index);
	}
}
