namespace Pppv.Editor
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using Pppv.Net;
	using Pppv.Utils;

	public class TabPageForNet : TabPage
	{
		private NetCanvas canvas;
		private bool netSaved;

		public TabPageForNet() : base()
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.Location = new Point(45, 45);
			this.Padding  = new Padding(3);
			this.Dock = DockStyle.Fill;
			this.TabIndex = 0;
			this.UseVisualStyleBackColor = true;
			this.UpdateFileNameText(false);
			this.InitializeComponent();
			this.AutoScroll = true;
		}

		public NetCanvas NetCanvas
		{
			get
			{
				return this.canvas;
			}

			private set
			{
				if (this.canvas != null)
				{
					this.canvas.LinkedNetSave   -= this.LinkedNetSaveHandler;
					this.canvas.LinkedNetChange -= this.LinkedNetChangeHandler;
					this.canvas.Resize          -= this.CanvasResizeHandler;
				}

				this.canvas = value;
				if (this.canvas != null)
				{
					this.canvas.LinkedNetSave   += this.LinkedNetSaveHandler;
					this.canvas.LinkedNetChange += this.LinkedNetChangeHandler;
					this.canvas.Resize          += this.CanvasResizeHandler;
				}
			}
		}

		public bool NetSaved
		{
			get { return this.netSaved; }
			private set { this.netSaved = value; }
		}

		public PetriNetGraphical Net
		{
			get { return NetCanvas.Net; }
		}

		public void PutNetOnTabPage(PetriNetGraphical net)
		{
			this.NetCanvas.PutNetOnCanvas(net);
			this.UpdateFileNameText(false);
		}
		
		protected override void OnResize(EventArgs e)
		{
			this.AutoScroll = false;
			base.OnResize(e);
			this.AutoScroll = true;
		}

		protected override void OnParentChanged(EventArgs e)
		{
			if (this.Parent != null)
			{
			}

			base.OnParentChanged(e);
		}

		private void LinkedNetSaveHandler(object sender, SaveNetEventArgs args)
		{
			this.ToolTipText = args.FilePath;
			this.NetSaved = true;
			this.UpdateFileNameText(false);
		}

		private void LinkedNetChangeHandler(object sender, EventArgs args)
		{
			this.NetSaved = false;
			this.UpdateFileNameText(true);
		}
		
		private void CanvasResizeHandler(object sender, EventArgs args)
		{
			this.AutoScrollMinSize = NetCanvas.Size;
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			this.Controls.Add(NetCanvas = new NetCanvas());
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private void UpdateFileNameText(bool changedMark)
		{
			if (this.NetCanvas != null)
			{
				if (this.NetCanvas.Net != null)
				{
					if (String.IsNullOrEmpty(this.NetCanvas.Net.FileName))
					{
						this.Text = "~";
					}
					else
					{
						this.Text = this.NetCanvas.Net.FileName + (changedMark ? "*" : "   ");
					}
				}
			}
		}
	}
}