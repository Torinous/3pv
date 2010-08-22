using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Editor;

namespace PPPV.Net{

	public class SaveEventArgs : EventArgs
	{
		string fileName;
		string netID;

		public string NetID {
			get { return netID; }
			set { netID = value; }
		}

		public string FileName {
			get { return fileName; }
			set { fileName = value; }
		}

		public SaveEventArgs(string fileName_, string netID_){
			fileName = fileName_;
			netID = netID_;
		}
	}
}
