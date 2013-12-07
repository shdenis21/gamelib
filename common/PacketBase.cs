using System;
using System.Net;
using System.IO;
namespace gamelib.Common
{
	public	abstract class PacketBase : IPacket
	{
		protected PacketBase(int id) { this.Id = id; }

        public int Id { get; private set; }

        protected void WriteHeader(BinaryWriter writer) { writer.Write(this.Id); } //Метод записи идентификатора пакет
        protected virtual void WriteBody(BinaryWriter writer) { } //Метод записи содержимого пакета

        public void Write(BinaryWriter writer) { this.WriteHeader(writer); this.WriteBody(writer); } //Общий метод записи пакета
	}
}

