using LimCore.SocketServer.Sockets.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LimCore.SocketServer
{
    public class TcpServer : SocketService, IDisposable
    {
        private int _port;
        public TcpServer(int port)
        {
            this._port = port;
        }

        public void Listen()
        {
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Any, this._port);
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(serverIp);
            tcpServer.Listen(100);
            Console.WriteLine("Tcp Server Service ready.");
            base._reciveAction = msgPro =>
            {
                // 判断协议是否正确

                Console.WriteLine("server<---{0}:{1}", base._socket.RemoteEndPoint?.ToString(), msgPro.MessageInfo.Content);
                string feedback = "收到消息.";
                Console.WriteLine("server--->{0}:{1}", base._socket.RemoteEndPoint?.ToString(), feedback);
                AsynSend(new PackageProtocol(feedback));

            };
            AsynAccept(tcpServer);
        }

        public void AsynAccept(Socket tcpServer)
        {
            tcpServer.BeginAccept(asyncResult =>
            {
                base._socket = (Socket)tcpServer.EndAccept(asyncResult);
                Console.WriteLine("Server<---{0}.", base._socket.RemoteEndPoint?.ToString());
                AsynAccept(tcpServer);
                AsynRecive(_reciveAction);
            }, null);
        }

        public void Dispose()
        {
            base._socket?.Dispose();
        }
    }
}
