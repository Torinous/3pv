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
	using Pppv.Commands;
	using Pppv.Graphviz;
	using Pppv.Utils;
	using Pppv.Verificator.Commands;

	public partial class StateSpaceViewer : UserControl
	{
		private CommandManager commandManager;
		
		public StateSpaceViewer()
		{
			this.InitializeComponent();
			VerificatorConfigurationData config = Configuration<VerificatorConfigurationData>.Instance.Data;
			this.graphvizToolPicker.SelectedIndex = (int)config.DefaultPlotter;
			this.graphvizEdgeLengthPicker.Value = config.EdgeLength;
			this.graphvizShapePicker.SelectedIndex = (int)config.DefaultNodeShape;
			this.useMarkingInStateName.Checked = config.UseMarkingInStateLabel;
			this.InitializeCommandManager();
		}

		public CommandManager CommandManager
		{
			get { return this.commandManager; }
			private set { this.commandManager = value; }
		}
		
		private void InitializeCommandManager()
		{
			this.CommandManager = new CommandManager();
			this.AddCommandsToManager();
			this.AssociateCommandsWithUI();
		}
		
		private void AddCommandsToManager()
		{
			CommandsList commandList = this.CommandManager.Commands;
			commandList.Add(new PlotReachabilityGraphImageCommand(this.PlotStateSpaceCommandHandler, null));
		}
		
		private void AssociateCommandsWithUI()
		{
			CommandsList commandList = this.CommandManager.Commands;
			commandList[PlotReachabilityGraphImageCommand.Id].CommandInstances.Add(this.plotStateSpaceButton);
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
		
		private void PlotStateSpaceCommandHandler(object sender, System.EventArgs e)
		{
			GraphvizPlotter plotter = new GraphvizPlotter();
			plotter.PlotterType = (Plotter)this.graphvizToolPicker.SelectedIndex;
			this.picViewer1.Image =	plotter.Plot(StateSpaceInDotFormatTranslator.Create());
		}
	}
}
