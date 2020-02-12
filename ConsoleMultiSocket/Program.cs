using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("客户端");
            Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //创建IP地址
            IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).First(m => m.AddressFamily == AddressFamily.InterNetwork);

            //创建IP点
            IPEndPoint ip_point = new IPEndPoint(ip, 8800);

            socket_client.Connect(ip_point);

            new Thread(m => {
                while (true)
                {
                    byte[] result = new byte[1024];
                    int length = socket_client.Receive(result);
                    Console.WriteLine(Encoding.UTF8.GetString(result, 0, length));
                }
            }).Start();


            new Thread(m => {
                while (true)
                {
                    string msg = Console.ReadLine();
                    socket_client.Send(Encoding.UTF8.GetBytes(msg));
                }
            }).Start();
        }
    }
}
