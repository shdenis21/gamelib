using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
namespace gamelib.Common
{
	public class OutputProccessor
	{
		public OutputProccessor(NetworkStream stream)
        {
            this._stream = stream;
            _writer = new BinaryWriter(this._stream);
            this.Packets = new Queue();
            this._lock = new ManualResetEvent(true);
        }

        private Thread _newThread;
        private NetworkStream _stream;
        private BinaryWriter _writer;
        private Queue Packets;
        private ManualResetEvent _lock;

        private void _worker()
        {
            while (true)
            {
                this._lock.WaitOne();

                if (this.Packets.Count > 0) //Если в очереди пакетов больше нуля
					this._writer.Write(this.Packets.Contains(this.Packets.Dequeue()));
                else
                    this._lock.Reset();
            }
        }

        public void Send(PacketBase packet) //Метод отправки пакета
        {
            this.Packets.Enqueue(packet);
            this._lock.Set();
        }

        public void Run()
        {
            this._newThread = new Thread(this._worker);
            this._newThread.Start();
        }
	}
}

