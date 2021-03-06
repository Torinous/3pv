﻿namespace Pppv.ApplicationFramework.Utils
{
	using System;
	using System.Collections;
	using System.Diagnostics;
	using System.Globalization;
	using System.Reflection;

	public sealed class DebugAssistant
	{
		private static Hashtable namespaces;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "надо так")]
		static DebugAssistant()
		{
			// create the namespaces hashtable
			namespaces = new Hashtable();

			// get the assembly of this class
			Assembly a = Assembly.GetAssembly(new DebugAssistant().GetType());

			Trace.WriteLine("DebugAssistant: Types and Namespaces found in assembly :[" + a.FullName + "]");

			// now cycle through each type and gather up all the namespaces
			// awkward. Is there a way to cycle through namespaces directly?
			foreach (Type type in a.GetTypes())
			{
				// check if the namespace is already in our table and by default theyre all turned on
				Trace.WriteLine("\tType:" + type.ToString());
				if (type.Namespace != null)
				{
					if (!namespaces.Contains(type.Namespace))
					{
						namespaces.Add(type.Namespace, true);
						Trace.WriteLine("\tNameSpace:" + type.Namespace.ToString(CultureInfo.CurrentCulture));
					}
				}
			}
		}

		private DebugAssistant()
		{
		}

		public static Hashtable Namespaces
		{
			get { return namespaces; }
		}

		/// <summary>
		/// Set debugging to on or off for a specific namespace.
		/// </summary>
		/// <param name="ns"></param>
		/// <param name="b"></param>
		[Conditional("DEBUG")]
		public static void DebugNamespace(string ns, bool isDebug)
		{
			// check if the namespace exists
			if (ns != null)
			{
				if (namespaces.Contains(ns))
				{
					namespaces[ns] = isDebug;
				}
			}
		}

		/// <summary>
		/// The log function that triggers all the registered delegates.
		/// </summary>
		/// <param name="msg"></param>
		[Conditional("DEBUG")]
		public static void LogTrace(string msg)
		{
			StackFrame sf = new StackFrame(1, true);
			Type t = sf.GetMethod().DeclaringType;

			// only proceed if the namespace in question is being debugged
			if (t != null)
			{
				if (t.Namespace != null)
				{
					if (namespaces[sf.GetMethod().DeclaringType.Namespace] == null)
					{
						namespaces.Add(sf.GetMethod().DeclaringType.Namespace, true);
						Trace.WriteLine("DebugAssistant: Added namespace [" + sf.GetMethod().DeclaringType.Namespace + "]");
					}

					if ((bool)namespaces[sf.GetMethod().DeclaringType.Namespace])
					{
						Trace.WriteLine("LOG:" + FormatMsg(msg, sf.GetFileName(), sf.GetFileLineNumber(), sf.GetMethod().Name, sf.GetMethod().ToString()));
					}
				}
			}
		}

		[Conditional("DEBUG")]
		public static void LogError(string msg)
		{
			StackFrame sf = new StackFrame(1, true);
			Type t = sf.GetMethod().DeclaringType;

			// only proceed if the namespace in question is being debugged
			if (t != null)
			{
				if (t.Namespace != null)
				{
					if (namespaces[sf.GetMethod().DeclaringType.Namespace] == null)
					{
						namespaces.Add(sf.GetMethod().DeclaringType.Namespace, true);
						Trace.WriteLine("DebugAssistant: Added namespace [" + sf.GetMethod().DeclaringType.Namespace + "]");
					}

					if ((bool)namespaces[sf.GetMethod().DeclaringType.Namespace])
					{
						Trace.WriteLine("ERR:" + FormatMsg(msg, sf.GetFileName(), sf.GetFileLineNumber(), sf.GetMethod().Name, sf.GetMethod().ToString()));
					}
				}
			}
		}

		private static string FormatMsg(string msg, string file, int lineNumber, string methodName, string method)
		{
			string str;
			string part1 = "[" + msg + "] " + methodName + "()" + ":";
			char[] delimiters = { '\\' };
			string[] elements = file.Split(delimiters);

			if (elements.Length <= 5)
			{
				str = part1 + file + ":" + lineNumber + " " + method;
			}
			else
			{
				str = part1 + elements[0] + "/" + elements[1] + "/.../"
					+ elements[elements.Length - 2] + "/" + elements[elements.Length - 1]
					+ ":" + lineNumber + " " + method +
					" [" + file + "]";
			}

			return str;
		}
	}
}