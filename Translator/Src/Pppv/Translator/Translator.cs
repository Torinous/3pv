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
      private PetriNetPrologTranslated net;
      private Encoding encoding;
      private CommandLineArguments arguments;

      public TranslatorToProlog()
      {
         this.encoding = Encoding.GetEncoding(1251);
         arguments = new CommandLineArguments(Environment.GetCommandLineArgs());
      }

      #region Properties
      public CommandLineArguments Arguments
      {
         get { return this.arguments; }
         set { this.arguments = value; }
      }

      public PetriNetPrologTranslated Net
      {
         get { return this.net; }
         set { this.net = value; }
      }

      public Encoding Encoding
      {
         get { return this.encoding; }
         set { this.encoding = value; }
      }

      public string InputFileName
      {
         get { return arguments["input"]; }
      }

      public string OutputFileName
      {
         get { return arguments["output"]; }
      }

      public bool NeedHelp
      {
         get { return Arguments["h"] == "true" || Arguments["help"] == "true"; }
      }

      public bool NeedKernelCode
      {
         get { return arguments["addkernel"] == "true" || arguments["addkernel"] == null; }
      }
      #endregion

      private static int Main()
      {
         try
         {
            (new TranslatorToProlog()).Run();
         }
         catch (Exception e)
         {
            Console.WriteLine(e.Message);
            return 1;
         }
         return 0;
      }

      private int Run()
      {
         Console.WriteLine("input["+this.InputFileName+"]");
         Console.WriteLine("output["+this.OutputFileName+"]");

         if (this.NeedHelp)
         {
            Console.WriteLine("pnml2prolog v. " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            return 0;
         }

         XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
         if (this.InputFileName == null)
         {
            this.net = (PetriNetPrologTranslated)serializer.Deserialize(Console.In);
         }
         else
         {
            this.net = (PetriNetPrologTranslated)serializer.Deserialize(File.OpenText(this.InputFileName));
         }

         if (this.OutputFileName == null)
         {
            Console.WriteLine(this.net.ToProlog());

            if (this.NeedKernelCode)
            {
               Console.WriteLine(this.KernelCode());
            }
         }
         else
         {
            StreamWriter targetText = new StreamWriter(this.OutputFileName, false, this.encoding);
            targetText.WriteLine(this.net.ToProlog());

            if (this.NeedKernelCode)
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

         stR = new StreamReader(current.GetManifestResourceStream("ss_kernel.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("ss_requests.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("ctl_kernel.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("ctl_requests.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("report_kernel.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("main.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         code.AppendLine();
         return code.ToString();
      }
   }
}
