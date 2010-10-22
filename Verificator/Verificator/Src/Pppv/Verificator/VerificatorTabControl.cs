/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 21:49
 *
 *
 */

namespace Pppv.Verificator
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	
	public partial class VerificatorTabControl : UserControl
	{
		public VerificatorTabControl()
		{
			this.InitializeComponent();
		}
		
		public Image StateSpaceImage
		{
			get { return this.stateSpaceViewer1.picViewer1.Image; }
		}
		
		public void ShowStateSpace(Image image)
		{
			this.stateSpaceViewer1.picViewer1.Image = image;
		}
	}
}
