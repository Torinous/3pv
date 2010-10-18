/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 5:27
 *
 *
 */

namespace Pppv.Verificator.Commands
{
   using System;
   using System.Drawing;
   using System.IO;
   using System.Reflection;
   using System.Text;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.ApplicationFramework.Commands;
   using Pppv.Net;

   public class OpenNetCommand : InterfaceCommand
   {
      public OpenNetCommand()
      {
         this.Name = "Открыть";
         this.Description = "Открыть сеть из файла";
         this.ShortcutKeys = Keys.Control | Keys.O;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Open.png"), true);
      }

      public override void Execute()
      {
         OpenFileDialog openFileDialog = new OpenFileDialog();
         openFileDialog.Filter = "txt files (*.pnml)|*.pnml|All files (*.*)|*.*";
         openFileDialog.FilterIndex = 1;
         openFileDialog.RestoreDirectory = true;

         if (openFileDialog.ShowDialog() == DialogResult.OK)
         {
            PetriNetVerificator verificator = PetriNetVerificator.Instance;
            PetriNet net = new PetriNet();
            StreamReader stream;
            stream = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding(1251));
            XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
            net = serializer.Deserialize(stream) as PetriNet;
            stream.Close();
            verificator.LoadNetToPrologEngine(net);
         }
      }

      public override void Unexecute()
      {
      }

      public override bool CheckEnabled()
      {
         return true;
      }
   }
}