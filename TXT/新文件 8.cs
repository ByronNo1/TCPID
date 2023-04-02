//---------- 服务端 TcpListener--------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApplication12
{
    class Program
    {
        private static byte[] result = new byte[1024];
        private const int port = 8888;
        private static string IpStr = "127.0.0.1";
        private static TcpListener listener;
        public static List<TcpClient> clients = new List<TcpClient>();

        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse(IpStr);
            IPEndPoint ip_end_point = new IPEndPoint(ip, port);
            listener = new TcpListener(ip_end_point);
            listener.Start();
            Console.WriteLine("启动监听成功");

            //异步接收 递归循环接收多个客户端
            listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpclient), listener);
            Console.ReadKey();
        }

        private static void DoAcceptTcpclient(IAsyncResult State)
        {
            /*                   */
            /* 处理多个客户端接入*/
            /*                   */
            TcpListener listener = (TcpListener)State.AsyncState;
            
            TcpClient client = listener.EndAcceptTcpClient(State);

            clients.Add(client);

            Console.WriteLine("\n收到新客户端:{0}", client.Client.RemoteEndPoint.ToString());
            //开启线程用来持续收来自客户端的数据
            Thread myThread = new Thread(new ParameterizedThreadStart(printReceiveMsg));
            myThread.Start(client);

            listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpclient), listener);
        }

        private static void printReceiveMsg(object reciveClient)
        {
            /*                   */
            /* 用来打印接收的消息*/
            /*                   */
            TcpClient client = reciveClient as TcpClient;
            if (client == null)
            {
                Console.WriteLine("client error");
                return;
            }
            while (true)
            {
                try
                {
                    NetworkStream stream = client.GetStream();                    
                    int num = stream.Read(result, 0, result.Length); //将数据读到result中，并返回字符长度                  
                    if (num != 0)
                    {
                        string str = Encoding.UTF8.GetString(result, 0, num);//把字节数组中流存储的数据以字符串方式赋值给str
                        //在服务器显示收到的数据
                        Console.WriteLine("From: " + client.Client.RemoteEndPoint.ToString() + " : " + str);


                        //服务器收到消息后并会给客户端一个消息。
                        string msg = "服务器已收到您的消息[" + str + "]" ;
                        result = Encoding.UTF8.GetBytes(msg);
                        stream = client.GetStream();
                        stream.Write(result, 0, result.Length);
                        stream.Flush();
                    }
                    else
                    {   //这里需要注意 当num=0时表明客户端已经断开连接，需要结束循环，不然会死循环一直卡住
                        Console.WriteLine("客户端关闭");
                        break;
                    }
                }
                catch (Exception e)
                {
                    clients.Remove(client);
                    Console.WriteLine("error:" + e.ToString());
                    break;
                }

            }

        }
    }
}

//--------- TcpClient 端 -----------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ConsoleApplication16
{
    class Program
    {
        public static byte[] data = new byte[1024];
        public static void printReceiveMsg(object Obj_tcpClient)
        {
            TcpClient tcpClient = (TcpClient)Obj_tcpClient;
            NetworkStream stream = tcpClient.GetStream();

            while (true)
            {
                if (stream.DataAvailable)
                {
                    int length = stream.Read(data, 0, data.Length);
                    string receiveMsg = Encoding.UTF8.GetString(data, 0, length);
                    Console.WriteLine("\n接收服务端发来的数据: " + receiveMsg);
                    Console.Write("请输入您要发送的数据: ");
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("这是 TcpClient 端. ");
            //声明一个客户端
            TcpClient tcpClient = new TcpClient("127.0.0.1",8888);
            //声明一个流用来写数据
            NetworkStream stream = tcpClient.GetStream();

            //通过线程循环接收
            Thread T = new Thread(new ParameterizedThreadStart(printReceiveMsg));
            T.Start(tcpClient);

            while (true)
            {
                Console.Write("请输入您要发送的数据: ");
                string msg = Console.ReadLine();

                byte[] data = Encoding.UTF8.GetBytes(msg);
                stream.Write(data, 0, data.Length);
            }
        }
    }
}

 

 
————————————————
版权声明：本文为CSDN博主「蜕变之痛」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/liuxueyi521/article/details/88829517