
namespace ScalableClient
{
    using System;
    using DotNetty.Transport.Channels;
    using Com.Virtuos.Rocket.NetworkMessage;
    using Google.ProtocolBuffers;
    using CommonProtocols;

    public class RocketClientHandler : SimpleChannelInboundHandler<Packet>
    {

        IChannelHandlerContext ctx;

        public int UnixNow
        {
            get
            {
                return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            }
        }

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            this.ctx = ctx;
            this.SendPing();
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, Packet packet)
        {
            var msg = packet.Payload;
            if(msg.RequestNumber == PingRequest.RequestNumberFieldNumber)
            {
                var response = msg.GetExtension<PingRequest>(PingRequest.RequestNumber);
                Console.WriteLine($"Receive server response pong:" + response.Timestamp);
                SendUserPasswordAuthenticateRequest();
            }
            else if(msg.RequestNumber == ActionCommitedRequest.RequestNumberFieldNumber)
            {
                var response = msg.GetExtension<ActionCommitedRequest>(ActionCommitedRequest.RequestNumber);
                Console.WriteLine($"Receive server response Submit GameTurn: [GameId: {response.GameId} TurnIndex: {response.TurnIndex}]");

            }
            else if (msg.RequestNumber == AsyncConnectionErrorRequest.RequestNumberFieldNumber)
            {
                var response = msg.GetExtension<AsyncConnectionErrorRequest>(AsyncConnectionErrorRequest.RequestNumber);
                Console.WriteLine($"Receive server response [ErrorCode: {response.Code}");
                SendActionCommitRequest();
            }
        }


        void SendPing()
        {            
            var pingRequest = PingRequest.CreateBuilder().SetTimestamp(UnixNow).Build();
            var packet = ProtocolWrapper.WrapMessage<PingRequest>(PingRequest.RequestNumberFieldNumber, PingRequest.RequestNumber, pingRequest);
            this.ctx.WriteAndFlushAsync(packet);
        }

        void SendActionCommitRequest()
        {
            var actionCommitRequest = ActionCommitedRequest.
                CreateBuilder().
                SetGameId(1).
                SetTurnIndex(1).
                Build();
            var packet = ProtocolWrapper.WrapMessage<ActionCommitedRequest>(ActionCommitedRequest.RequestNumberFieldNumber, ActionCommitedRequest.RequestNumber, actionCommitRequest);
            this.ctx.WriteAndFlushAsync(packet);
            Console.WriteLine($"Submit GameTurn: [GameId: {actionCommitRequest.GameId} TurnIndex: {actionCommitRequest.TurnIndex}]");
        }


        void SendUserPasswordAuthenticateRequest()
        {
            var authenticateRequest = AsyncAuthRequest.
                CreateBuilder().
                SetName("alice").
                SetPass("a123456").
                Build();
            var packet = ProtocolWrapper.WrapMessage<AsyncAuthRequest>(AsyncAuthRequest.RequestNumberFieldNumber, AsyncAuthRequest.RequestNumber, authenticateRequest);
            this.ctx.WriteAndFlushAsync(packet);
        }

    }
}
