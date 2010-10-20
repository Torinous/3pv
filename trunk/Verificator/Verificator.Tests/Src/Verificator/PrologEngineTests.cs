/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 11.10.2010
 * Time: 2:57
 *
 *
 */
namespace Pppv.Verificator
{
	using System;
	using NUnit.Framework;

	using SbsSW.SwiPlCs;

	[TestFixture]
	public class PrologEngineTests
	{
		[Test]
		public void TestPrologEngineInitialize()
		{
			if (!PlEngine.IsInitialized)
			{
				string[] empty_param = { "-q" };
				PlEngine.Initialize(empty_param);
				PlEngine.PlCleanup();
			}
		}
		
		[Test]
		public void TestPrologSimpleQuery()
		{
			if (!PlEngine.IsInitialized)
			{
				string[] empty_param = { "-q" };
				PlEngine.Initialize(empty_param);
				using (PlQuery q = new PlQuery("member(A, [h,e,l,l,o])"))
				{
					foreach (PlTermV s in q.Solutions)
					{
						Console.WriteLine(s[0].ToString());
					}
				}
			}
		}
	}
}
