﻿namespace PPPV.Editor.Commands
{
   using System;
   using System.IO;
   using System.Text;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using PPPV.Net;

   public class OpenNetCommand : Command
   {
      public OpenNetCommand()
      {
         Name = "Открыть";
         Description = "Открыть сеть из файла";
         ShortcutKeys = Keys.Control | Keys.O;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Open.png"), true);
      }

      public override void Execute()
      {
         OpenFileDialog openFileDialog = new OpenFileDialog();
         openFileDialog.Filter = "txt files (*.pnml)|*.pnml|All files (*.*)|*.*";
         openFileDialog.FilterIndex = 1 ;
         openFileDialog.RestoreDirectory = true ;

         if(openFileDialog.ShowDialog() == DialogResult.OK)
         {
            StreamReader stream;
            EditorApplication app = EditorApplication.Instance;
            stream = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding(1251));
            app.NewNet(stream, openFileDialog.FileName);
            stream.Close();
         }
      }

      public override void Unexecute()
      {
      }
   }
}
