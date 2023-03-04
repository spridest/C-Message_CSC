using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ClientWindow
{
    public partial class window : Form
    {

        private static TcpClient client = null;
        private static StreamWriter writer = null;

        public window()
        {
            InitializeComponent();
        }

        private void Connect_button1_Click(object sender, EventArgs e)
        {
            try
            {
                var serverIP = IP_textBox.Text;
                var serverPort = int.Parse(Port_textBox.Text);

                if (client == null)
                {
                    client = new TcpClient(serverIP, serverPort); // 連接
                    Message_richTextBox.AppendText("已連接到伺服端!\n");
                    Console.WriteLine("已連接到伺服端!");

                    // 取得網路串流
                    NetworkStream stream = client.GetStream();

                    // 設定編碼方式
                    StreamReader reader = new StreamReader(stream);
                    writer = new StreamWriter(stream);
                    writer.AutoFlush = true;

                }
                else
                {
                    Message_richTextBox.AppendText("離線後再使用!\n");
                    Console.WriteLine("離線後再使用!");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Message_richTextBox.AppendText("連線異常!\n");
            }
        }

        private void Message_richTextBox_TextChanged(object sender, EventArgs e)
        {
            Message_richTextBox.SelectionStart = Message_richTextBox.Text.Length;
            Message_richTextBox.ScrollToCaret();
        }
    }
}
