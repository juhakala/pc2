using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace projectWpf.Sources.pages
{
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
		public CLap(int dri, float ti, float[] secs)
		{
			driver = dri;
			time = ti;
			sectors = new float[3];
			sectors = secs;
		}
	}
	class CTeam
	{
		private ObservableCollection<string> _drivers;
		private int _curDriver;
		private int _nextDriver;
		public ObservableCollection<string> drivers {
			get { return _drivers; }
			set { _drivers = value; }
		}
		public ObservableCollection<CLap> Laps;
		public int curDriver {
			get { return _curDriver; }
			set { _curDriver = value; }
		}
		public int nextDriver
		{
			get { return _nextDriver; }
			set { _nextDriver = value; }
		}
		public CTeam()
		{
			Laps = new ObservableCollection<CLap>();
			drivers = new ObservableCollection<string>();
		}
		public void AddDriver(string name)
		{
			drivers.Add(name);
		}
		public void AddLap(float time, float sec1, float sec2, float sec3)
		{
			Laps.Add(new CLap(curDriver, time, new float[] { sec1, sec2, sec3 }));
		}
	}
}
