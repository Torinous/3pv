namespace Pppv.Editor
{
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;

	using Pppv.Net;

	public class TabControlForNets : TabControl
	{
		public TabControlForNets()
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.Dock = DockStyle.Fill;
			this.ShowToolTips = true;
			this.SelectedIndex = 0;
			this.TabIndex = 3;
		}

		public event EventHandler<EventArgs> AddTabPage;

		public event EventHandler<RemoveTabPageEventArgs> RemoveTabPage;

		public void CloseTab(int indexOfTab)
		{
			if (this.OnRemoveTabPage(new RemoveTabPageEventArgs(indexOfTab)))
			{
				TabPages.Remove(TabPages[indexOfTab]);
			}
		}

		public TabPageForNet AddNewTab()
		{
			TabPageForNet tmpTabPage  = new TabPageForNet();
			this.SuspendLayout();
			this.TabPages.Add(tmpTabPage);
			this.SelectTab(tmpTabPage);
			this.OnSelectedIndexChanged(new EventArgs()); // Делаем за Microsoft их работу
			this.ResumeLayout(false);
			this.PerformLayout();
			this.OnAddTabPage(new EventArgs());
			return tmpTabPage;
		}

		public int TabIdForNet(PetriNet net)
		{
			for (int i = 0; i < this.TabCount; i++)
			{
				if ((this.TabPages[i] as TabPageForNet).Net == net)
				{
					return i;
				}
			}

			return -1;
		}

		public int GetIdForTabPage(TabPage tabPage)
		{
			for (int i = 0; i < this.TabCount; i++)
			{
				if (this.TabPages[i] == tabPage)
				{
					return i;
				}
			}

			return -1;
		}

		protected virtual void OnAddTabPage(EventArgs e)
		{
			if (this.AddTabPage != null)
			{
				this.AddTabPage(this, e);
			}
		}

		protected virtual bool OnRemoveTabPage(RemoveTabPageEventArgs args)
		{
			if (this.RemoveTabPage != null)
			{
				this.OnRemoveTabPage(args);
				return args.BreakEvent;
			}

			return true;
		}
	}
}