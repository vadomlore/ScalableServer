namespace ScalableServer
{
    using Authenticate;
    using Com.Virtuos.Rocket.NetworkMessage;
    using DotNetty.Transport.Channels;
    using System;

    public class RocketServerlHandler : SimpleChannelInboundHandler<Packet>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, Packet packet)
        {
            var msg = packet.Payload;
            if (msg.RequestNumber == PingRequest.RequestNumberFieldNumber)
            {
                var request = msg.GetExtension<PingRequest>(PingRequest.RequestNumber);
                Console.WriteLine($"Receive client request ping: {request.Timestamp}");
                ctx.WriteAndFlushAsync(packet);
            }
            else if(msg.RequestNumber == ActionCommitedRequest.RequestNumberFieldNumber)
            {
                var request = msg.GetExtension<ActionCommitedRequest>(ActionCommitedRequest.RequestNumber);
                Console.WriteLine($"Receive {packet.Payload.GetType().Name}: GameId:{request.GameId} TurnIndex: {request.TurnIndex}");                
                packet = AddGameTurn(request);
                ctx.WriteAndFlushAsync(packet);
            }
            else if(msg.RequestNumber == AsyncAuthRequest.RequestNumberFieldNumber)
            {
                new AuthenticateRequestHandler(ctx, msg.GetExtension<AsyncAuthRequest>(AsyncAuthRequest.RequestNumber)).ProcessAuthenciate();
            }
        }

        Packet AddGameTurn(ActionCommitedRequest request)
        {
            var actionCommitRequest = ActionCommitedRequest.
                CreateBuilder().
                SetGameId(request.GameId).
                SetTurnIndex(request.TurnIndex + 1).
                Build();
            var messageBuilder = Message.CreateBuilder();
            messageBuilder.SetRequestNumber(ActionCommitedRequest.RequestNumberFieldNumber);
            messageBuilder.SetExtension<ActionCommitedRequest>(ActionCommitedRequest.RequestNumber, actionCommitRequest);
            var msg = messageBuilder.Build();
            var packet = Packet.CreateBuilder();
            packet.Id = new Random().Next(0, int.MaxValue);
            packet.Payload = msg;
            return packet.Build();
        }
    }
}
