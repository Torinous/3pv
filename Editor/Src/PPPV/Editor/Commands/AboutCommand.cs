using System;
using System.Windows.Forms;
using System.Drawing;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
	public class AboutCommand : Command
	{
		//Данные
		private Control sender;

		//Конструктор
		public AboutCommand(Control sender_)
		{
			Name = "О программе";
			Description = "Вызов формы \"О программе\"";
			sender = sender_;
		}
		//Методы
		public override void Execute()
		{
			Form f = new AboutForm();
			f.ShowDialog(sender.FindForm());
			f.Dispose();
		}

		public override void UnExecute()
		{
		
		}
	}
}
