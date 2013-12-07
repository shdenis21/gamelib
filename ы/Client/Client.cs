using System;
using System.Net.Sockets;
using gamelib.Common;
using System.IO;
namespace gamelib.Client
{
	public class Client
	{
		public Client(string ip, int port, PacketHandlerStorage handlers, IAuthen auth) 
        {
			bool s = false;
            this._tcpClient = new TcpClient(ip, port); 

            this._connection = new Connection(this._tcpClient, handlers, auth);
            this._connection.Context = this;
            this._connection.Run();
			
        }
		
        private TcpClient _tcpClient;
        private Connection _connection;
	}
}

