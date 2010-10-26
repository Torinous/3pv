/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 21:33
 */

namespace Pppv.Editor.Shapes
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Diagnostics.CodeAnalysis;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using System.Xml.Serialization;

	using Pppv.Editor;
	using Pppv.Net;

	public interface IShape
	{
		event EventHandler<MoveEventArgs> Move;

		event PaintEventHandler Paint;

		event EventHandler Change;

		INetElement BaseElement { get; }
				
		Point Location { get; }

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "етить")]
		int X { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "етить")]
		int Y { get; set; }

		Region HitRegion { get; set; }

		Point Center { get; }

		Size Size { get; set; }
		
		IShape ParentShape { get; set; }
		
		DependentShapesList DependentShapes { get; }
		
		PetriNetGraphical ParentNetGraphical { get; set; }
		
		void MoveBy(Point radiusVector);

		bool Intersect(Point point);

		bool Intersect(Rectangle rectangle);

		bool Intersect(Region region);

		void Draw(PaintEventArgs e);

		void DrawHandler(object sender, PaintEventArgs e);

		Point GetConnectPoint(Point from);

		void UpdateHitRegion();
		
		void AddDependantShape(IShape shape);
		
		void RemoveDependantShape(int index);
	}
}