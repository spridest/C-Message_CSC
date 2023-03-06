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
using Newtonsoft.Json;

namespace ClientWindow
{
    public partial class window : Form
    {

        private static TcpClient client = null;
        private static StreamWriter writer = null;
        public static RichTextBox richTextBox = new RichTextBox();

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

                    // 開啟一個執行緒處理伺服器回傳的訊息
                    Thread thread = new Thread(ReceiveMessage);
                    thread.Start();
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

        // 傳送訊息給伺服器
        private static void SendMessage(object message)
        {
            writer.WriteLine(message);
        }

        // 接收伺服器回傳的訊息
        private void ReceiveMessage()
        {
            try
            {
                // 取得網路串流
                NetworkStream stream = client.GetStream();

                // 設定編碼方式
                StreamReader reader = new StreamReader(stream);

                while (true)
                {
                    // 讀取伺服器回傳的訊息
                    string message = reader.ReadLine();

                    if (message == null)
                    {
                        Console.WriteLine("Disconnected from server.");
                        break;
                    }

                    // 顯示伺服器回傳的訊息

                    //Message JsonMessage = JsonConvert.DeserializeObject<Message>(message); // 解析Json

                    Console.WriteLine(message);
                    UpdateRichTextBox(message + "\n");
                    //Message_richTextBox.AppendText(message + "\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateRichTextBox(string message)
        {
            if (Message_richTextBox.InvokeRequired)
            {
                Message_richTextBox.Invoke(new Action<string>(UpdateRichTextBox), message);
                return;
            }

            Message_richTextBox.AppendText(message);
        }

        private void Message_richTextBox_TextChanged(object sender, EventArgs e)
        {
            Message_richTextBox.SelectionStart = Message_richTextBox.Text.Length;
            Message_richTextBox.ScrollToCaret();
        }

        private void SendMessage_button_Click(object sender, EventArgs e)
        {

            string SendText = SendMessage_textBox.Text;
            //Message message = new Message()
            //{
            //    Type = 0,
            //    Content = SendText
            //};
            //string json = JsonConvert.SerializeObject(message);
            if (SendText != "")
            {
                SendMessage(SendText);
                Message_richTextBox.AppendText("自己：" + SendText + "\n");
                SendMessage_textBox.Clear();
            }
        }

        class Message
        {
            public int Type { get; set; } // 0:text、1:image
            public string Sender { get; set; }
            public string Receiver { get; set; }
            public string Content { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private void SendMessage_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessage_button.PerformClick();
            }
        }
    }
}
