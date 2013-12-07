using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
namespace gamelib.Common
{
	public class InputProcessor
	{
		public InputProcessor(NetworkStream stream, Connection connection, PacketHandlerStorage handlers)
        {
            this._connection = connection;
            this._stream = stream;
            this.Handlers = handlers;
            Reader = new BinaryReader(this._stream);
            this._started = false;
        }

        private NetworkStream _stream;
        private Connection _connection; //Объект класса соединения
        private Thread _newThread;
        private BinaryReader Reader;
        private bool _started;

        public PacketHandlerStorage Handlers { get; set; }

        private void _handlePacket()
        {
			
            int id = Reader.ReadInt32(); //Читаем id пакета
            PacketHandlerBase handler = this.Handlers.GetHandlerById(id); //Получаем обработчик
            handler.Reader = this.Reader;
            handler.Read(); //Вызываем чтение
            this._connection.Receive(handler); //Вызываем обработку
        }

        private void _worker()
        {
            while (!this._started)
            {
                _handlePacket();
            }
        }

        public void Run()
        {
            this._newThread = new Thread(this._worker);
            this._newThread.Start();
        }
	}
}

