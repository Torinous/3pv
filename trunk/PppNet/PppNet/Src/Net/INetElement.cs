namespace Pppv.Net
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	public interface INetElement : IXmlSerializable
	{
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Для декартовых координат прекрасно подходит")]
		int Y { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Для декартовых координат прекрасно подходит")]
		int X { get; set; }

		string Id { get; }

		string Name { get; set; }

		PetriNet ParentNet { get; set; }

		void SetId(int number);
	}
}
