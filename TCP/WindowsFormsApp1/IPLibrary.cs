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
                        throw new Exception("Local IP error," + value); //警告
                                                                        //  throw new ArgumentNullException("IP error," + value);
                    }
                }
                else
                {
                    mLocalIP = IPAddress.Parse("127.0.0.1");          //跳回預設
                    throw new Exception("Local IPAddress error," + value);  //警告
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

        private IPAddress mRemoteIP = IPAddress.Parse("127.0.0.1");
        public string RemoteIP
        {
            get
            {
                return mRemoteIP.ToString();
            }
            set
            {
                if (IPAddress.TryParse(value, out mRemoteIP)) //判斷能不能轉換成IP
                {
                    string[] AA = value.Split('.');
                    int[] IpInt = new int[AA.Length];

                    //判斷轉成IP 是否都是正確避免輸入 "1.1.1" 只有三碼也會過
                    if (AA.Length != 3 && int.TryParse(AA[0], out IpInt[0])
                       && int.TryParse(AA[1], out IpInt[1]) && int.TryParse(AA[2], out IpInt[2])
                       && int.TryParse(AA[3], out IpInt[3])
                       && IpInt[0] < 256 && IpInt[1] < 256 && IpInt[2] < 256 && IpInt[3] < 256)
                    {
                        mRemoteIP = IPAddress.Parse(value);
                    }
                    else
                    {
                        mRemoteIP = IPAddress.Parse("127.0.0.1");  //跳回預設
                        throw new Exception("Remote IP error," + value); //警告
                                                                         //  throw new ArgumentNullException("IP error," + value);
                    }
                }
                else
                {
                    mRemoteIP = IPAddress.Parse("127.0.0.1");          //跳回預設
                    throw new Exception("Remote IPAddress error," + value);  //警告
                                                                             // throw new ArgumentNullException( "IP error," + value);
                }
            }
        }

        private int mRemotePort = 5000;
        public string RemotePort
        {
            get
            {
                return mRemotePort.ToString();
            }
            set
            {
                if (int.TryParse(value, out mRemotePort)) //判斷能不能轉換成IP
                {
                }
                else
                {
                    mRemotePort = 5000;         //跳回預設
                    throw new Exception("Remote Port error," + value);  //警告
                    // throw new ArgumentNullException( "IP error," + value);
                }
            }

        }

        public string StrReceiveMsg;
        public string StrSendMsg;

        public SocketType ConnectionType = SocketType.Client;
        public enum SocketType
        {
            Client,
            Server
        }

        private EventHandler IPLibraryConnectOk;
        private EventHandler IPLibraryDisConnect;
        private EventHandler IPLibraryReceiveMsg;
        private EventHandler IPLibrarySendMsg;


        /// <summary>
        /// Clint
        /// </summary>
        //宣告網路資料流變數
        NetworkStream myNetworkStream = null;
        //宣告 Tcp 用戶端物件
        TcpClient myTcpClient = null;
        bool isClientReadData = true;


        /// <summary>
        /// Server
        /// </summary>
        /// <returns></returns>
        TcpListener m_server;
        Thread m_thrListening; // 持續監聽是否有Client連線及收值的執行緒
        List<TcpClient> ListServerClient = new List<TcpClient>();  //紀錄SERVER 的連線端
        List<NetworkStream> ListServerNetworkStream = new List<NetworkStream>();  //紀錄SERVER 的連線端




        public IPLibrary()
        {

        }

        ~IPLibrary()
        {
            isClientReadData = false;
        }
        public event EventHandler ConnectOK
        {
            add
            {
                IPLibraryConnectOk += value;
            }
            remove
            {
                IPLibraryConnectOk -= value;
            }
        }
        public event EventHandler DisConnect
        {
            add
            {
                IPLibraryDisConnect += value;
            }
            remove
            {
                IPLibraryDisConnect -= value;
            }
        }
        public event EventHandler ReceiveMsg
        {
            add
            {
                IPLibraryReceiveMsg += value;
            }
            remove
            {
                IPLibraryReceiveMsg -= value;
            }
        }
        public event EventHandler SendMsg
        {
            add
            {
                IPLibrarySendMsg += value;
            }
            remove
            {
                IPLibrarySendMsg -= value;
            }
        }


        public int Connection()
        {
            try
            {
                if (ConnectionType == SocketType.Server)
                {
                    return ServerHost(mLocalIP, mLocalPort);
                }
                else if (ConnectionType == SocketType.Client)
                {
                    return Client(mRemoteIP, mRemotePort);
                }
            }
            catch (Exception)
            {

                return -1;
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
                if (IPLibraryConnectOk != null)
                {
                    IPLibraryConnectOk(this, null);
                }

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
                            //if (IPLibraryDisConnect != null)
                            //{
                            //    IPLibraryDisConnect(this, null);
                            //}
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
                //if (IPLibraryDisConnect != null)
                //{
                //    IPLibraryDisConnect(this, null);
                //}
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
                int intGetL;
                do // 當有資料傳入時將資料顯示至介面上
                {

                    if (stream.DataAvailable && (intGetL = stream.Read(btDatas, 0, btDatas.Length)) != 0)
                    {
                        // sData = System.Text.Encoding.ASCII.GetString(btDatas, 0, i);
                        sData = CharClass.byteTobyteString(btDatas, intGetL);
                        //     UdpateMessage("Received Data:" + sData);
                        Console.WriteLine("Received Data:" + sData);
                        StrReceiveMsg = sData;
                        if (IPLibraryReceiveMsg != null)
                        {
                            IPLibraryReceiveMsg(this, null);
                        }

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
                if (IPLibraryDisConnect != null)
                {
                    IPLibraryDisConnect(this, null);
                }
                Console.WriteLine("SocketException: {0}", ex);
            }
        }

        private void ServerWriteData(string _HexStrSend)
        {
            try
            {

                for (int i = 0; i < ListServerClient.Count; i++)
                {
                    TcpClient mclient = ListServerClient[i];
                    if (mclient != null && mclient.Connected && _HexStrSend != "")
                    {
                        NetworkStream stream = mclient.GetStream();
                        //將字串轉 byte 陣列，使用 ASCII 編碼
                        //Byte[] myBytes = Encoding.ASCII.GetBytes(strTest);
                        byte[] myBytes = CharClass.ConvertHexStrToByteArray(_HexStrSend);

                        Console.WriteLine("將字串寫入資料流　!!" + _HexStrSend);
                        //將字串寫入資料流
                        stream.Write(myBytes, 0, myBytes.Length);
                        if (IPLibrarySendMsg != null)
                        {
                            IPLibrarySendMsg(this, null);
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        private int Client(IPAddress _LocalIP, int _LocalPort)
        {
            IPEndPoint ipep = new IPEndPoint(mLocalIP, mLocalPort); //設定本機 PORT
            myTcpClient = new TcpClient(ipep);
            try
            {
                //測試連線至遠端主機
                myTcpClient.Connect(_LocalIP, _LocalPort);
                Console.WriteLine("連線成功 !!\n");
                //建立網路資料流
                myNetworkStream = myTcpClient.GetStream();
                m_thrListening = new Thread(ClientReadData);
                m_thrListening.Start();
                if (IPLibraryConnectOk != null)
                {
                    IPLibraryConnectOk(this, null);
                }
                return 0;
            }
            catch
            {
                Console.WriteLine("主機 {0} 通訊埠 {1} 無法連接  !!", _LocalIP.ToString(), _LocalPort.ToString());
                DisConnection();
                return -1;
            }
        }
        //寫入資料

        public void Send(string StrSend)
        {
            StrSendMsg = StrSend;
            if (ConnectionType == SocketType.Server)
            {
                ServerWriteData(StrSend);
            }
            else
            {
                ClientWriteData(StrSend);
            }

        }


        private void SendTEst()
        {
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

            //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000100000000");  //client T7 time out
            //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);
            //TimeBytes = CharClass.ConvertHexStrToByteArray("0000000AFFFF0000000200000000");  //Server T7 time out
            //myNetworkStream.Write(TimeBytes, 0, TimeBytes.Length);

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


                    Thread.Sleep(100);
                    //  break;
                }
            }


        }

        private void ClientWriteData(string _HexStrSend)
        {
            try
            {
                if (myTcpClient != null && myTcpClient.Connected && _HexStrSend != "")
                {
                    //將字串轉 byte 陣列，使用 ASCII 編碼
                    //Byte[] myBytes = Encoding.ASCII.GetBytes(strTest);
                    byte[] myBytes = CharClass.ConvertHexStrToByteArray(_HexStrSend);

                    Console.WriteLine("將字串寫入資料流　!!" + _HexStrSend);
                    //將字串寫入資料流
                    myNetworkStream.Write(myBytes, 0, myBytes.Length);
                    if (IPLibrarySendMsg != null)
                    {
                        IPLibrarySendMsg(this, null);
                    }

                }
            }
            catch (Exception)
            {

            }
        

        }

        //讀取資料
        public void ClientReadData()
        {
            try
            {
                if (myTcpClient.Connected)
                {


                    while (isClientReadData)
                    {
                        Thread.Sleep(5);
                        if (myTcpClient.Connected && myNetworkStream.DataAvailable)
                        {
                            byte[] data = new byte[myTcpClient.ReceiveBufferSize];
                            int length = myNetworkStream.Read(data, 0, data.Length);
                            string receiveMsg = "";//= Encoding.UTF8.GetString(data, 0, length);
                            //for (int i = 0; i < length; i++)
                            //{
                            //    receiveMsg += data[i].ToString("x2").ToUpper();
                            //}
                            receiveMsg = CharClass.byteTobyteString(data, length);
                            StrReceiveMsg = receiveMsg;
                            Console.WriteLine("收到SERVER的資訊: " + receiveMsg);
                            if (IPLibraryReceiveMsg != null)
                            {
                                IPLibraryReceiveMsg(this, null);
                            }
                        }
                        else
                        {
                            if (myTcpClient.Connected == false)
                            {
                                DisConnection();
                                break;
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                DisConnection();
            }

        }
        public void DisConnection()
        {
            if (IPLibraryDisConnect != null)
            {
                IPLibraryDisConnect(this, null);
            }

            try
            {
                Thread.Sleep(20);
                if (m_thrListening != null && m_thrListening.IsAlive)
                {
                    m_thrListening.Abort();
                    Thread.Sleep(20);
                }
                m_thrListening = null;
            }
            catch (Exception)
            {

            }

            if (ConnectionType == SocketType.Server)
            {
                try
                {
                    if (m_server != null)
                    {
                        m_server.Stop();
                    }
                    m_server = null;
                }
                catch (Exception)
                {

                }
            }

            try
            {
                if (myTcpClient != null) //判斷是否物件
                {
                    myTcpClient.Close();
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (myNetworkStream != null)
                {
                    myNetworkStream.Close();
                    myNetworkStream.Dispose();
                }
            }
            catch (Exception)
            {
            }
            myTcpClient = null;


        }


    }








}
