/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.08.2010
 * Time: 15:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Pppv.Utils
{
   using System;
   using System.Runtime.InteropServices;

   internal static class NativeMethods
   {
      [DllImport("kernel32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool QueryPerformanceCounter(out long x);

      [DllImport("kernel32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool QueryPerformanceFrequency(out long x);

      ////Мега хук чтобы избавиться от строк вида Ctrl-Oemplus
      [DllImport("User32.dll")]
      internal static extern int MapVirtualKey(int uCode, int uMapType);
   }
}