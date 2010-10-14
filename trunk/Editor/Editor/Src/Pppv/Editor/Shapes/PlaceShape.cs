/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 25.09.2010
 * Time: 2:50
 */

namespace Pppv.Editor.Shapes
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Globalization;
   using System.Windows.Forms;

   using Pppv.Editor;
   using Pppv.Net;
   using Pppv.ApplicationFramework.Utils;

   public class PlaceShape : Shape, IPlace
   {
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Не смертельно")]
      public PlaceShape(IPlace place, PetriNetGraphical parentNet)
      {
         this.BaseElement = place;
         this.ParentNetGraphical = parentNet;
         this.HitRegion = new Region();
         this.Size = new Size(50, 50);
         this.UpdateHitRegion();
      }

      /*public Point Center
      {
         get { return new Point(this.X + ((int)Size.Width / 2), this.Y + ((int)Size.Height / 2)); }
      }*/

      public TokensList Tokens
      {
         get { return (this.BaseElement as IPlace).Tokens; }
      }

      public override void Draw(PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
         SolidBrush grayBrush = new SolidBrush(Color.FromArgb(200, 100, 100, 100));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
         Font font1 = new Font(new FontFamily("Arial"), 16, FontStyle.Regular, GraphicsUnit.Pixel);

         GraphicsPath tmpPath = new GraphicsPath();
         tmpPath.AddEllipse(this.X, this.Y, Size.Width, Size.Height);
         Region fillRegion = new Region(tmpPath);
         dc.FillRegion(grayBrush, fillRegion);
         dc.DrawEllipse(blackPen, this.X, this.Y, Size.Width, Size.Height);
         dc.DrawString(this.Name, font1, blackBrush, this.X + ((int)Size.Width / 2) + 5, this.Y - 5);
         dc.DrawString(this.Tokens.Count.ToString(CultureInfo.CurrentCulture), font1, blackBrush, this.X + ((int)Size.Width / 2) - 10, this.Y + ((int)Size.Height / 2) - 10);
         this.OnPaint(new PaintEventArgs(e.Graphics, e.ClipRectangle));
      }

      public override void UpdateHitRegion()
      {
         using (PreciseTimer pr = new PreciseTimer("Place.UpdateRegion"))
         {
            this.HitRegion.MakeEmpty();
            GraphicsPath tmpPath = new GraphicsPath();
            tmpPath.AddEllipse(this.X, this.Y, Size.Width, Size.Height);
            this.HitRegion.Union(tmpPath);
         }
      }
   }
}