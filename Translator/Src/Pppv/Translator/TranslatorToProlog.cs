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
               Console.WriteLine(PetriNetPrologTranslated.KernelCode);
            }
         }
         else
         {
            StreamWriter targetText = new StreamWriter(this.OutputFileName, false, this.encoding);
            targetText.WriteLine(this.NetTranslator.ToProlog());

            if (this.NeedKernelCode)
            {
               targetText.WriteLine(PetriNetPrologTranslated.KernelCode);
            }

            targetText.Close();
         }

         return 0;
      }
   }
}
