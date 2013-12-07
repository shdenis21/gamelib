using System;
using gamelib.Common;
using System.IO;
namespace gamelib.Common
{
	public interface AfterConnected
	{
		void Run(BinaryReader reader, BinaryWriter writer);
		
		
	}
}

