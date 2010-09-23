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
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Editor.Commands;
   using Pppv.Editor.Tools;
   using Pppv.Net;
   using Pppv.Utils;

   public class EditorApplication : ApplicationContext
   {
      private static EditorApplication instance;
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

      public static EditorApplication Instance
      {
         get
         {
            if (instance == null)
            {
               instance = new EditorApplication();
            }

            return instance;
         }
      }
      
      public MainForm MainFormInst
      {
         get
         {
            return this.mainFormInst;
         }
      }

      public PetriNetWrapper ActiveNet
      {
         get
         {
            if (this.MainFormInst.TabControl.SelectedIndex != -1)
            {
               return (this.MainFormInst.TabControl.TabPages[this.MainFormInst.TabControl.SelectedIndex] as TabPageForNet).Net;
            }
            else
            {
               return null;
            }
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

      public void Run()
      {
         /*TODO: Тут бы надо завернуть в Try и написать хорошую форму для показа Exceptions*/
         System.Windows.Forms.Application.Run(this.mainFormInst);
         PreciseTimer.ShowTimeTable();
      }

      public void Quit()
      {
         this.mainFormInst.Close();
      }

      public void NewNet()
      {
         PetriNetWrapper net = new PetriNetWrapper();
         TabPageForNet addedTabPage = this.MainFormInst.TabControl.AddNewTab();
         addedTabPage.PutNetOnTabPage(net);
      }

      public void LoadNet(TextReader netStream, string fileName)
      {
         if (netStream != null)
         {
            PetriNetWrapper net = new PetriNetWrapper();
            XmlSerializer serealizer = new XmlSerializer(net.GetType());
            net = (PetriNetWrapper)serealizer.Deserialize(netStream);
            net.FileOfNetPath = fileName;
            TabPageForNet addedTabPage = this.MainFormInst.TabControl.AddNewTab();
            addedTabPage.PutNetOnTabPage(net);
         }
      }

      public void CloseNet(PetriNet net)
      {
         if (this.MainFormInst.TabControl.SelectedIndex != -1)
         {
            this.MainFormInst.TabControl.CloseTab(this.MainFormInst.TabControl.TabIdForNet(net));
         }
      }

      [STAThread]
      private static void Main()
      {
         EditorApplication context = EditorApplication.Instance;
         Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);
         Application.Run(context);
      }

      private void InitializeComponent()
      {
         this.mainFormInst = new Editor.MainForm(this);
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
            RtlAwareMessageBox.Show((Control)sender, e.Exception.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
         }
      }
   }
}