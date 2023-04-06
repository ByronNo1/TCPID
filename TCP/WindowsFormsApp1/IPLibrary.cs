using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using WindowsFormsApp1;
using System.Runtime.InteropServices.ComTypes;

namespace TCPIP
{
    internal class IPLibrary
    {
        public string DeviceID = "0";

        private IPAddress mLocalIP = IPAddress.Parse("127.0.0.1");
        public string LocalIP
        {
            get
            {
                return mLocalIP.ToString();
            }
            set
            {
                if (IPAddress.TryParse(value, out mLocalIP)) //判斷能不能轉換成IP
                {
                    string[] AA = value.Split('.');
                    int[] IpInt = new int[AA.Length];

                    //判斷轉成IP 是否都是正確避免輸入 "1.1.1" 只有三碼也會過
                    if (AA.Length != 3 && int.TryParse(AA[0], out IpInt[0])
                       && int.TryParse(AA[1], out IpInt[1]) && int.TryParse(AA[2], out IpInt[2])
                       && int.TryParse(AA[3], out IpInt[3])
                       && IpInt[0] < 256 && IpInt[1] < 256 && IpInt[2] < 256 && IpInt[3] < 256)
                    {
                        mLocalIP = IPAddress.Parse(value);
                    }
                    else
                    {
                        mLocalIP = IPAddress.Parse("127.0.0.1");  //跳回預設
                        throw new Exception("IP error," + value); //警告
                                                                  //  throw new ArgumentNullException("IP error," + value);
                    }
                }
                else
                {
                    mLocalIP = IPAddress.Parse("127.0.0.1");          //跳回預設
                    throw new Exception("IPAddress error," + value);  //警告
                                                                      // throw new ArgumentNullException( "IP error," + value);
                }
            }
        }

        private int mLocalPort = 5000;
        public string LocalPort
        {
            get
            {
                return mLocalPort.ToString();
            }
            set
            {
                if (int.TryParse(value, out mLocalPort)) //判斷能不能轉換成IP
                {
                }
                else
                {
                    mLocalPort = 5000;         //跳回預設
                    throw new Exception("Port error," + value);  //警告
                    // throw new ArgumentNullException( "IP error," + value);
                }
            }

        }

        public SocketType ConnectionType = SocketType.Client;
        public enum SocketType
        {
            Client,
            Server
        }



        /// <summary>
        /// Clint
        /// </summary>
        //宣告網路資料流變數
        NetworkStream myNetworkStream = null;
        //宣告 Tcp 用戶端物件
        TcpClient myTcpClient = null;
        List<TcpClient> ListServerClient = new List<TcpClient>();  //紀錄SERVER 的連線端



        /// <summary>
        /// Server
        /// </summary>
        /// <returns></returns>
        TcpListener m_server;
        Thread m_thrListening; // 持續監聽是否有Client連線及收值的執行緒


        public int Connection()
        {
            if (ConnectionType == SocketType.Server)
            {
                return ServerHost(mLocalIP, mLocalPort);
            }
            else if (ConnectionType == SocketType.Client)
            {
                return Client(mLocalIP, mLocalPort);
            }

            return 0;
        }

        private int ServerHost(IPAddress _LocalIP, int _LocalPort)
        {
            try
            {
                int nPort = _LocalPort; // 設定 Port
                IPAddress localAddr = _LocalIP; // 設定 IP
                // Create TcpListener 並開始監聽
                m_server = new TcpListener(localAddr, nPort);
                m_server.Start();
                m_thrListening = new Thread(Listening);
                m_thrListening.Start();
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
                return -1;
            }
            return 0;
        }

        private void Listening()
        {
            try
            {
                while (true)
                {
                    //  UpdateStatus("Waiting for connection...");

                    TcpClient client = m_server.AcceptTcpClient(); // 要等有Client建立連線後才會繼續往下執行
                                                                   //   UpdateStatus("Connect to client!");
                    Console.WriteLine("OPEN:" + client.Client.RemoteEndPoint.ToString());
                    ListServerClient.Add(client);

                    Task tt = new Task(new Action(() =>
                        { 
                            ListeningGetData(client);
                            Console.WriteLine("Close:" + client.Client.RemoteEndPoint.ToString());
                            client.Close();
                            ListServerClient.Remove(client);
                            client = null;
                        }));
                    tt.Start();
                    Thread.Sleep(5);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
        }

        private void ListeningGetData(TcpClient client)
        {
            try
            {
                bool isClosed = false;
                byte[] btDatas = new byte[256]; // Receive data buffer
                byte[] testDatas = new byte[256]; // Receive data buffer
                string sData = "";
                NetworkStream stream = client.GetStream();
                sData = "";
                int i;
                do // 當有資料傳入時將資料顯示至介面上
                {

                    if (stream.DataAvailable && (i = stream.Read(btDatas, 0, btDatas.Length)) != 0)
                    {
                        sData = System.Text.Encoding.ASCII.GetString(btDatas, 0, i);
                        //     UdpateMessage("Received Data:" + sData);
                        Console.WriteLine("Received Data:" + sData);
                    }
                    //測試是否連線
                    if (client.Connected && client.Client.Poll(0, SelectMode.SelectRead))
                    {
                        isClosed = client.Client.Receive(testDatas, SocketFlags.Peek) == 0;
                    }

                    Thread.Sleep(5);
                } while (client.Client.Connected && isClosed == false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
        }




        private int Client(IPAddress _LocalIP, int _LocalPort)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4998); //設定本機 PORT
            myTcpClient = new TcpClient(ipep);
            try
            {
                //測試連線至遠端主機
                myTcpClient.Connect(_LocalIP, _LocalPort);
                Console.WriteLine("連線成功 !!\n");
                ////建立網路資料流
                //myNetworkStream = myTcpClient.GetStream();
                return 0;
            }
            catch
            {
                Console.WriteLine("主機 {0} 通訊埠 {1} 無法連接  !!", _LocalIP.ToString(), _LocalPort.ToString());
                return -1;
            }
        }
        //寫入資料
        public void ClientWriteData(string _strTest)
        {
            if (myTcpClient.Connected && _strTest != "")
            {
                String strTest = "this is a test string !!";
                strTest = _strTest;
                //將字串轉 byte 陣列，使用 ASCII 編碼
                //Byte[] myBytes = Encoding.ASCII.GetBytes(strTest);
                byte[] myBytes =CharClass.ConvertHexStrToByteArray(strTest);
                Console.WriteLine("建立網路資料流 !!");
                //建立網路資料流
                myNetworkStream = myTcpClient.GetStream();
                byte[] TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000501812CD3");
                //timeOut :
              //  myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000680000019");
             //   myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);

                Console.WriteLine("將字串寫入資料流　!!");
                //將字串寫入資料流
                //  myNetworkStream.Write(myBytes, 0, myBytes.Length);
                
                TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000100000000");  //client T7 time out
                myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000200000000");  //Server T7 time out
                myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);

                //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000180000019");
                ////timeOut :
                //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000280000019");
                ////timeOut :
                //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000380000019");
                ////timeOut :
                //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000480000019");
                ////timeOut :
                //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000580000019");
                ////timeOut :
                //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000680000019");
                ////timeOut :
                //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000980000019");
                ////timeOut :
                //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);


                while (true)
                {
                    Thread.Sleep(10);
                    if (myNetworkStream.DataAvailable)
                    {
                        byte[] data = new byte[myTcpClient.ReceiveBufferSize];
                        int length = myNetworkStream.Read(data, 0, data.Length);
                        string receiveMsg = "";//= Encoding.UTF8.GetString(data, 0, length);
                        for (int i = 0; i < length; i++)
                        {
                            receiveMsg += data[i].ToString("x2").ToUpper();
                        }

                        Console.WriteLine("\n接收服务端发来的数据: " + receiveMsg);
                        //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000180000019");
                        ////timeOut :
                        //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                        //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000280000019");
                        ////timeOut :
                        //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                        //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000380000019");
                        ////timeOut :
                        //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                        //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000480000019");
                        ////timeOut :
                        //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                        //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000580000019");
                        ////timeOut :
                        //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                        //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000680000019");
                        ////timeOut :
                        //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
                        //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000980000019");
                        ////timeOut :
                        //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);

                        Thread.Sleep(100);
                      //  break;
                    }
                }

            }

        }




        //讀取資料
        public void ClientReadData()
        {
            if (myTcpClient.Connected)
            {
                //Console.WriteLine("從網路資料流讀取資料 !!");
                ////從網路資料流讀取資料
                //int bufferSize = myTcpClient.ReceiveBufferSize;
                //byte[] myBufferBytes = new byte[bufferSize];
                //myNetworkStream.Read(myBufferBytes, 0, bufferSize);
                ////取得資料並且解碼文字
                //Console.WriteLine(Encoding.ASCII.GetString(myBufferBytes, 0, bufferSize));
                
                //建立網路資料流
                myNetworkStream = myTcpClient.GetStream();

                while (true)
                {
                    if (myNetworkStream.DataAvailable)
                    {
                        byte[] data = new byte[myTcpClient.ReceiveBufferSize];
                        int length = myNetworkStream.Read(data, 0, data.Length);
                        string receiveMsg = "";//= Encoding.UTF8.GetString(data, 0, length);
                        for (int i = 0; i < length; i++)
                        {
                            receiveMsg += data[i].ToString("x2").ToUpper();
                        }

                        Console.WriteLine("\n接收服务端发来的数据: " + receiveMsg);
                        break;
                    }
                }

            }
        }
        public void DisConnection()
        {
            if (ConnectionType == SocketType.Server)
            {
                if (m_thrListening.IsAlive)
                {
                    m_thrListening.Abort();
                    m_thrListening = null;
                    Thread.Sleep(50);
                }
                m_server.Stop();
                m_server = null;
            }
            if (myTcpClient != null) //判斷是否物件
            {
                myTcpClient.Close();
                if (myNetworkStream != null)
                {
                    myNetworkStream.Close();
                    myNetworkStream.Dispose();
                }
                myTcpClient = null;
            }


        }


    }








}
