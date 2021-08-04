using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;

namespace read_mem_file
{
    class Program
    {
        //private static  byte[] sizeBuffer = new byte[20];
        //private static byte[] cur = new byte[] { };
        //private static byte[] old = new byte[] { };
        //private static MemoryMappedFile mmf;
        //private static  MemoryMappedViewStream stream;
        //private static void ReadCallBack(IAsyncResult ar)
        //{
        //    int n = stream.EndRead(ar);
        //    Console.WriteLine($"READ BYTES LENGHT: {n}");
        //    if (n != 20 )
        //        throw new InvalidOperationException("Invalid message header.");
        //    fetch_next();
        //}
        //private static void fetch_next()
        //{
        //    stream.BeginRead(sizeBuffer, 0, 20, new AsyncCallback(ReadCallBack), null);
        //}
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //try
            //{
            //    mmf = MemoryMappedFile.OpenExisting("$pcars2$");
            //    stream = mmf.CreateViewStream();
            //    BinaryReader reader = new BinaryReader(stream);
            //    while (true)
            //    {
            //        cur = reader.ReadBytes(20);
            //        Console.WriteLine(cur.Length);
            //        for (int i = 0; i < cur.Length; i++)
            //        {
            //            if (i % 4 == 0)
            //                Console.WriteLine();
            //            Console.Write($"{cur[i]}.");
            //        }
            //        System.Threading.Thread.Sleep(1000);
            //        Console.Clear();
            //        reader.BaseStream.Position = 0;
            //    }
            //}
            //catch (Exception err)
            //{
            //    Console.WriteLine($"CATCHED ERR: {err}");
            //}
            MemReader myReader = new MemReader("$pcars2$");
            myReader.SetupStreamReading();
            myReader.ReadStream();
            while (true)
            { }
        }
    }
}
