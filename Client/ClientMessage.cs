using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

public class Client
{
    private static readonly string serverIP = "127.0.0.1"; // 伺服器 IP
    private static readonly int serverPort = 8888; // 伺服器 Port

    private static TcpClient client = null;
    private static StreamWriter writer = null;

    public static void Main()
    {
        try
        {
            client = new TcpClient(serverIP, serverPort);
            Console.WriteLine("已連接到伺服端!");
            Console.WriteLine("可以隨便打字");

            // 取得網路串流
            NetworkStream stream = client.GetStream();

            // 設定編碼方式
            StreamReader reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            // 開啟一個執行緒處理伺服器回傳的訊息
            Thread thread = new Thread(ReceiveMessage);
            thread.Start();

            // 等待使用者輸入訊息
            string message = "";
            while (message != "exit")
            {
                message = Console.ReadLine();
                SendMessage(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // 關閉連線
            client.Close();
        }
    }

    // 傳送訊息給伺服器
    private static void SendMessage(string message)
    {
        writer.WriteLine(message);
    }

    // 接收伺服器回傳的訊息
    private static void ReceiveMessage()
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
                Console.WriteLine(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
