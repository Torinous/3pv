/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 3:01
 *
 *
 */
namespace Pppv.Utils
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Diagnostics;
	using System.Globalization;
	using System.IO;
	using System.Text;
	using System.Windows.Forms;
	using System.Xml.Serialization;
	
	using Pppv.ApplicationFramework;

	public class Configuration<T> where T : IXmlSerializable, new()
	{
		private static Configuration<T> instance = null;
		private T data;
		private string sourceFile;

		private Configuration()
		{
			this.data = new T();
		}

		public static Configuration<T> Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Configuration<T>();
				}

				return instance;
			}
		}

		public T Data
		{
			get { return this.data; }
			set { this.data = value; }
		}

		public string SourceFile
		{
			get { return this.sourceFile; }
			set { this.sourceFile = value; }
		}

		public void Save()
		{
			XmlSerializer formatter = new XmlSerializer(typeof(T));
			Stream stream = new FileStream(this.SourceFile, FileMode.Create, FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, this.data);
			stream.Close();
		}

		public void Load()
		{
			try
			{
				XmlSerializer formatter = new XmlSerializer(typeof(T));
				Stream stream = new FileStream(this.SourceFile, FileMode.Open, FileAccess.Read, FileShare.None);
				this.data = (T)formatter.Deserialize(stream);
				stream.Close();
			}
			catch (Exception e)
			{
				throw new PppvException(String.Format(CultureInfo.InvariantCulture, "Возможно конфигурационный файл {0} старой версии, он будет перезаписан новым файлом по умолчанию.", this.SourceFile), e);
			}
		}
	}
}

