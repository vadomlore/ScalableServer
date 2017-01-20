namespace ScalableServer.Authenticate
{
    using Com.Virtuos.Rocket.NetworkMessage;
    using CommonProtocols;
    using DotNetty.Transport.Channels;

    public class AuthenticateRequestHandler
    {
        public bool Authenticated { get; set; }
        IChannelHandlerContext ctx;
        AsyncAuthRequest request;
        public AuthenticateRequestHandler(IChannelHandlerContext ctx, AsyncAuthRequest request)
        {
            this.ctx = ctx;
            this.request = request;
        }

        public virtual void ProcessAuthenciate()
        {            
            var response = AsyncConnectionErrorRequest.CreateBuilder().SetCode(AsyncConnectionErrorRequest.Types.ConnectionError.SERVER_ERROR).Build();
            var packet = ProtocolWrapper.WrapMessage<AsyncConnectionErrorRequest>(AsyncConnectionErrorRequest.RequestNumberFieldNumber, AsyncConnectionErrorRequest.RequestNumber, response);
            ctx.WriteAndFlushAsync(packet);
        }
    }
}
