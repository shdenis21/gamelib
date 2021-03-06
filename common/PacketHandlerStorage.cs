using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
namespace gamelib.Common
{
	public class PacketHandlerStorage
	{
		public PacketHandlerStorage() { this._storage = new Dictionary<int, PacketHandlerBase>(); }

        public Dictionary<int, PacketHandlerBase> _storage;
        public PacketHandlerBase GetHandlerById(int id)
        {
            PacketHandlerBase x = this._storage[id];
            return (PacketHandlerBase)x.Clone(); //Вот тут то и пригодился метод Clone
        }
		
        public void AddHandler(int idhandler, PacketHandlerBase handler)
        {
            this._storage.Add(idhandler, handler);
        }
	}
}

