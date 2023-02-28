using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using System.Collections.Generic;
using System.Linq;


namespace Server
{
    class Program
    {
        // 儲存所有客戶端連線
        static List<TcpClient> clients = new List<TcpClient>();

        static void Main(string[] args)
        {
            // 伺服器 IP 與 Port
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 8888;

            // 建立監聽 Socket
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();
            Console.WriteLine("伺服器已啟動");

            // 接受客戶端連線，並建立新執行緒處理連線
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("有新客戶端連線");

                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(client);
            }
        }

        // 處理客戶端連線
        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            clients.Add(client);

            // 取得用戶端資訊
            string clientInfo = client.Client.RemoteEndPoint.ToString();
            Console.WriteLine("客戶端 " + clientInfo + " 已連線");

            // 設定編碼方式
            Encoding encoding = Encoding.UTF8;

            // 處理客戶端發送的訊息
            while (true)
            {
                // 讀取客戶端發送的訊息
                byte[] buffer = new byte[1024];
                int n = client.GetStream().Read(buffer, 0, buffer.Length);
                string message = encoding.GetString(buffer, 0, n);
                Console.WriteLine("客戶端 " + clientInfo + " 傳送了訊息：" + message);

                // 轉發訊息給其他客戶端
                foreach (TcpClient c in clients)
                {
                    if (c != client)
                    {
                        c.GetStream().Write(buffer, 0, n);
                    }
                }

                if (message == "exit")
                {
                    // 關閉連線
                    // client.Close();
                    int index = clients.FindIndex(v => v.Client.RemoteEndPoint.ToString() == clientInfo);
                    clients.RemoveAt(index);
                    Console.WriteLine("客戶端 " + clientInfo + " 已斷線");
                }
            }

            
            
        }

        
    }
}
