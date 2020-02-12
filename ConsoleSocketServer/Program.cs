using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("服务端");
            //第1步：创建Socket
            Socket socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            //*********创建ip地址和创建IP点是为绑定服务的
            //创建IP地址
            IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).First(m => m.AddressFamily == AddressFamily.InterNetwork);

            //创建IP点
            IPEndPoint ip_point = new IPEndPoint(ip, 8800);
            //*********创建ip地址和创建IP点是为绑定服务的


            //第2步：绑定
            socket_server.Bind(ip_point);


            //第3步：监听//阻塞
            socket_server.Listen(10);

            //重复监听客户端的连接
            Thread conn_thread = new Thread(t => {
                while(true)
                {
                    //第4步：接受客户端连接
                    Socket socket_client = socket_server.Accept();

                    Thread thread = new Thread(m => {
                        while (true)
                        {
                            //服务器接收消息
                            byte[] result = new byte[1024];
                            int length = socket_client.Receive(result);
                            Console.WriteLine(Encoding.UTF8.GetString(result, 0, length));
                        }
                    });
                    thread.Start();


                    //第5步：用新的套接字发送和接收数据
                    Thread send_thread = new Thread(m => {
                        while (true)
                        {
                            string msg = Console.ReadLine();
                            socket_client.Send(Encoding.UTF8.GetBytes(msg));
                        }
                    });
                    send_thread.Start();
                }
            });

            conn_thread.Start();
            
        }
    }
}
