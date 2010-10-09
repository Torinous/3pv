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

   using Pppv.Editor.Commands;
   using Pppv.Editor.Tools;
   using Pppv.Net;
   using Pppv.Utils;

   public class EditorApplication : ApplicationContext
   {
      private Editor.MainForm mainFormInst;

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

      public MainForm MainFormInst
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
         TextWriterTraceListener myListener = new TextWriterTraceListener(logFile);
         
         System.Diagnostics.Debug.Listeners.Add(tr1);
         System.Diagnostics.Debug.Listeners.Add(myListener);
         System.Diagnostics.Debug.AutoFlush = true;
      }

      /*public void Run()
      {
         
         System.Windows.Forms.Application.Run(this.mainFormInst);
         PreciseTimer.ShowTimeTable();
      }*/

      [STAThread]
      private static void Main()
      {
         Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);
         Application.Run(new EditorApplication());
      }

      private void InitializeComponent()
      {
         this.mainFormInst = new Editor.MainForm();
         this.mainFormInst.Closed += this.MainFormCloseHandler;
      }

      private void MainFormCloseHandler(object sender, EventArgs e)
      {
         ExitThread();
      }

      private class ThreadExceptionHandler
      {
         public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
         {
            // TODO: Тут бы надо написать хорошую форму для показа Exceptions
            RtlAwareMessageBox.Show(null, e.Exception.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
         }
      }
   }
}