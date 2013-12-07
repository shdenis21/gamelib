using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
namespace gamelib.Common
{
	/// <summary>
	/// Connection.
	/// </summary>
	public class Connection
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="gamelib.Common.Connection"/> class.
		/// </summary>
		/// <param name='client'>
		/// Client.
		/// </param>
		/// <param name='handlers'>
		/// Handlers.
		/// </param>
		public Connection(TcpClient client, PacketHandlerStorage handlers, IAuthen auth, AfterConnected connected)
        {
			this._aftercon = connected;
			this._auth = auth;
            this._client = client;
            this.Stream = this._client.GetStream();
            this._inputProccessor = new InputProcessor(this.Stream, this, handlers);
            this._outputProccessor = new OutputProccessor(this.Stream);
			this._reader = new BinaryReader(this.Stream);
			this._writer = new BinaryWriter(this.Stream);
        }
	 	private	BinaryReader _reader;
		private BinaryWriter _writer;
		private IAuthen _auth;
		private AfterConnected _aftercon;
        private TcpClient _client;
        private InputProcessor _inputProccessor; //Объект класса чтения/обработки пакетов
        private OutputProccessor _outputProccessor; //Объект класса записи пакетов
        public NetworkStream Stream { get; private set; }
        public object Context { get; set; }
		/// <summary>
		/// Run this instance.
		/// </summary>
        public void Run()
        {
			// авторизация
			if(this._auth != null)
			{ 
					while(s==false) 
					{ if(this._auth._Authen(reader,writer) == true)
						{s= true;} 
						else s= false;
					}
			}
			//перед запуском обработчиков
		this._aftercon.Run(_reader,_writer);
			// запуск потоков 
            this._inputProccessor.Run();
            this._outputProccessor.Run();
        }
		/// <summary>
		/// Send the specified packet.
		/// </summary>
		/// <param name='packet'>
		/// Packet.
		/// </param>
        public void Send(PacketBase packet)
        {
            this._outputProccessor.Send(packet);
        }
		/// <summary>
		/// Receive the specified handler.
		/// </summary>
		/// <param name='handler'>
		/// Handler.
		/// </param>
        public void Receive(PacketHandlerBase handler)
        {
            handler.Context = this.Context;
            handler.Handle();
        }
	}
}

