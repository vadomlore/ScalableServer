namespace ScalableClient
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using DotNetty.Codecs;
    using DotNetty.Handlers.Tls;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using Examples.Common;
    using DotNetty.Codecs.ProtocolBuffers;
    using Com.Virtuos.Rocket.NetworkMessage;
    using Google.ProtocolBuffers;
    using DotNetty.Codecs.Protobuf;

    class Program
    {
        static async Task RunClientAsync()
        {
            ExampleHelper.SetConsoleLogger();

            var group = new MultithreadEventLoopGroup();

            X509Certificate2 cert = null;
            string targetHost = null;
            if (ClientSettings.IsSsl)
            {
                cert = new X509Certificate2(Path.Combine(ExampleHelper.ProcessDirectory, "shared\\dotnetty.com.pfx"), "password");
                targetHost = cert.GetNameInfo(X509NameType.DnsName, false);
            }
            try
            {
                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(group)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;

                        if (cert != null)
                        {
                            pipeline.AddLast(new TlsHandler(stream => new SslStream(stream, true, (sender, certificate, chain, errors) => true), new ClientTlsSettings(targetHost)));
                        }

                        pipeline.AddLast(new ProtobufVarint32FrameDecoder());
                        pipeline.AddLast(new ProtobufDecoder(Packet.DefaultInstance, RegistyExtensions()));
                        pipeline.AddLast(new ProtobufVarint32LengthFieldPrepender());
                        pipeline.AddLast(new ProtobufEncoder());
                        pipeline.AddLast(new RocketClientHandler());
                    }));

                IChannel bootstrapChannel = await bootstrap.ConnectAsync(new IPEndPoint(ClientSettings.Host, ClientSettings.Port));

                for (;;)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    try
                    {
                        await bootstrapChannel.WriteAndFlushAsync(line + "\r\n");
                    }
                    catch
                    {
                    }
                    if (string.Equals(line, "bye", StringComparison.OrdinalIgnoreCase))
                    {
                        await bootstrapChannel.CloseAsync();
                        break;
                    }
                }
                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                group.ShutdownGracefullyAsync().Wait(1000);
            }
        }

        public static ExtensionRegistry RegistyExtensions()
        {
            var extension = ExtensionRegistry.CreateInstance();
            CommonProtos.RegisterAllExtensions(extension);
            RequestProtos.RegisterAllExtensions(extension);
            GameProtos.RegisterAllExtensions(extension);
            PushProtos.RegisterAllExtensions(extension);
            AsyncProtos.RegisterAllExtensions(extension);
            return extension;
        }

        static void Main() => RunClientAsync().Wait();
    }
}
