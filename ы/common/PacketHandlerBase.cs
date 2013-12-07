using System;
using System.IO;
namespace gamelib.Common
{
	
	public abstract class PacketHandlerBase : IPacketHandler
    {
        public PacketHandlerBase() { }

        public BinaryReader Reader { get; set; }
        public object Context { get; set; }

        public virtual void Read() { } //Метод чтения
        public virtual void Handle() { } //Метод обработки
        public abstract Object Clone(); //Метод, возвращающий клона обработчика
    }
}

