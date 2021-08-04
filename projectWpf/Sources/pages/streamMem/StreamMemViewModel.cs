using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace projectWpf.Sources.pages.streamMem
{
	class StreamMemViewModel : BaseViewModel
	{
		private MemReader _myReader;
		public MemReader myReader { get { return _myReader; } }

		private bool _setupResult;
		public bool SetupResult { get { return _setupResult; } set { _setupResult = value; NotifyPropertyChanged("SetupResult"); } }

		public StreamMemViewModel()
		{
			_myReader = new MemReader("$pcars2$");
			_setupResult = false;
		}
		public void SetupAndStartStreamRead()
		{
			SetupResult = _myReader.SetupStreamReading();
			if (SetupResult)
			{
				myReader.ReadStream();
			}
			Log($"Reading started -> {SetupResult}");
		}
		private ICommand _doSomething;
		public ICommand DoSomethingCommand
		{
			get
			{
				if (_doSomething == null)
				{
					_doSomething = new RelayCommand(
						p => !this._setupResult,
						p => this.SetupAndStartStreamRead());
				}
				return _doSomething;
			}
		}
	}
}
