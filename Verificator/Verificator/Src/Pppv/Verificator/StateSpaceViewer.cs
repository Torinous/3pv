/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 19.10.2010
 * Time: 2:17
 *
 *
 */

namespace Pppv.Verificator
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	
	using Pppv.ApplicationFramework;
	using Pppv.ApplicationFramework.Commands;
	using Pppv.Graphviz;
	using Pppv.Utils;
	using Pppv.Verificator.Commands;

	public partial class StateSpaceViewer : UserControl
	{
		public StateSpaceViewer()
		{
			this.InitializeComponent();
			VerificatorConfigurationData config = Configuration<VerificatorConfigurationData>.Instance.Data;
			this.graphvizToolPicker.SelectedIndex = (int)config.DefaultPlotter;
			this.graphvizEdgeLengthPicker.Value = config.EdgeLength;
			this.graphvizShapePicker.SelectedIndex = (int)config.DefaultNodeShape;
			this.useMarkingInStateName.Checked = config.UseMarkingInStateLabel;
		}

		private void Button1Click(object sender, EventArgs e)
		{
			PlotStateSpaceImage plotCommand = new PlotStateSpaceImage();
			if (this.graphvizToolPicker.SelectedIndex != -1)
			{
				plotCommand.PlotterType = (Plotter)this.graphvizToolPicker.SelectedIndex;
			}
			
			plotCommand.Execute();
			this.picViewer1.Image = plotCommand.ResultImage;
		}
		
		private void GraphvizToolPickerTextChanged(object sender, EventArgs e)
		{
			Configuration<VerificatorConfigurationData>.Instance.Data.DefaultPlotter = (Plotter)(sender as ComboBox).SelectedIndex;
		}
		
		private void GraphvizEdgeLengthPickerValueChanged(object sender, EventArgs e)
		{
			Configuration<VerificatorConfigurationData>.Instance.Data.EdgeLength = (int)(sender as NumericUpDown).Value;
		}
		
		private void GraphvizShapePickerSelectedValueChanged(object sender, EventArgs e)
		{
			Configuration<VerificatorConfigurationData>.Instance.Data.DefaultNodeShape = (NodeShape)(sender as ComboBox).SelectedIndex;
		}
		
		private void UseMarkingInStateNameCheckedChanged(object sender, EventArgs e)
		{
			Configuration<VerificatorConfigurationData>.Instance.Data.UseMarkingInStateLabel = (sender as CheckBox).Checked;
		}
	}
}
