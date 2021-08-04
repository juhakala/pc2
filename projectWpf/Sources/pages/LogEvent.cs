using System;
using System.Collections.Generic;
using System.Text;

namespace projectWpf.Sources.pages
{
	class LogEvent
	{
		private string _name;

		public string Name { get { return _name; } set { _name = value; } }
		private string _time;

		public string Time { get { return _time; } set { _time = value; } }

		public LogEvent(string name)
		{
			Name = name;
			Time = DateTime.Now.ToString("HH:mm:ss");
		}
	}
}
