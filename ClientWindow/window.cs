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
        private static string ClientName = null;
        public static RichTextBox richTextBox = new RichTextBox();

        class Message
        {
            public int Type { get; set; } // 0:text、1:image
            public string Sender { get; set; }
            public string SenderIP { get; set; }
            public string Content { get; set; }
            public string ImageData { get; set; }
            public DateTime Timestamp { get; set; }
        }

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

                    // 設定名稱
                    ClientName = ClientName_textBox.Text;

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
            writer.Write(message);
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

                    Message MessageClass = JsonConvert.DeserializeObject<Message>(message); // 解析Json

                    if (MessageClass.Type == 0) // 文字訊息處理
                    {
                        Console.WriteLine(message);
                        UpdateRichTextBox($"[{MessageClass.Timestamp}]{MessageClass.Sender} {MessageClass.SenderIP}：\n{MessageClass.Content}\n");
                        //Message_richTextBox.AppendText(message + "\n");
                    }
                    else if (MessageClass.Type == 1) // 圖片訊息處理
                    {
                        Console.WriteLine(message);
                        UpdateRichTextBox($"[{MessageClass.Timestamp}]{MessageClass.Sender} {MessageClass.SenderIP}：\n", MessageClass.ImageData);
                        //Message_richTextBox.AppendText(message + "\n");
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 處裡接收到的文字
        private void UpdateRichTextBox(string message)
        {
            if (Message_richTextBox.InvokeRequired)
            {
                Message_richTextBox.Invoke(new Action<string>(UpdateRichTextBox), message);
                return;
            }
            Message_richTextBox.AppendText(message);
        }
        // 處理接收到的圖片
        private void UpdateRichTextBox(string message, string ImageData)
        {

            if (Message_richTextBox.InvokeRequired)
            {
                Message_richTextBox.Invoke(new Action<string>(UpdateRichTextBox), message);
                return;
            }
            Message_richTextBox.AppendText(message);


            // 将图像复制到剪贴板
            Image image = Byte2Image(ImageData);
            Clipboard.SetImage(image);
            // 将图像粘贴到 RichTextBox 控件中
            Message_richTextBox.Paste();

            if (Message_richTextBox.InvokeRequired)
            {
                Message_richTextBox.Invoke(new Action<string>(UpdateRichTextBox), "\n");
                return;
            }
            Message_richTextBox.AppendText("\n");
        }

        // 顯示窗變更時觸發
        private void Message_richTextBox_TextChanged(object sender, EventArgs e)
        {
            Message_richTextBox.SelectionStart = Message_richTextBox.Text.Length;
            Message_richTextBox.ScrollToCaret();
        }


        // 傳送訊息的按鈕按下後
        private void SendMessage_button_Click(object sender, EventArgs e)
        {

            string SendText = SendMessage_textBox.Text;
            Message message = new Message()
            {
                Type = 0,
                Sender = ClientName,
                Content = SendText,
                Timestamp = DateTime.Now
            };
            string json = JsonConvert.SerializeObject(message);
            if (SendText != "")
            {
                SendMessage(json);
                Message_richTextBox.AppendText($"自己：\n{SendText}\n");
                SendMessage_textBox.Clear();
            }
        }

        // 文字狀態按下Enter後
        private void SendMessage_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessage_button.PerformClick();
            }
        }



        // 尋找要發送的貼圖併發送
        private void sticker_button_Click(object sender, EventArgs e)
        {
            // 创建 OpenFileDialog 控件
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图像文件 (*.jpg, *.png)|*.jpg;*.png";

            // 显示 OpenFileDialog 控件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件路径
                string selectedFilePath = openFileDialog.FileName;

                // 發送貼圖
                Image SendIamge = ReszieImage(selectedFilePath);
                string ImageData = Image2Byte(SendIamge);

                Message message = new Message()
                {
                    Type = 1,
                    Sender = ClientName,
                    ImageData = ImageData,
                    Timestamp = DateTime.Now
                };
                string json = JsonConvert.SerializeObject(message);

                SendMessage(json);
                UpdateRichTextBox("自己：\n", ImageData);
            }
        }

        // 把圖片壓成能夠發送的大小
        private Image ReszieImage(string ImagePath)
        {
            // 加载原始图像
            Image originalImage = Image.FromFile(ImagePath);

            // 指定目标宽度和高度
            int targetWidth = 50;
            int targetHeight = 50;

            // 创建目标图像对象
            Image targetImage = new Bitmap(targetWidth, targetHeight);

            // 创建绘图对象
            Graphics graphics = Graphics.FromImage(targetImage);

            // 绘制调整后的图像
            graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);

            return targetImage;
        }

        // 把圖片壓成發送的文字串
        private string Image2Byte(Image image)
        {
            byte[] imageBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = memoryStream.ToArray();
            }
            string compressedImageBase64String = Convert.ToBase64String(imageBytes);
            return compressedImageBase64String;
        }

        // 把接收的文字串解壓成圖片
        private Image Byte2Image(string imagedata)
        {
            byte[] compressedImageBytes = Convert.FromBase64String(imagedata);
            Image receivedImage;
            using (MemoryStream memoryStream = new MemoryStream(compressedImageBytes))
            {
                receivedImage = Image.FromStream(memoryStream);
            }
            return receivedImage;
        }

        

    }
}
