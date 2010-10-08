namespace Pppv.Translator
{
   using System;
   using System.Collections.Generic;
   using System.Configuration;
   using System.Diagnostics;
   using System.IO;
   using System.Reflection;
   using System.Resources;
   using System.Text;
   using System.Xml;
   using System.Xml.Serialization;

   using Pppv.Net;
   using Pppv.Utils;

   public class TranslatorToProlog
   {
      #region Private Variables
      private string inputFileName = "none";
      private string outputFileName = "none";
      private bool helpReq;
      private bool addKernel = true;
      private PetriNetPrologTranslated net;
      private Encoding enc1251;
      #endregion

      public TranslatorToProlog()
      {
         this.enc1251 = Encoding.GetEncoding(1251);
      }

      #region Properties and Command Line Switches
      [CommandLineSwitch("O", "Target for output.")]
      [CommandLineAlias("o")]
      public string OutputTarget
      {
         get { return this.outputFileName; }
         set { this.outputFileName = value; }
      }
      
      [CommandLineSwitch("help", "Show some help.")]
      [CommandLineAlias("h")]
      public bool HelpRequest
      {
         get { return this.helpReq; }
         set { this.helpReq = value; }
      }

      [CommandLineSwitch("AddKernel", "Add verification Kernel code.")]
      [CommandLineAlias("ak")]
      public bool AddKernel
      {
         get { return this.addKernel; }
         set { this.addKernel = value; }
      }
      #endregion

      private static int Main()
      {
         (new TranslatorToProlog()).Run();
         return 0;
      }

      private int Run()
      {
         Debug.WriteLine(System.Environment.CommandLine);
         Parser cmdParser = new Parser(System.Environment.CommandLine, this);
         try
         {
            cmdParser.Parse();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            throw;
         }

         if (this.HelpRequest)
         {
            Console.WriteLine("pnml2prolog v. " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            return 0;
         }

         if (cmdParser.Parameters.Length == 1)
         {
            this.inputFileName = cmdParser.Parameters[0];
         }
         else
         {
            if (cmdParser.Parameters.Length > 1)
            {
               Console.WriteLine("\nYou must specify only one or none input files.");
               return 0;
            }
         }

         XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
         if (this.inputFileName == "none")
         {
            try
            {
               this.net = (PetriNetPrologTranslated)serializer.Deserialize(Console.In);
            }
            catch (XmlException ex)
            {
               Console.WriteLine(ex.Message);
               return 1;
            }
         }
         else
         {
            try
            {
               this.net = (PetriNetPrologTranslated)serializer.Deserialize(File.OpenText(this.inputFileName));
            }
            catch (XmlException ex)
            {
               Console.WriteLine(ex.Message);
               return 1;
            }
         }

         if (this.outputFileName == "none")
         {
            try
            {
               Console.WriteLine(this.net.ToProlog());
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
               throw;
            }

            if (this.AddKernel)
            {
               Console.WriteLine(this.KernelCode());
            }
         }
         else
         {
            StreamWriter targetText = new StreamWriter(this.outputFileName, false, this.enc1251);
            try
            {
               targetText.WriteLine(this.net.ToProlog());
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
               throw;
            }

            if (this.AddKernel)
            {
               targetText.WriteLine(this.KernelCode());
            }

            targetText.Close();
         }

         return 0;
      }

      private string KernelCode()
      {
         Assembly current = Assembly.GetExecutingAssembly();
         StreamReader stR;

         StringBuilder code = new StringBuilder(3000);

         stR = new StreamReader(current.GetManifestResourceStream("ss_kernel.pl"), this.enc1251);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("ss_requests.pl"), this.enc1251);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("ctl_kernel.pl"), this.enc1251);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("ctl_requests.pl"), this.enc1251);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("report_kernel.pl"), this.enc1251);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("main.pl"), this.enc1251);
         code.AppendLine(stR.ReadToEnd());
         code.AppendLine();
         return code.ToString();
      }
   }
}
