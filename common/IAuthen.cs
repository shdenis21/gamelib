using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using gamelib.Common;
//интерфейс проверки логина и пароля
namespace gamelib.Common
{
	public interface IAuthen
	{
		 bool _Authen(BinaryReader reader, BinaryWriter writer);
	}
}

