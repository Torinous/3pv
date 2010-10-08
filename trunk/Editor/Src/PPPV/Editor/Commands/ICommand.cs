﻿namespace Pppv.Editor.Commands
{
   using System;

   public interface ICommand
   {
      bool IsHistorical { get; }

      string Name { get; }

      void Execute();

      void Unexecute();
   }
}
