namespace Pppv.Net
{
	using System;
	using System.Collections;
	using System.Globalization;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	
	using Pppv.ApplicationFramework;

	[Serializable()]
	[XmlRoot("predicate")]
	public class Predicate : IXmlSerializable
	{
		private string text;

		public Predicate()
		{
		}

		public Predicate(string text)
		{
			this.text = Upperfy(text);
		}

		public string Text
		{
			get { return this.text; }
			set { this.text = Upperfy(value); }
		}

		public override string ToString()
		{
			return this.Text;
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("value");
			writer.WriteString(this.Text);
			writer.WriteEndElement(); // value
		}

		public void ReadXml(XmlReader reader)
		{
			if (reader.Name == "predicate")
			{
				if (!reader.IsEmptyElement)
				{
					reader.Read();
					if (reader.Name == "value")
					{
						this.Text = reader.ReadString();
						reader.ReadEndElement(); // predicate
					}
					else
					{
						throw new PppvException(String.Format(CultureInfo.InvariantCulture, "Невозможно десереализовать элемент predicate. Получен узел {0}, ожидался value", reader.Name));
					}
				}
			}
			else
			{
				throw new PppvException(String.Format(CultureInfo.InvariantCulture, "Невозможно десереализовать элемент predicate. Получен узел {0}", reader.Name));
			}
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		private static string Upperfy(string t)
		{
			string txt;
			if (t.Length > 1)
			{
				txt = t.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + t.Substring(1);
			}
			else
			{
				if (t.Length == 1)
				{
					txt = t.ToUpper(CultureInfo.CurrentCulture);
				}
				else
				{
					txt = t;
				}
			}

			return txt;
		}
	}
}