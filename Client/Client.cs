using System;
using System.Net.Sockets;
using gamelib.Common;
using System.IO;
using System.Collections.Generic;
namespace gamelib.Client
{
	public class Client
	{
		public Client(string ip, int port, PacketHandlerStorage handlers, IAuthen auth,AfterConnected conn, List<Connection.prefabid> RemotesPrototypes) 
        {
            this._tcpClient = new TcpClient(ip, port); 
			
            this._connection = new Connection(this._tcpClient, handlers, auth, conn);
            this._connection.Context = this;
            this._connection.RunClient(RemotesPrototypes);
			
        }
		
        private TcpClient _tcpClient;
        private Connection _connection;
	}
}

