/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 31.10.2010
 * Time: 2:48
 *
 *
 */

namespace Pppv.Verificator
{
	using System;
	
	using SbsSW.SwiPlCs;

	public static class SWIProlog
	{
		public static void InitPrologEngineIfNeed()
		{
			if (!PlEngine.IsInitialized)
			{
				string[] param = { "-q", "-p", "pppv_library=" + Environment.CurrentDirectory + "\\Prolog" };
				PlEngine.Initialize(param);
			}
		}
	}
}
