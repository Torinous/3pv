namespace Pppv.Translator
{
   using System;
   using System.Text;
   using System.IO;
   using System.Xml;
   using System.Xml.Serialization;
   using System.Resources;
   using System.Reflection;
   using System.Diagnostics;
   using System.Configuration;
   using System.Collections.Generic;

   using Pppv.Utils;
   using Pppv.Net;

   class PNML2PrologConverter
   {
      #region Private Variables
      private string inputFileName = "none";
      private string outputFileName = "none";
      private bool HelpReq;
      private bool addKernel = true;
      private PetriNetPrologTranslated net;
      private Encoding enc1251;
      #endregion

      #region Properties and Command Line Switches
      [CommandLineSwitch("O", "Target for output.")]
      [CommandLineAlias("o")]
      public string outputTarget {
         get {return outputFileName;}
         set {outputFileName = value;}
      }
      
      [CommandLineSwitch("help", "Show some help.")]
      [CommandLineAlias("h")]
      public bool HelpRequest{
         get { return HelpReq; }
         set { HelpReq = value; }
      }

      [CommandLineSwitch("AddKernel", "Add verification Kernel code.")]
      [CommandLineAlias("ak")]
      public bool AddKernel{
         get { return addKernel; }
         set { addKernel = value; }
      }
      #endregion

      #region Constructor and Destructor
      public PNML2PrologConverter(){
         enc1251 = Encoding.GetEncoding(1251);
      }
      #endregion

      #region Private Utility Functions
      private int Run()
      {
         Debug.WriteLine(System.Environment.CommandLine);
         Parser cmdParser = new Parser(System.Environment.CommandLine, this);
         try
         {
            cmdParser.Parse();
         }
         catch(Exception ex)
         {
            Console.WriteLine(ex.Message);
            return 1;
         }

         if (HelpReq)
         {
            Console.WriteLine("pnml2prolog v. " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            return 0;
         }

         if (cmdParser.Parameters.Length == 1)
         {
            inputFileName = cmdParser.Parameters[0];
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
         if (inputFileName == "none")
         {
            try
            {
               net = (PetriNetPrologTranslated)serializer.Deserialize(Console.In);
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
               return 1;
            }
         }
         else
         {
            try
            {
               net = (PetriNetPrologTranslated)serializer.Deserialize(File.OpenText(inputFileName));
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
               return 1;
            }
         }

         if (outputFileName == "none")
         {
            try
            {
               Console.WriteLine(net.ToProlog());
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
               return 1;
            }

            if(AddKernel)
            {
               Console.WriteLine(KernelCode());
            }
         }
         else
         {
            StreamWriter TargetText = new StreamWriter(outputFileName, false, enc1251);
            try
            {
               TargetText.WriteLine(net.ToProlog());
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
               return 1;
            }
            if(AddKernel)
            {
               TargetText.WriteLine(KernelCode());
            }
            TargetText.Close();
         }
         return 0;
      }
      #endregion

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

      //точка входа
      static int Main(string[] args)
      {
         (new PNML2PrologConverter()).Run();
         return 0;
      }
   }
}
