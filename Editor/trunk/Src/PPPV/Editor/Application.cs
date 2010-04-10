using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Windows.Forms;

using PPPV.Utils;

namespace PPPV.Editor 
{
  public class EditorApplication : ApplicationContext 
  {
     //Данные
     private Editor.MainForm _mainFormInst;
     
     //Акцессоры
     public Form MainFormInst
     {
       get
       {
         return _mainFormInst;
       }
     }
     
     /*Конструкторы*/
     public EditorApplication()
     {
       InitializeComponent();
       _mainFormInst.Show();
     }

      private void InitializeComponent()
      {
         _mainFormInst = new Editor.MainForm(this);
         _mainFormInst.Closed += MainFormCLoseHandler;
      }

      public void PrepareLog(){
         //Log preparing
         FileStream LogFile = new FileStream(System.Windows.Forms.Application.StartupPath + "\\log", FileMode.Create);
         TextWriterTraceListener MyListener = new TextWriterTraceListener(LogFile);
         Trace.Listeners.Add(MyListener);
         Trace.AutoFlush = true;
      }

      public void Run()
      {
         PrepareLog();
         /*TODO: Тут бы надо завернуть в Try и написать хорошую форму для показа Exceptions*/
         System.Windows.Forms.Application.Run(_mainFormInst);
         PreciseTimer.ShowTimeTable();
      }

      private void MainFormCLoseHandler(object sender, EventArgs e)
      {
        ExitThread();
      }
      public void Quit()
      {
        _mainFormInst.Close();
      }
      
      [STAThread]
      private static void Main(string[] args) 
      {
        /*Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);*/
       /* try
        {*/
           EditorApplication context = new EditorApplication();
           Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);
           Application.Run(context);
          //(new Editor.Application()).Run();
        /*}
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
        }*/
      }
      
      /// <summary>
      /// Handles any thread exceptions
      /// </summary>
      public class ThreadExceptionHandler
      {
        public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
          MessageBox.Show(e.Exception.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      
   } // class Application
} // namespace
