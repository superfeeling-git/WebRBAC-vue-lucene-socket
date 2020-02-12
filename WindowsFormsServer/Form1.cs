using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 用来存储不同客户端的socket对象
        /// </summary>
        Dictionary<string, Socket> dict = new Dictionary<string, Socket>();

        /// <summary>
        /// 初始化加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //第1步：创建socket
            Socket socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //第2步：绑定
            IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).First(m => m.AddressFamily == AddressFamily.InterNetwork);
            IPEndPoint point = new IPEndPoint(ip,6600);
            socket_server.Bind(point);

            //第3步：监听
            socket_server.Listen(10);


            Thread thread_client = new Thread(m => {
                while(true)
                { 
                    //第4步：返回新的用来和客户端通信的连接
                    Socket socket_client = socket_server.Accept();

                    IPEndPoint remote = socket_client.RemoteEndPoint as IPEndPoint;
                    string client_str = $"{remote.Address}:{remote.Port}";
                    ClientList.Items.Add(client_str);

                    //把当前socket对象加入集合
                    dict.Add(client_str, socket_client);


                    Thread thread_msg = new Thread(t => {
                        while(true)
                        {
                            Socket curr_socket = t as Socket;
                            byte[] result = new byte[1024];
                            int length = curr_socket.Receive(result);
                            string msg_content = Encoding.UTF8.GetString(result, 0, length);
                            MsgHistory.Items.Add(msg_content);
                            MsgLog.Items.Add($"消息来源：消息内容：{msg_content},消息时间：{DateTime.Now}");
                        }
                    });
                    thread_msg.IsBackground = true;
                    thread_msg.Start(socket_client);
                }
            });

            thread_client.IsBackground = true;
            thread_client.Start();
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(ClientList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("您没有选择客户端");
            }

            //要发送到的客户端 IP:端口号
            string key = ClientList.SelectedItems[0].Text;

            //找到目标socket
            Socket send_socket = dict.First(m => m.Key == key).Value;

            //发送消息
            send_socket.Send(Encoding.UTF8.GetBytes(Msg.Text.Trim()));

            //清空
            Msg.Text = string.Empty;
        }
    }
}