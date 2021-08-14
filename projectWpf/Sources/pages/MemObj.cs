using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace projectWpf.Sources.pages
{
	enum Vector
	{
		VEC_X = 0,
		VEC_Y,
		VEC_Z,
		//-------------
		VEC_MAX
	};
	class ParticipantInfo : BaseViewModel
	{
		//static int STRING_LENGTH_MAX = 64;

		bool _mIsActive;
		string _mName;
		ObservableCollection<float> _mWorldPosition;// change to ObservableCollection<float> and init in constructor
		float _mCurrentLapDistance;
		uint _mRacePosition;
		uint _mLapsCompleted;
		uint _mCurrentLap;
		int _mCurrentSector;
		//100bytes
		float _mCurrentSector1Time;
		float _mCurrentSector2Time;
		float _mCurrentSector3Time;

		float _mFastestSector1Time;
		float _mFastestSector2Time;
		float _mFastestSector3Time;

		float _mFastestLapTime;
		float _mLastLapTime;
		bool _mLapInvalidated;
		uint _mRaceState;
		uint _mPitMode;
		float _mSpeed;


		public bool mIsActive { get { return _mIsActive; } set { _mIsActive = value; NotifyPropertyChanged("mIsActive"); } }
		public string mName { get { return _mName; } set { _mName = value; NotifyPropertyChanged("mName"); } }
		public ObservableCollection<float> mWorldPosition { get { return _mWorldPosition; } set { _mWorldPosition = value; NotifyPropertyChanged("mWorldPosition"); } }
		public float mCurrentLapDistance { get { return _mCurrentLapDistance; } set { _mCurrentLapDistance = value; NotifyPropertyChanged("mCurrentLapDistance"); } }
		public uint mRacePosition { get { return _mRacePosition; } set { _mRacePosition = value; NotifyPropertyChanged("mRacePosition"); } }
		public uint mLapsCompleted { get { return _mLapsCompleted; } set { _mLapsCompleted = value; NotifyPropertyChanged("mLapsCompleted"); } }
		public uint mCurrentLap { get { return _mCurrentLap; } set { _mCurrentLap = value; NotifyPropertyChanged("mCurrentLap"); } }
		public int mCurrentSector { get { return _mCurrentSector; } set { _mCurrentSector = value; NotifyPropertyChanged("mCurrentSector"); } }

		public float mCurrentSector1Time { get { return _mCurrentSector1Time; } set { _mCurrentSector1Time = value; NotifyPropertyChanged("mCurrentSector1Time"); } }
		public float mCurrentSector2Time { get { return _mCurrentSector2Time; } set { _mCurrentSector2Time = value; NotifyPropertyChanged("mCurrentSector2Time"); } }
		public float mCurrentSector3Time { get { return _mCurrentSector3Time; } set { _mCurrentSector3Time = value; NotifyPropertyChanged("mCurrentSector3Time"); } }
		public float mFastestSector1Time { get { return _mFastestSector1Time; } set { _mFastestSector1Time = value; NotifyPropertyChanged("mFastestSector1Time"); } }
		public float mFastestSector2Time { get { return _mFastestSector2Time; } set { _mFastestSector2Time = value; NotifyPropertyChanged("mFastestSector2Time"); } }
		public float mFastestSector3Time { get { return _mFastestSector3Time; } set { _mFastestSector3Time = value; NotifyPropertyChanged("mFastestSector3Time"); } }
		public float mFastestLapTime { get { return _mFastestLapTime; } set { _mFastestLapTime = value; NotifyPropertyChanged("mFastestLapTime"); } }
		public float mLastLapTime { get { return _mLastLapTime; } set { _mLastLapTime = value; NotifyPropertyChanged("mLastLapTime"); } }
		public bool mLapInvalidated { get { return _mLapInvalidated; } set { _mLapInvalidated = value; NotifyPropertyChanged("mLapInvalidated"); } }
		public uint mRaceState { get { return _mRaceState; } set { _mRaceState = value; NotifyPropertyChanged("mRaceState"); } }
		public uint mPitMode { get { return _mPitMode; } set { _mPitMode = value; NotifyPropertyChanged("mPitMode"); } }
		public float mSpeed { get { return _mSpeed; } set { _mSpeed = value; NotifyPropertyChanged("mSpeed"); } }




		private string _cColor;
		public string cColor { get { return _cColor; } set { _cColor = value; NotifyPropertyChanged("cColor"); } }
		public ParticipantInfo(uint i)
		{
			mWorldPosition = new ObservableCollection<float>();
			mWorldPosition.Add(0);
			mWorldPosition.Add(0);
			mWorldPosition.Add(0);
			mRacePosition = i;
			cColor = "Red";
		}
	}
	class MemObj : BaseViewModel
	{
		//static int STORED_PARTICIPANTS_MAX = 64;    

		private uint _mVersion;
		private uint _mBuilderVersionNumber;

		private uint _GameState;
		private uint _SessionState;
		private uint _RaceState;

		private int _mViewedParticipantIndex;
		private int _mNumParticipants;
		private ObservableCollection<ParticipantInfo> _mParticipantInfo;

		public uint mVersion { get { return _mVersion; } set { _mVersion = value; NotifyPropertyChanged("mVersion"); } }
		public uint mBuilderVersionNumber { get { return _mBuilderVersionNumber; } set { _mBuilderVersionNumber = value; NotifyPropertyChanged("mBuilderVersionNumber"); } }

		public uint GameState { get { return _GameState; } set { _GameState = value; NotifyPropertyChanged("GameState"); } }
		public uint SessionState { get { return _SessionState; } set { _SessionState = value; NotifyPropertyChanged("SessionState"); } }
		public uint RaceState { get { return _RaceState; } set { _RaceState = value; NotifyPropertyChanged("RaceState"); } }

		public int mViewedParticipantIndex { get { return _mViewedParticipantIndex; } set { _mViewedParticipantIndex = value; NotifyPropertyChanged("mViewedParticipantIndex"); } }
		public int mNumParticipants
		{
			get { return _mNumParticipants; }
			set
			{
				for (int i = mNumParticipants; value > i; i++)
				{
					App.Current.Dispatcher.Invoke((Action)delegate
					{
						mParticipantInfo.Add(new ParticipantInfo((uint)i + 1));
					});
				}
				_mNumParticipants = value; NotifyPropertyChanged("mNumParticipants");
			}
		}
		public ObservableCollection<ParticipantInfo> mParticipantInfo  { get { return _mParticipantInfo; } set { _mParticipantInfo = value; /*NotifyPropertyChanged("mParticipantInfo");*/  /*Debug.WriteLine($"patrinfo CHANGED");*/ } }
		public MemObj(int nParticipants = 0)
		{
			mParticipantInfo = new ObservableCollection<ParticipantInfo>();
			mNumParticipants = nParticipants;
		}
	}
}
