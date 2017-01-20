
namespace ScalableClient
{
    using System;
    using DotNetty.Transport.Channels;
    using Com.Virtuos.Rocket.NetworkMessage;
    using Google.ProtocolBuffers;
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
                SendActionCommitRequest();
            }
            else if(msg.RequestNumber == ActionCommitedRequest.RequestNumberFieldNumber)
            {
                var response = msg.GetExtension<ActionCommitedRequest>(ActionCommitedRequest.RequestNumber);
                Console.WriteLine($"Receive server response Submit GameTurn: [GameId: {response.GameId} TurnIndex: {response.TurnIndex}]");
            }
        }


        void SendPing()
        {            
            var pingRequest = PingRequest.CreateBuilder().SetTimestamp(UnixNow).Build();
            var messageBuilder= Message.CreateBuilder();
            messageBuilder.SetRequestNumber(PingRequest.RequestNumberFieldNumber);
            messageBuilder.SetExtension<PingRequest>(PingRequest.RequestNumber, pingRequest);
            var msg = messageBuilder.Build();
            var packet = Packet.CreateBuilder();
            packet.Id = new Random().Next(0, int.MaxValue);
            packet.Payload = msg;
            this.ctx.WriteAndFlushAsync(packet.Build());
        }

        void SendActionCommitRequest()
        {
            var actionCommitRequest = ActionCommitedRequest.
                CreateBuilder().
                SetGameId(1).
                SetTurnIndex(1).
                Build();
            var messageBuilder = Message.CreateBuilder();
            messageBuilder.SetRequestNumber(ActionCommitedRequest.RequestNumberFieldNumber);
            messageBuilder.SetExtension<ActionCommitedRequest>(ActionCommitedRequest.RequestNumber, actionCommitRequest);
            var msg = messageBuilder.Build();
            var packet = Packet.CreateBuilder();
            packet.Id = new Random().Next(0, int.MaxValue);
            packet.Payload = msg;
            this.ctx.WriteAndFlushAsync(packet.Build());
            Console.WriteLine($"Submit GameTurn: [GameId: {actionCommitRequest.GameId} TurnIndex: {actionCommitRequest.TurnIndex}]");
        }
    }
}
