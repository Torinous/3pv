﻿namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class QuitCommand : Command
   {
      public QuitCommand()
      {
         this.Name = "Выход";
         this.Description = "Завершение работы приложения 3PV:Editor";
         this.ShortcutKeys = Keys.Control | Keys.Q;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
      }

      public override void Execute()
      {
         EditorApplication.Instance.Quit();
      }

      public override void Unexecute()
      {
      }
   }
}