/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 3:27
 *
 *
 */

namespace Pppv.Verificator
{
   using System;
   using System.Diagnostics;
   using System.IO;
   using System.Reflection;
   using System.Text;
   using System.Threading;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   using SbsSW.SwiPlCs;

   public class PetriNetVerificator
   {
      private static PetriNetVerificator instance;
      private PetriNet net;
      private VerificatorForm mainForm;
      private Configuration<VerificatorConfigurationData> configuration;

      private PetriNetVerificator()
      {
         this.configuration = Configuration<VerificatorConfigurationData>.Instance;
         this.configuration.SourceFile = Environment.CurrentDirectory + "\\Verificator.conf";
         this.configuration.Load();
         this.InitPrologEngineIfNeed();
      }

      ~PetriNetVerificator()
      {
         this.configuration.Save();
      }

      public event EventHandler<PostStatusMessageArgs> PostStatusMessage;

      public static PetriNetVerificator Instance
      {
         get
         {
            if (instance == null)
            {
               instance = new PetriNetVerificator();
            }

            return instance;
         }
      }

      public VerificatorForm MainForm
      {
         get { return this.mainForm; }
         private set { this.mainForm = value; }
      }

      public PetriNet Net
      {
         get { return this.net; }
         private set { this.net = value; }
      }

      public void StartInterface(Form parentForm)
      {
         this.MainForm = new VerificatorForm(this);
         this.MainForm.ShowDialog(parentForm);
      }

      public void StopInterface()
      {
         this.MainForm.Close();
         this.MainForm = null;
      }

      public void LoadNetToPrologEngine(PetriNet petriNet)
      {
         this.SetStatusMessage("Загрузка сети: " + petriNet.Id);
         DateTime startTime = DateTime.Now;
         this.Net = petriNet;
         this.InitPrologEngineIfNeed();
         PetriNetPrologTranslated netTranslator = new PetriNetPrologTranslated(this.Net);

         string tmpFile = Path.GetTempFileName();
         StreamWriter tmpFilestream = new StreamWriter(tmpFile, false, Encoding.GetEncoding(1251));
         tmpFilestream.Write(netTranslator.ToProlog());
         tmpFilestream.Close();
         tmpFile = tmpFile.Replace("\\", "\\\\");
         PlQuery.PlCall("consult('" + tmpFile + "').");
         File.Delete(tmpFile);
         TimeSpan duration = DateTime.Now - startTime;
         this.SetStatusMessage(String.Format("Сеть: {0} загружена(за {1} мс)", petriNet.Id, duration.TotalMilliseconds));
      }

      public void Cleanup()
      {
      }

      public void CalculateStateSpace()
      {
         this.SetStatusMessage("Вычисление пространства состояний сети: " + this.Net.Id);
         DateTime startTime = DateTime.Now;
         PlQuery.PlCall("statespace:createStateSpace.");
         TimeSpan duration = DateTime.Now - startTime;
         this.SetStatusMessage(String.Format("Пространство состояний сети: {0} построено({1} мс)", this.Net.Id, duration.TotalMilliseconds));
      }

      public string GetStateSpaceInDot()
      {
         return StateSpaceInDotFormatTranslator.Create();
      }

      public void StartPrologInterface()
      {
         PetriNetPrologTranslated netTranslator = new PetriNetPrologTranslated(this.Net);
         string tmpFile = Path.GetTempFileName();
         StreamWriter tmpFilestream = new StreamWriter(tmpFile, false, Encoding.GetEncoding(1251));
         tmpFilestream.Write(netTranslator.ToProlog());
         tmpFilestream.Close();

         Process prologProcess = new Process();
         prologProcess.StartInfo.FileName = "swipl-win.exe";
         string libraryPath = Environment.CurrentDirectory + "\\Prolog";
         tmpFile = tmpFile.Replace("\\", "\\\\");
         prologProcess.StartInfo.Arguments = "-g consult('" + tmpFile + "') -q -p pppv_library=" + libraryPath;
         DebugAssistant.LogTrace("Start process by [" + prologProcess.StartInfo.FileName + " " + prologProcess.StartInfo.Arguments + "]");
         try
         {
            prologProcess.Start();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
            DebugAssistant.LogTrace(ex.Message);
         }

         DebugAssistant.LogTrace("\tOK");
      }

      protected virtual void OnPostStatusMessage(PostStatusMessageArgs e)
      {
         if (this.PostStatusMessage != null)
         {
            this.PostStatusMessage(this, e);
         }
      }

      protected void SetStatusMessage(string message)
      {
         this.OnPostStatusMessage(new PostStatusMessageArgs(message));
      }

      private void InitPrologEngineIfNeed()
      {
         if (!PlEngine.IsInitialized)
         {
            string[] param = { "-q", "-p", "pppv_library=" + Environment.CurrentDirectory + "\\Prolog" };
            PlEngine.Initialize(param);
         }
      }
   }
}
