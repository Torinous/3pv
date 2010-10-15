namespace Pppv.ApplicationFramework.Utils
{
   using System;
   using System.Collections;
   using System.Diagnostics;
   using System.Globalization;
   using System.Runtime.InteropServices;

   public sealed class PreciseTimer : IDisposable
   {
      private static long ctr1, ctr2, freq;
      private static Hashtable timerTable = new Hashtable();
      private string id;

      public PreciseTimer(string id)
      {
         NativeMethods.QueryPerformanceFrequency(out freq);
         this.id = id;
         NativeMethods.QueryPerformanceCounter(out ctr1);
      }

      public static void ShowTimeTable()
      {
         System.Diagnostics.Debug.WriteLine("TimerTable:\n");
         ICollection c = timerTable.Keys;
         foreach (string str in c)
         {
            System.Diagnostics.Debug.WriteLine(String.Format(CultureInfo.CurrentCulture, "{0}: {1,10:N5} ms", str, timerTable[str]));
         }
      }

      public void Dispose()
      {
         NativeMethods.QueryPerformanceCounter(out ctr2);
         if (timerTable.ContainsKey(this.id))
         {
            timerTable[this.id] = ((double)timerTable[this.id] + (((ctr2 - ctr1) * 1000.0) / freq)) / 2.0;
         }
         else
         {
            timerTable.Add(this.id, (ctr2 - ctr1) * 1000.0 / freq);
         }
      }
   }
}