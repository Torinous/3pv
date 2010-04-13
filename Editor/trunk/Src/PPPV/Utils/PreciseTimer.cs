using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace PPPV.Utils
{
public class PreciseTimer : IDisposable
  {

    private static long ctr1 = 0, ctr2 = 0, freq = 0;
    private string ID;
    private static Hashtable timerTable = new Hashtable();

    [DllImport("kernel32.dll")]
    public extern static short QueryPerformanceCounter(ref long x);

    [DllImport("kernel32.dll")]
    public extern static short QueryPerformanceFrequency(ref long x);

    public PreciseTimer(string ID)
    {
      QueryPerformanceFrequency(ref freq);
      this.ID = ID;
      QueryPerformanceCounter(ref ctr1);
    }
    public void Dispose()
    {
      QueryPerformanceCounter(ref ctr2);
      if(timerTable.ContainsKey(ID))
      {
        timerTable[ID] = (((double)timerTable[ID] + (ctr2 - ctr1)*1000.0/freq)/2.0);
      }
      else
      {
        timerTable.Add(ID,(ctr2 - ctr1)*1000.0/freq);
      }
    }
    public static void ShowTimeTable()
    {
      System.Diagnostics.Debug.WriteLine("TimerTable:\n");
      ICollection c = timerTable.Keys;
      foreach(string str in c)
      {
        System.Diagnostics.Debug.WriteLine(String.Format("{0}: {1,10:N5} ms",str, timerTable[str]));
      }
    }
  }
}
