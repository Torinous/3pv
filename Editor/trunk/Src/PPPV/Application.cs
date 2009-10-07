using System;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;

using PPPV.Editor;
using PPPV.Utils;

namespace PPPV {
   public class Application {
      private Editor.MainForm _mainFormInst;

      /*Конструкторы*/
      public Application() {

         InitializeComponent();
      }

      private void InitializeComponent() {
         _mainFormInst = new Editor.MainForm();
      }

      public void PrepareLog(){
         //Log preparing
         FileStream LogFile = new FileStream(System.Windows.Forms.Application.StartupPath + "\\log", FileMode.Create);
         TextWriterTraceListener MyListener = new TextWriterTraceListener(LogFile);
         Trace.Listeners.Add(MyListener);
         Trace.AutoFlush = true;
      }

      public void Run() {
         PrepareLog();
         /*TODO: Тут бы надо завернуть в Try и написать хорошую форму для показа Exceptions*/
         System.Windows.Forms.Application.Run(_mainFormInst);
         PreciseTimer.ShowTimeTable();
      }
   } // class Application
} // namespace
