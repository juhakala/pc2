using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace projectWpf.Sources.pages
{
	class CDriver : BaseViewModel
	{
		private string _name;
		private bool _active;
		private Stopwatch _stopWatch;
		public string name {
			get { return _name; }
			set { _name = value; NotifyPropertyChanged("name"); }
		}
		public bool active {
			get { return _active; }
			set { _active = value; NotifyPropertyChanged("active"); }
		}
		public Stopwatch stopwatch {
			get { return _stopWatch; }
			set { _stopWatch = value; }
		}
		private string _elapsedTime;
		public string ElapsedTime
		{
			get { return _elapsedTime; }
			set {
				_elapsedTime = stopwatch.Elapsed.ToString(@"hh\:mm");
				NotifyPropertyChanged("ElapsedTime");
			}
		}
		public CDriver(string nam, bool act = false)
		{
			name = nam;
			active = act;
			stopwatch = new Stopwatch();
			_elapsedTime = stopwatch.Elapsed.ToString(@"hh\:mm");
		}
	}

	class CLap
	{
		private float _time;
		public float time {
			get { return _time; }
			set
			{
				_time = value;
			}
		}
		private float[] _sectors;
		public float[] sectors {
			get { return _sectors; }
			set
			{
				_sectors = value;
			}
		}
		private int _driver;
		public int driver {
			get { return _driver; }
			set
			{
				_driver = value;
			}
		}
		private int _number;
		public int number
		{
			get { return _number; }
			set
			{
				_number = value;
			}
		}
		public CLap(int dri, float ti, float[] secs, int nLap)
		{
			driver = dri;
			time = ti;
			sectors = new float[3];
			sectors = secs;
			number = nLap;
		}
	}
	class CTeam : BaseViewModel
	{
		private MemObj memVm;
		private ObservableCollection<CDriver> _drivers;
		private ObservableCollection<CLap> _Laps;
		private int _curDriver;
		private int _nextDriver;
		private string _name;
		private int _selectedDriver;
		private int _index;

		public int index {
			get { return _index; }
			set { _index = value; }
		}

		public ObservableCollection<CDriver> drivers {
			get { return _drivers; }
			set { _drivers = value; }
		}
		public ObservableCollection<CLap> Laps {
			get { return _Laps; }
			set { _Laps = value; }
		}
		public int curDriver {
			get { return _curDriver; }
			set
			{
				_curDriver = value;
				foreach (var driver in drivers)
				{
					driver.active = false;
				}
				drivers[value].active = true;
			}
		}
		public int nextDriver {
			get { return _nextDriver; }
			set { _nextDriver = value; }
		}
		public string name {
			get { return _name; }
			set
			{
				_name = value;
				NotifyPropertyChanged("name");
			}
		}
		private bool _active;
		public bool active
		{
			get { return _active; }
			set
			{
				_active = value;
				Debug.WriteLine($"{index} active");
				NotifyPropertyChanged("active");
			}
		}
		public int selectedDriver {
			get { return _selectedDriver; }
			set
			{
				_selectedDriver = value;
				NotifyPropertyChanged("selectedDriver");
				if (value != -1)
				{
					memVm.keepChildSelected(index);
					memVm.selectedTeam = index;
				}
			}
		}
		public CTeam(string nam, string dri = "Comp")
		{
			Laps = new ObservableCollection<CLap>();
			drivers = new ObservableCollection<CDriver>();
			name = nam;
			AddDriver(dri);
			curDriver = 0;
			nextDriver = 0;
			active = false;
		}
		public void AddDriver(string name)
		{
			if (drivers.Count == 1 && drivers[0].name == "Comp")
				drivers[0].name = name;
			else
				drivers.Add(new CDriver(name));
		}
		public void AddLap(float time, float sec1, float sec2, float sec3)
		{
			Laps.Add(new CLap(curDriver, time, new float[] { sec1, sec2, sec3 }, Laps.Count + 1));
		}
		public void SetNextDriver()
		{
			if (selectedDriver > -1)
				nextDriver = selectedDriver;
		}
		public void ChangeDriver()
		{
			drivers[curDriver].stopwatch.Stop();
			curDriver = nextDriver;
			drivers[curDriver].stopwatch.Start();
		}
		public void GiveParentViewModel(MemObj memV, int ind)
		{
			memVm = memV;
			index = ind;
		}

		public Timer aTimer;
		public void startElapsedTimerNotices()
		{
			if (drivers.Count > 1)
			{
				if (aTimer == null)
				{
					aTimer = new System.Timers.Timer();
					aTimer.Interval = 20000;
					aTimer.Elapsed += OnTimedEvent;
					aTimer.Enabled = true;
				}
			}
		}
		private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
		{
			drivers[curDriver].ElapsedTime = "";
		}
	}
}
