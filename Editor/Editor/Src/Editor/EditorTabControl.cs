/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 15:20
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	using Pppv.Net;

	public partial class EditorTabControl : UserControl
	{
		public EditorTabControl()
		{
			InitializeComponent();
			this.tabControl.SelectedIndexChanged += SelectedIndexChangedRetranslator;
		}
		
		public event EventHandler<EventArgs> AddTabPage;

		public event EventHandler<RemoveTabPageEventArgs> RemoveTabPage;
		
		public event EventHandler<EventArgs> SelectedIndexChanged;
		
		public int SelectedIndex
		{
			get { return this.tabControl.SelectedIndex; }
			set { this.tabControl.SelectedIndex = value; }
		}
		
		public TabControl.TabPageCollection TabPages
		{
			get { return this.tabControl.TabPages; }
		}

		public void CloseTab(int indexOfTab)
		{
			if (this.OnRemoveTabPage(new RemoveTabPageEventArgs(indexOfTab)))
			{
				this.tabControl.TabPages.Remove(this.tabControl.TabPages[indexOfTab]);
			}
		}
		
		public TabPageForNet AddNewTab()
		{
			TabPageForNet tmpTabPage  = new TabPageForNet();
			this.SuspendLayout();
			this.tabControl.TabPages.Add(tmpTabPage);
			this.tabControl.SelectTab(tmpTabPage);
			//this.tabControl.OnSelectedIndexChanged(new EventArgs()); // Делаем за Microsoft их работу
			this.ResumeLayout(false);
			this.PerformLayout();
			this.OnAddTabPage(new EventArgs());
			return tmpTabPage;
		}
		
		public int TabIdForNet(PetriNet net)
		{
			for (int i = 0; i < this.tabControl.TabCount; i++)
			{
				if ((this.tabControl.TabPages[i] as TabPageForNet).Net == net)
				{
					return i;
				}
			}

			return -1;
		}
		
		public int GetIdForTabPage(TabPage tabPage)
		{
			for (int i = 0; i < this.tabControl.TabCount; i++)
			{
				if (this.tabControl.TabPages[i] == tabPage)
				{
					return i;
				}
			}

			return -1;
		}

		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
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
		
		private void SelectedIndexChangedRetranslator(object sender, EventArgs args)
		{
			this.OnSelectedIndexChanged(args);
		}
	}
}
