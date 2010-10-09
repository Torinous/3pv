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
      private PetriNetPrologTranslated netTranslator;
      private Encoding encoding;
      private CommandLineArguments arguments;

      public TranslatorToProlog()
      {
         this.encoding = Encoding.GetEncoding(1251);
         this.arguments = new CommandLineArguments(Environment.GetCommandLineArgs());
      }

      #region Properties
      public CommandLineArguments Arguments
      {
         get { return this.arguments; }
         set { this.arguments = value; }
      }

      public PetriNetPrologTranslated NetTranslator
      {
         get { return this.netTranslator; }
         set { this.netTranslator = value; }
      }

      public Encoding Encoding
      {
         get { return this.encoding; }
         set { this.encoding = value; }
      }

      public string InputFileName
      {
         get { return this.Arguments["input"]; }
      }

      public string OutputFileName
      {
         get { return this.Arguments["output"]; }
      }

      public bool NeedHelp
      {
         get { return this.Arguments["h"] == "true" || this.Arguments["help"] == "true"; }
      }

      public bool NeedKernelCode
      {
         get { return this.Arguments["addkernel"] == "true" || this.Arguments["addkernel"] == null; }
      }
      #endregion

      public string KernelCode()
      {
         Assembly current = Assembly.GetExecutingAssembly();
         StreamReader stR;

         StringBuilder code = new StringBuilder(3000);

         stR = new StreamReader(current.GetManifestResourceStream("Pppv.Resources.Verificator.ss_kernel.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("Pppv.Resources.Verificator.ss_requests.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("Pppv.Resources.Verificator.ctl_kernel.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("Pppv.Resources.Verificator.ctl_requests.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("Pppv.Resources.Verificator.report_kernel.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         stR = new StreamReader(current.GetManifestResourceStream("Pppv.Resources.Verificator.main.pl"), this.encoding);
         code.AppendLine(stR.ReadToEnd());
         code.AppendLine();
         return code.ToString();
      }

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
         Debug.WriteLine("input[" + this.InputFileName + "]");
         Debug.WriteLine("output[" + this.OutputFileName + "]");

         if (this.NeedHelp)
         {
            Console.WriteLine("pnml2prolog v. " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            return 0;
         }

         XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
         PetriNet net = new PetriNet();
         if (this.InputFileName == null)
         {
            net = serializer.Deserialize(Console.In) as PetriNet;
         }
         else
         {
            net = serializer.Deserialize(File.OpenText(this.InputFileName)) as PetriNet;
         }

         this.netTranslator = new PetriNetPrologTranslated(net);

         if (this.OutputFileName == null)
         {
            Console.WriteLine(this.NetTranslator.ToProlog());

            if (this.NeedKernelCode)
            {
               Console.WriteLine(this.KernelCode());
            }
         }
         else
         {
            StreamWriter targetText = new StreamWriter(this.OutputFileName, false, this.encoding);
            targetText.WriteLine(this.NetTranslator.ToProlog());

            if (this.NeedKernelCode)
            {
               targetText.WriteLine(this.KernelCode());
            }

            targetText.Close();
         }

         return 0;
      }
   }
}
