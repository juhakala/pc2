using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace projectWpf.Sources.pages
{
	class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private static ObservableCollection<LogEvent> _logEvents;
		public static ObservableCollection<LogEvent> LogEvents
		{ 
			get 
			{
				if (_logEvents == null)
				{
					_logEvents = new ObservableCollection<LogEvent>();
				}
				return _logEvents;
			}
		}
		public void Log(string message)
		{
			_logEvents.Add(new LogEvent(message));
		}
		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
