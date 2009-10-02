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

using PPPV.CommandLineParameterParser;

namespace PPPV.pnml2prolog
{
   class PNML2PrologConverter
   {
      #region Private Variables
      private string inputFileName = "none";
      private string outputFileName = "none";
      private bool HelpReq = false;
      private ITranslatedToProlog AbstractNet;
      private ResourceManager RManager;
      #endregion

      #region Properties and Command Line Switches
      [CommandLineSwitch("O", "Target for output.")]
      [CommandLineAlias("o")]
      public string outputTarget 
      {
         get {return outputFileName;}
         set {outputFileName = value;}
      }
        
      [CommandLineSwitch("help", "Show some help.")]
      [CommandLineAlias("h")]
      public bool HelpRequest
      {
         get { return HelpReq; }
         set { HelpReq = value; }
      }
      #endregion

      #region Constructor and Destructor
      public PNML2PrologConverter()
      {
         RManager = new ResourceManager("pnml2prolog.prolog", this.GetType().Assembly); 
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
         if (HelpReq) {
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

         XmlDocument PNMLFile = new System.Xml.XmlDocument();
         if (inputFileName == "none")
         {
            try
            {
               PNMLFile.Load(Console.In);
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
               PNMLFile.Load(inputFileName);
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
               return 1;
            }
         }
         XmlNodeList Nets = PNMLFile.GetElementsByTagName("pnml");
         string NetType = Nets[0].FirstChild.Attributes["type"].Value;
         switch (NetType)
         {
            case "P/T net":
            try
            {
               // create the XML serializer.
                        ptnet.pnml_pt net1 = new ptnet.pnml_pt();
                        XmlSerializer formatter = new XmlSerializer(net1.GetType());
                        XmlTextReader tr = new XmlTextReader( new StringReader(PNMLFile.DocumentElement.OwnerDocument.OuterXml));
                        net1 = (ptnet.pnml_pt)formatter.Deserialize(tr);
                        AbstractNet = net1;
                    }
                    catch (Exception e)
                    {
                    	Console.WriteLine("Hello again");
                        Console.WriteLine(e.Message);
                        return 1;
                    }
                    break;
                case "PPr/T net":
                    try
                    {
                        // create the XML serializer.
                        pprtnet.pnml_pprt net1 = new pprtnet.pnml_pprt();
                        XmlSerializer formatter = new XmlSerializer(net1.GetType());
                        XmlTextReader tr = new XmlTextReader( new StringReader(PNMLFile.DocumentElement.OwnerDocument.OuterXml));
                        net1 = (pprtnet.pnml_pprt)formatter.Deserialize(tr);
                        AbstractNet = net1;
                    }
                    catch (Exception e)
                    {
                    	Console.WriteLine("Error Deser PPr/T");
                        Console.WriteLine(e.Message);
                        return 1;
                    }
                    break;
                default:
                    Console.WriteLine("I`m sorry, but seems i do not understand that net type.");
                    Console.WriteLine("\t\t\t\t\t\t\tpnml2prolog v. " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
                  return 1;
         }
         Encoding enc1251 = Encoding.GetEncoding(1251);
         if (outputFileName == "none")
         {
            try
            {
               Console.WriteLine(AbstractNet.ToProlog());
            }
            catch (Exception e) 
            {
               Console.WriteLine(e.Message);
               return 1;
            }
            Console.WriteLine(AdditionalCode());
         }
         else 
         {
            StreamWriter TargetText = new StreamWriter(outputFileName,false,enc1251);
            try
            {
               TargetText.WriteLine(AbstractNet.ToProlog());
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
               return 1;
            }
            TargetText.WriteLine(AdditionalCode());
            TargetText.Close();
         }
      return 0;
   }
   #endregion

   private string AdditionalCode()
   {
      StringBuilder code = new StringBuilder(2000);
      code.AppendLine(RManager.GetString("gdm_kernel"));
      code.AppendLine();
      code.AppendLine(RManager.GetString("gdm_requests"));
      code.AppendLine();
      code.AppendLine(RManager.GetString("ctl_kernel"));
      code.AppendLine();
      code.AppendLine(RManager.GetString("ctl_requests"));
      code.AppendLine();
      code.AppendLine(RManager.GetString("report"));
      code.AppendLine();
      code.AppendLine(RManager.GetString("main"));
      code.AppendLine();
      return code.ToString();
   }

      //точка входа
      static int Main(string[] args)
      {
         (new PNML2PrologConverter()).Run();
      }
   }
}
