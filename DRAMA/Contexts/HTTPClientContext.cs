namespace DRAMA.Contexts;

public static class HTTPClientContext
{
    /// <summary>
    ///     Addresses the potential of socket exhaustion by sharing connections across HttpClient instances.
    ///     <br/>
    ///     The SocketsHttpHandler cycles connections according to PooledConnectionLifetime to avoid stale DNS problems.
    ///     <br/>
    ///     An HttpMessageHandler is to be used when initialising all HttpClient instances in this framework, according to the following example:
    ///     
    ///     <code>
    ///         public static HttpMessageHandler HTTPHandler { get; set; } = new SocketsHttpHandler() { PooledConnectionLifetime = Timeout.InfiniteTimeSpan };
    ///         
    ///         for (int iterator = 0; iterator &lt; int.MaxValue; iterator++)
    ///             await new HttpClient(HTTPHandler, disposeHandler: false).GetAsync(endpoint);
    ///     </code>
    /// 
    ///     More information can be found at <a href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#httpclient-and-lifetime-management"></a>.
    /// </summary>
    public static HttpMessageHandler HTTPHandler { get; set; } = new SocketsHttpHandler() { PooledConnectionLifetime = Timeout.InfiniteTimeSpan };
}
