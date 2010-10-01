/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 21:33
 */

namespace Pppv.Editor.Shapes
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;
   using System.Xml.Serialization;

   using Pppv.Editor;
   using Pppv.Net;
   using Pppv.Utils;

   public interface IShape
   {
      event EventHandler<MoveEventArgs> Move;

      event PaintEventHandler Paint;

      event EventHandler Change;

      Point Location { get; }

      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "етить")]
      int X { get; set; }

      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "етить")]
      int Y { get; set; }

      Region HitRegion { get; set; }

      Point Center { get; }

      Size Size { get; set; }

      PetriNetGraphical ParentNet { get; set; }

      NetElement BaseElement { get; }

      void MoveBy(Point radiusVector);

      bool Intersect(Point point);

      bool Intersect(Rectangle rectangle);

      bool Intersect(Region region);

      void Draw(PaintEventArgs e);

      Point GetConnectPoint(Point from);

      void UpdateHitRegion();

      Point GetConnectPoint(Point from, NetCanvas onCanvas);

      void ParentNetDrawHandler(object sender, PaintEventArgs e);
   }
}