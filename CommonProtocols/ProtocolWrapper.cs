using Com.Virtuos.Rocket.NetworkMessage;
using Google.ProtocolBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonProtocols
{
    public static class ProtocolWrapper
    {
        public static Packet WrapMessage<T>(int requestNumberFieldNumber, GeneratedExtensionBase<T> requestNumber, T value)
        {
            var messageBuilder = Message.CreateBuilder();
            messageBuilder.SetRequestNumber(requestNumberFieldNumber);
            messageBuilder.SetExtension<T>(requestNumber, value);
            var msg = messageBuilder.Build();
            var packet = Packet.CreateBuilder();
            packet.Id = new Random().Next(0, int.MaxValue);
            packet.Payload = msg;
            return packet.Build();
        }
    }
}
