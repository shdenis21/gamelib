using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using gamelib.Common;
namespace gamelib.Server
{/// <summary>
/// Server.
/// </summary>
	public class Server
	{/// <summary>
	/// Initializes a new instance of the <see cref="gamelib.Server.Server"/> class.
	/// </summary>
	/// <param name='port'>
	/// Port.
	/// </param>
	/// <param name='contextFactory'>
	/// Context factory.
	/// </param>
	/// <param name='Handlers'>
	/// Handlers.
	/// </param>
	/// <param name='auth'>
	/// Auth.
	/// </param>
		public Server(int port, ClientContextFactory contextFactory, IAuthen _auth, AfterConnected afterconn)
        {
			this._aftercon = afterconn;
			this._auth = _auth;
            this.Port = port;
            this.Started = false;
            this._contextFactory = contextFactory;
            this._connectios = new List<Connection>();
        }
		private AfterConnected _aftercon;
		private IAuthen _auth;
        private Thread _newThread;
        private TcpListener _listner;
		
        public List<Connection> _connectios; //Список соединений
        public int Port { get; set; }
        public bool Started { get; private set; }
        public PacketHandlerStorage Handlers { get; set; } //Хранилище обработчиков
        private ContextFactory _contextFactory { get; set; }

        private void _worker()
        {
            this._listner = new TcpListener(IPAddress.Any, this.Port);
            this._listner.Start();
            this.Started = true;

            while (this.Started)
            {
                TcpClient client = this._listner.AcceptTcpClient();
			
                Connection connection = new Connection(client, this.Handlers, this._auth, this._aftercon);
                connection.Context = this._contextFactory.MakeContext(connection);
            this._connectios.Add(connection);
			int SesionId = 	this._connectios.IndexOf(connection);
                connection.RunServerProtocolClient(SesionId);
            }
        }

        public void Run()
        {
            this._newThread = new Thread(this._worker);
            this._newThread.Start();
        }
		
		
	}
}

