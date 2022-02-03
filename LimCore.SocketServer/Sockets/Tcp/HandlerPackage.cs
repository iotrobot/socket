using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LimCore.SocketServer.Sockets.Tcp
{
    public class HandlerPackage
    {
        public string temp = string.Empty;
        public List<PackageProtocol> HandlerString(string package)
        {
            List<PackageProtocol> msgProList = new List<PackageProtocol>();
            if (!String.IsNullOrEmpty(temp))
            {
                package = temp + package;
            }
            string pattern = "(^<protocol Type=.*?>.*?</protocol>)";
            if (Regex.IsMatch(package, pattern))
            {
                //匹配协议内容
                string match = Regex.Match(package, pattern).Groups[0].Value;
                //将匹配的内容添加到集合
                msgProList.Add(HandlerObject(match));
                temp = string.Empty;
                //截取未匹配字符串，进行下一次匹配
                package = package.Substring(match.Length);
                if (!String.IsNullOrEmpty(package))
                {
                    msgProList.AddRange(HandlerString(package));
                }
            }
            else
            {
                temp = package;
            }
            return msgProList;
        }
    }
}
