/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.08.2010
 * Time: 15:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace PPPV.Editor
{
	using System;
	using System.Runtime.InteropServices;
	
	internal static class NativeMethods
	{
		//Мега хук чтобы избавиться от строк вида Ctrl-Oemplus
		[DllImport("User32.dll")]
		internal static extern int MapVirtualKey(int uCode, int uMapType);
	}
}
