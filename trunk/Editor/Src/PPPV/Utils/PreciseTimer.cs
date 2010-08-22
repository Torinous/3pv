using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;

namespace PPPV.Utils
{
public sealed class PreciseTimer : IDisposable
	{

		private static long ctr1, ctr2, freq;
		private string id;
		private static Hashtable timerTable = new Hashtable();

		[DllImport("kernel32.dll")]
		public extern static short QueryPerformanceCounter(ref long x);

		[DllImport("kernel32.dll")]
		public extern static short QueryPerformanceFrequency(ref long x);

		public PreciseTimer(string id)
		{
			QueryPerformanceFrequency(ref freq);
			this.id = id;
			QueryPerformanceCounter(ref ctr1);
		}

		public void Dispose()
		{
			QueryPerformanceCounter(ref ctr2);
			if(timerTable.ContainsKey(id))
			{
				timerTable[id] = (((double)timerTable[id] + (ctr2 - ctr1)*1000.0/freq)/2.0);
			}
			else
			{
				timerTable.Add(id, (ctr2 - ctr1)*1000.0/freq);
			}
		}

		public static void ShowTimeTable()
		{
			System.Diagnostics.Debug.WriteLine("TimerTable:\n");
			ICollection c = timerTable.Keys;
			foreach(string str in c)
			{
				System.Diagnostics.Debug.WriteLine(String.Format(CultureInfo.CurrentCulture, "{0}: {1,10:N5} ms",str, timerTable[str]));
			}
		}
	}
}
