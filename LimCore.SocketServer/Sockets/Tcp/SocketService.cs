using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LimCore.SocketServer.Sockets.Tcp
{
    public abstract class SocketService
    {
        protected Socket _socket;
        public Action<PackageProtocol> _reciveAction;
        protected HandlerPackage _handlerPackage = new HandlerPackage();

        protected void AsynRecive(Action<PackageProtocol> reciveAction)
        {
            byte[] buffer = new byte[1024];
            _socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None,
            asyncResult =>
            {
                int length = _socket.EndReceive(asyncResult);
                string msg = Encoding.UTF8.GetString(buffer, 0, length);
                foreach (PackageProtocol item in _handlerPackage.HandlerString(msg))
                {
                    reciveAction(item);
                }
                AsynRecive(reciveAction);
            }, null);
        }

        public void AsynSend(PackageProtocol packageProtocol)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(packageProtocol.ToString());
            _socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, asyncResult =>
            {
                //完成发送消息
                int length = _socket.EndSend(asyncResult);
            }, null);
        }
    }
}
