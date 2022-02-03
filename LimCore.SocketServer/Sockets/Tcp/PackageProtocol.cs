using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimCore.SocketServer.Sockets.Tcp
{
    public class PackageProtocol
    {
        public PackageProtocol(string msg)
        {
            // 判断包协议是否正确
        }

        public override string ToString()
        {
            return "";
        }

        public byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString());
        }
    }
}
