﻿namespace PPPV.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Diagnostics;
   using System.Windows.Forms;

   using PPPV.Net;
   using PPPV.Utils;

  public class CopyCommand : Command
  {
    //Данные

    //Конструктор
    public CopyCommand()
    {
      Name = "Копировать";
      Description = "Копировать выделенный элемент сети";
      ShortcutKeys = Keys.Control | Keys.C;
      Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Copy.png"), true);
    }

    //Методы
    public override void Execute()
    {
      
    }

    public override void Unexecute()
    {
      
    }
  }
}
