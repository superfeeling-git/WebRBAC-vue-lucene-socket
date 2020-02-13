using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsProcess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process[] process = Process.GetProcesses(Dns.GetHostName());
            listView1.Items.Clear();
            foreach (var p in process)
            {                
                ListViewItem item = listView1.Items.Add(p.ProcessName);
                item.SubItems.Add(p.Id.ToString());
                item.SubItems.Add(p.PrivateMemorySize64.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "可执行文件(*.exe)|*.exe";
            var open = openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text);
            
        }
    }
}
