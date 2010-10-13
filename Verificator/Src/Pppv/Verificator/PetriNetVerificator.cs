﻿/*
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
   using System.IO;
   using System.Reflection;
   using System.Text;
   using System.Threading;
   using System.Windows.Forms;

   using Pppv.Net;

   using SbsSW.SwiPlCs;

   public class PetriNetVerificator
   {
      private static PetriNetVerificator instance;
      private PetriNet net;

      private PetriNetVerificator()
      {
         this.InitPrologEngineIfNeed();
      }

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

      private void InitPrologEngineIfNeed()
      {
         if (!PlEngine.IsInitialized)
         {
            String[] empty_param = { "-q" };
            PlEngine.Initialize(empty_param);
         }
      }

      public PetriNet Net
      {
         get { return net; }
         private set { net = value; }
      }

      public void StartInterface(Form parentForm)
      {
         Form verificatorForm = new VerificatorForm(Net);
         verificatorForm.ShowDialog(parentForm);
      }

      public  void LoadNetToPrologEngine(PetriNet petriNet)
      {
         this.Net = petriNet;
         InitPrologEngineIfNeed();
         PetriNetPrologTranslated netTranslator = new PetriNetPrologTranslated(Net);

         string tmpFile = Path.GetTempFileName();
         StreamWriter tmpFilestream = new StreamWriter(tmpFile, false, Encoding.GetEncoding(1251));
         tmpFilestream.Write(netTranslator.ToProlog());
         tmpFilestream.WriteLine();
         tmpFilestream.Write(PetriNetPrologTranslated.KernelCode);
         tmpFilestream.Close();
         tmpFile = tmpFile.Replace("\\", "\\\\");
         PlQuery.PlCall("consult('" + tmpFile + "').");
         File.Delete(tmpFile);
      }

      public void Cleanup()
      {
         PlEngine.PlCleanup();
      }

      public void CalculateStateSpace()
      {
         PlQuery.PlCall("goalgds.");
      }

      public string GetStateSpaceInDot()
      {
         return StateSpaceInDotFormatTranslator.Create();
      }
   }
}
