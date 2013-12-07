using System;
using gamelib.Common;
namespace gamelib.Server
{
	/// <summary>
	/// Client context.
	/// </summary>
	public class ClientContext
	{
		
		public ClientContext(Connect connection) { this.Connection = connection; }

        public Connect Connection { get; set; }
	}
	
	
	/// <summary>
	/// Client context factory.
	/// </summary>
	public class ClientContextFactory : ContextFactory
    {
		
		
        public object MakeContext(Connect connection)
        {
            return new ClientContext(connection);
        }
    }
	/// <summary>
	/// Context factory.
	/// </summary>
	interface ContextFactory
	{
		object MakeContext(Connect connection);
	}
	
	
}

