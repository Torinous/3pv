/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.08.2010
 * Time: 15:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace PPPV.Utils
{
   using System;
   using System.Runtime.InteropServices;

   internal static class NativeMethods
   {
      [DllImport("kernel32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal  extern static bool QueryPerformanceCounter(out long x);

      [DllImport("kernel32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal extern static bool QueryPerformanceFrequency(out long x);
   }
}
