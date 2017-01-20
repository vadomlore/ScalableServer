namespace ScalableServer
{
    using System;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using DotNetty.Handlers.Logging;
    using DotNetty.Handlers.Tls;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using DotNetty.Codecs.ProtocolBuffers;

    using Examples.Common;
    using Com.Virtuos.Rocket.NetworkMessage;
    using DotNetty.Codecs.Protobuf;
    using Google.ProtocolBuffers;

    class Program
    {
        static async Task RunServerAsync()
        {
            ExampleHelper.SetConsoleLogger();

            var bossGroup = new MultithreadEventLoopGroup(1);
            var workerGroup = new MultithreadEventLoopGroup();
            X509Certificate2 tlsCertificate = null;
            if (ServerSettings.IsSsl)
            {
                tlsCertificate = new X509Certificate2(Path.Combine(ExampleHelper.ProcessDirectory, "shared\\dotnetty.com.pfx"), "password");
            }
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    .Handler(new LoggingHandler("LSTN"))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        if (tlsCertificate != null)
                        {
                            pipeline.AddLast(TlsHandler.Server(tlsCertificate));
                        }
                        pipeline.AddLast(new LoggingHandler("CONN"));
                        pipeline.AddLast(new ProtobufVarint32FrameDecoder());

                        pipeline.AddLast(new ProtobufDecoder(Packet.DefaultInstance, RegistyExtensions()));
                        pipeline.AddLast(new ProtobufVarint32LengthFieldPrepender());
                        pipeline.AddLast(new ProtobufEncoder());
                        pipeline.AddLast(new RocketServerlHandler());
                    }));

                IChannel bootstrapChannel = await bootstrap.BindAsync(ServerSettings.Port);

                Console.ReadLine();

                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
            }
        }

        public static void Main() => RunServerAsync().Wait();

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
    }
}
