namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.IO;
   using System.Reflection;
   using System.Text;
   using System.Windows.Forms;

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
            MainForm mainForm = MainForm.Instance;
            StreamReader stream;
            stream = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding(1251));
            mainForm.LoadNet(stream, openFileDialog.FileName);
            stream.Close();
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