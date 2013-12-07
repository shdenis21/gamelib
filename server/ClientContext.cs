using System;
using gamelib.Common;
namespace gamelib.Server
{
	/// <summary>
	/// Client context.
	/// </summary>
	public class ClientContext
	{
		
		public ClientContext(Connection connection) { this.Connection = connection; }

        public Connection Connection { get; set; }
	}
	
	
	/// <summary>
	/// Client context factory.
	/// </summary>
	public class ClientContextFactory : ContextFactory
    {
		
		
        public object MakeContext(Connection connection)
        {
            return new ClientContext(connection);
        }
    }
	/// <summary>
	/// Context factory.
	/// </summary>
	interface ContextFactory
	{
		object MakeContext(Connection connection);
	}
	
	
}

