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

namespace read_mem_file
{
	class MemObj
	{
		uint mVersion;
		uint mBuilderVersionNumber;

		uint GameState;
		uint SessionState;
		uint RaceState;
	}
	class MemReader
	{
		public MemObj memBlock;
		public int count = 0;
		public int Count { get { return count; } }

		//static int READ_TIMEOUT = 1000;
		static int READ_LENGHT = 400;
		private byte[] _sizeBuffer;
		private byte[] _lastBuffer;
		private string _fileName;
		private MemoryMappedFile _mmf;
		private MemoryMappedViewStream _stream;
		//private BinaryReader _reader;
		private Task _nTask;
		private Stopwatch _stopWatch;

		System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

		public MemReader(string fileName)
		{
			_sizeBuffer = new byte[READ_LENGHT];
			_lastBuffer = new byte[READ_LENGHT];
			_fileName = fileName;
			memBlock = new MemObj();
		}
		public bool SetupStreamReading()
		{
			try
			{
				_mmf = MemoryMappedFile.OpenExisting("$pcars2$");
				_stream = _mmf.CreateViewStream();
				//_stream.ReadTimeout = READ_TIMEOUT; // timeouts are not supported on pc2 streams?
				//_reader = new BinaryReader(_stream);
				_stopWatch = new Stopwatch();
				return true;
			}
			catch (Exception err)
			{
				Console.WriteLine($"OpenStream CATCHED ERR: {err}");
				return false;
			}
		}
		public void ReadStream()
		{
			_stopWatch.Restart();
			_nTask = _stream.ReadAsync(_sizeBuffer, 0, READ_LENGHT);
			_ = _nTask.ContinueWith(args => StreamReadContinueWith());
			_stream.Position = 0;
		}
		private void StreamReadContinueWith()
		{
			Console.Clear();
			count++;
			_stopWatch.Stop();
			if (!_sizeBuffer.SequenceEqual(_lastBuffer))
			{
				Console.WriteLine($"DIFFERENT {count} {_stopWatch.ElapsedMilliseconds}");
				printB(_lastBuffer);
				printB(_sizeBuffer);
				_lastBuffer = _sizeBuffer.ToArray(); ;
				Thread.Sleep(5000);
			}
			else
			{
				Console.WriteLine($"SAME {count} {_stopWatch.ElapsedMilliseconds}");
				Thread.Sleep(500);
			}
			this.ReadStream();
		}
		private void printB(byte[] arr)
		{
			foreach (byte b in arr)
			{
				Console.Write($"{b} ");
			}
			Console.WriteLine();
		}
	}
}
