using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace projectWpf.Sources.pages.streamMem
{
	class MemReader : BaseViewModel
	{
		//private float xMin = 0, xMax = 0, yMin = 0, yMax = 0, zMin = 0, zMax = 0;
		//public float XMin { get { return xMin; } set { xMin = value; NotifyPropertyChanged("XMin"); } }
		//public float XMax { get { return xMax; } set { xMax = value; NotifyPropertyChanged("XMax"); } }
		//public float YMin { get { return yMin; } set { yMin = value; NotifyPropertyChanged("YMin"); } }
		//public float YMax { get { return yMax; } set { yMax = value; NotifyPropertyChanged("YMax"); } }
		//public float ZMin { get { return zMin; } set { zMin = value; NotifyPropertyChanged("ZMin"); } }
		//public float ZMax { get { return zMax; } set { zMax = value; NotifyPropertyChanged("ZMax"); } }

		private static string _path = Directory.GetCurrentDirectory() + "\\Resources\\lemans_layout3_4.jpg";
		private ImageSource _BGImage = new BitmapImage(new Uri(_path, UriKind.Absolute));

		public ImageSource BGImage
		{
			get { return _BGImage; }
			set
			{
				_BGImage = value;
				NotifyPropertyChanged("BGImage");
			}
		}

		private MemObj _memBlock;
		public MemObj memBlock { get { return _memBlock; } set { _memBlock = value; } }

		private int count = 0;
		public int Count
		{
			get { return count; }
			set
			{
				count = value;
				NotifyPropertyChanged("Count");
			}
		}
		static int READ_LENGHT = 7000;
		private byte[] _sizeBuffer;
		private byte[] _lastBuffer;
		private string _fileName;
		private MemoryMappedFile _mmf;
		private MemoryMappedViewStream _stream;
		private Task _nTask;
		private Stopwatch _stopWatch;

		public MemReader(string fileName)
		{
			_fileName = fileName;
			memBlock = new MemObj(0);
		}
		public bool SetupStreamReading()
		{
			//Debug.WriteLine(Directory.GetCurrentDirectory());
			try
			{
				_mmf = MemoryMappedFile.OpenExisting("$pcars2$");
				_stream = _mmf.CreateViewStream();
				READ_LENGHT = (int)_stream.Length;
				_sizeBuffer = new byte[READ_LENGHT];
				_lastBuffer = new byte[READ_LENGHT];
				_stopWatch = new Stopwatch();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public void ReadStream()
		{
			_stopWatch.Restart();
			_stream.Position = 0;
			_nTask = _stream.ReadAsync(_sizeBuffer, 0, READ_LENGHT);
			_ = _nTask.ContinueWith(args => StreamReadContinueWith());
		}
		private void StreamReadContinueWith()
		{
			Count++;
			if (!_sizeBuffer.SequenceEqual(_lastBuffer))
			{
				_lastBuffer = _sizeBuffer.ToArray();
				MemObjfromBuffer();
			}
			_stopWatch.Stop();
			if (_stopWatch.ElapsedMilliseconds < 16)
				Thread.Sleep((int)(16 - _stopWatch.ElapsedMilliseconds));
			this.ReadStream();
		}

		private void readToParticipantInfo(int i, int start)
		{			
			memBlock.mParticipantInfo[i].mIsActive = BitConverter.ToBoolean(_sizeBuffer, start);
			string tmp = Encoding.ASCII.GetString(_sizeBuffer, start + 1, 64);
			memBlock.mParticipantInfo[i].mName = tmp.Substring(0, tmp.IndexOf('\0'));
			for (int j = 0; j < 3; j++)
			{
				float point = 0;
				if (j == 0)
					point = (BitConverter.ToSingle(_sizeBuffer, start + 68 + j * 4) + 776) / (float)7.91 + 49;
				else
					point = (BitConverter.ToSingle(_sizeBuffer, start + 68 + j * 4) + 2902) / (float)8.22 + 23;
				memBlock.mParticipantInfo[i].mWorldPosition[j] = point;
			}
			memBlock.mParticipantInfo[i].mCurrentLapDistance = (float)BitConverter.ToSingle(_sizeBuffer, start + 80);
			memBlock.mParticipantInfo[i].mRacePosition = BitConverter.ToUInt32(_sizeBuffer, start + 84);
			memBlock.mParticipantInfo[i].mLapsCompleted = BitConverter.ToUInt32(_sizeBuffer, start + 88);
			memBlock.mParticipantInfo[i].mCurrentLap = BitConverter.ToUInt32(_sizeBuffer, start + 92);
			memBlock.mParticipantInfo[i].mCurrentSector = BitConverter.ToInt32(_sizeBuffer, start + 96);

		}

		private void MemObjfromBuffer()
		{
			memBlock.mVersion = BitConverter.ToUInt32(_sizeBuffer, 0);
			memBlock.mBuilderVersionNumber = BitConverter.ToUInt32(_sizeBuffer, 4);

			memBlock.GameState = BitConverter.ToUInt32(_sizeBuffer, 8);
			memBlock.SessionState = BitConverter.ToUInt32(_sizeBuffer, 12);
			memBlock.RaceState = BitConverter.ToUInt32(_sizeBuffer, 16);
			memBlock.mViewedParticipantIndex = BitConverter.ToInt32(_sizeBuffer, 20);
			memBlock.mNumParticipants = BitConverter.ToInt32(_sizeBuffer, 24);
			for (int i = 0; i < memBlock.mNumParticipants; i++)
			{
				readToParticipantInfo(i, 28 + i * 100);
			}
			for (int i = 0; i < memBlock.mNumParticipants; i++)
			{
				memBlock.mParticipantInfo[i].mCurrentSector1Time = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 7408 + i * 4), 3);
				memBlock.mParticipantInfo[i].mCurrentSector2Time = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 7664 + i * 4), 3);
				memBlock.mParticipantInfo[i].mCurrentSector3Time = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 7920 + i * 4), 3);
				memBlock.mParticipantInfo[i].mFastestSector1Time = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 8176 + i * 4), 3);
				memBlock.mParticipantInfo[i].mFastestSector2Time = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 8432 + i * 4), 3);
				memBlock.mParticipantInfo[i].mFastestSector3Time = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 8688 + i * 4), 3);
				memBlock.mParticipantInfo[i].mFastestLapTime = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 8944 + i * 4), 3);
				memBlock.mParticipantInfo[i].mLastLapTime = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 9200 + i * 4), 3);
				memBlock.mParticipantInfo[i].mLapInvalidated = BitConverter.ToBoolean(_sizeBuffer, 9456 + i * 4);
				memBlock.mParticipantInfo[i].mRaceState = BitConverter.ToUInt32(_sizeBuffer, 9520 + i * 4);
				memBlock.mParticipantInfo[i].mPitMode = BitConverter.ToUInt32(_sizeBuffer, 9776 + i * 4);
				memBlock.mParticipantInfo[i].mSpeed = (float)Math.Round(BitConverter.ToSingle(_sizeBuffer, 10800 + i * 4) * (float)3.6, 0);
				string tmp = Encoding.Default.GetString(_sizeBuffer, 15152 + i * 64, 64);
				//memBlock.mParticipantInfo[i].mCarClass = Encoding.Default.GetString(_sizeBuffer, 15152 + i * 64, 64);
				memBlock.mParticipantInfo[i].mCarClass = tmp.Substring(0, tmp.IndexOf('\0'));
				//Debug.WriteLine(BitConverter.ToString(_sizeBuffer, 11056 + 4096 + i * 64, 64));
			}
			//Debug.WriteLine(BitConverter.ToString(_sizeBuffer, 7398, 20));
		}
	}
}
