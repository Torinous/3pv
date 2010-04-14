﻿using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor.Commands
{
  public class CutCommand : Command
  {
    //Данные

    //Конструктор
    public CutCommand()
    {
      Name = "Вырезать";
      Description = "Вырезать выделенный элемент сети";
      ShortcutKeys = Keys.Control | Keys.X;
    }
    
    //Методы
    public override void Execute()
    {
      
    }

    public override void UnExecute()
    {
      
    }
    
    public override Image GetPictogram()
    {
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Cut.png"), true);
    }
  }
}