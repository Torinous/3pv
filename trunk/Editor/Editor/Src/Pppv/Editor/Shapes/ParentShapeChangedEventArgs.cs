/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 25.10.2010
 * Time: 16:55
 *
 */

namespace Pppv.Editor.Shapes
{
	using System;
	
	public class ParentShapeChangedEventArgs : EventArgs
	{
		private IShape oldParentShape, newParentShape;
		
		public ParentShapeChangedEventArgs(IShape oldParentShape, IShape newParentShape)
		{
			this.OldParentShape = oldParentShape;
			this.NewParentShape = newParentShape;
		}
		
		public IShape NewParentShape
		{
			get { return this.newParentShape; }
			private set { this.newParentShape = value; }
		}
		
		public IShape OldParentShape
		{
			get { return this.oldParentShape; }
			private set { this.oldParentShape = value; }
		}
	}
}
