using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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
			//проверка кто сервер а кто клиент 
			if(handlers != null)
			{
				
            this._outputProccessor = new OutputProccessor(this.Stream);
            this._inputProccessor = new InputProcessor(this.Stream, this, handlers);
			}
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
		bool s = false;
		public int SesionClient;
		/// <summary>
		/// Runs the server protocol client.
		/// </summary>
		/// <param name='SesionId'>
		/// Sesion identifier.
		/// </param>
        public void RunServerProtocolClient(int SesionId)
        {bool s = false;
			// авторизация
			if(this._auth != null)
			{ 
					while(s==false) 
					{ if(this._auth._Authen(this._reader,this._writer) == true)
						{
							s= true;
							_writer.Write(SesionId);
						} 
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
		/// Runs the client.
		/// </summary>
		//запуск клиента
	 public void RunClient(List<prefabid> _remotesprototypes)
        { 
			// авторизация
			if(this._auth != null)
			{ 
					while(s==false) 
					{ if(this._auth._Authen(this._reader,this._writer) == true)
						{
							s= true;
							SesionClient = this._reader.ReadInt32();
						} 
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
			
	
	
		public class prefabid
		{
			public GameObject  PrefabOwner;
		}
	}
}

