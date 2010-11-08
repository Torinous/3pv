namespace Pppv.Editor
{
	using System;
	using System.Collections;
	using System.Diagnostics;
	using System.Globalization;
	using System.IO;
	using System.Reflection;
	using System.Text;
	using System.Threading;
	using System.Windows.Forms;

	using Microsoft.SqlServer.MessageBox;
	
	using Pppv.Editor.Commands;
	using Pppv.Editor.Tools;
	using Pppv.Net;
	using Pppv.ApplicationFramework.Utils;

	public class EditorApplication : ApplicationContext
	{
		private Form mainFormInst;

		private EditorApplication()
		{
			PrepareLog();
			this.InitializeComponent();
			this.mainFormInst.Show();
		}

		public static string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public Form MainFormInst
		{
			get
			{
				return this.mainFormInst;
			}
		}

		public static void PrepareLog()
		{
			TextWriterTraceListener tr1 = new TextWriterTraceListener(System.Console.Out);
			FileStream logFile = new FileStream(System.Windows.Forms.Application.StartupPath + "\\log", FileMode.Create);
			TextWriterTraceListener logFileListener = new TextWriterTraceListener(logFile);
			
			System.Diagnostics.Debug.Listeners.Add(tr1);
			System.Diagnostics.Debug.Listeners.Add(logFileListener);
			System.Diagnostics.Debug.AutoFlush = true;
		}

		[STAThread]
		private static void Main()
		{
			Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);
			Application.Run(new EditorApplication());
		}

		private void InitializeComponent()
		{
			this.mainFormInst = new EditorForm();
			this.mainFormInst.Closed += this.MainFormCloseHandler;
		}

		private void MainFormCloseHandler(object sender, EventArgs e)
		{
			this.ExitThread();
		}

		private class ThreadExceptionHandler
		{
			public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
			{
				string str = "Необработанное исключение в потоке. Что-то пошло не так. ;(";
				ApplicationException newException = new ApplicationException(str, e.Exception);
				newException.Source = "EditorApplication";
				ExceptionMessageBox box = new ExceptionMessageBox(newException);
				box.Show(null);
			}
		}
	}
}