using System;
using System.Net;
using System.IO;
namespace gamelib.Common
{
	public interface IPacket
	{
		
		void Write(BinaryWriter writer); //Метод записи пакета
	}
	
	interface IPacketHandler : ICloneable
    {
        void Read(); //Метод чтения
        void Handle(); //Метод обработки
    }
}

