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

namespace WindowsFormsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 公共变量，用来接收和发送消息
        /// </summary>
        Socket socket_client;
        private void Form1_Load(object sender, EventArgs e)
        {
            //第1步：创建socket
            socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //第2步：绑定
            IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).First(m => m.AddressFamily == AddressFamily.InterNetwork);
            IPEndPoint point = new IPEndPoint(ip, 6600);

            //第3步：连接
            socket_client.Connect(point);

            Thread thread = new Thread(m => {
                while(true)
                {
                    try
                    { 
                    byte[] result = new byte[1024];
                    int length = socket_client.Receive(result);
                    string msg_content = Encoding.UTF8.GetString(result, 0, length);
                    this.Invoke(new Action(()=> {
                        MsgHistory.Items.Add(msg_content);
                        MsgLog.Items.Add($"消息来源：消息内容：{msg_content},消息时间：{DateTime.Now}");
                    }));
                    }
                    catch(Exception ex)
                    {
                        MsgLog.Items.Add(ex.Message);
                        socket_client.Dispose();
                        break;
                    }
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //发送消息
            socket_client.Send(Encoding.UTF8.GetBytes(Msg.Text.Trim()));

            //清空
            Msg.Text = string.Empty;
        }
    }
}
