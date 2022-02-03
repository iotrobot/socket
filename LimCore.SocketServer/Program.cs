// See https://aka.ms/new-console-template for more information
// https://www.cnblogs.com/gaochundong/p/csharp_tcp_service_models.html
using LimCore.SocketServer;

TcpServer server = new TcpServer(8889);
server.Listen();
Console.ReadKey();