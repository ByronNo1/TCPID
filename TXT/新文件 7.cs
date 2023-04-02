//建立一個監聽用Socket
static Socket socketListener;
//建立一個Server方法
public static void Server(int myPort,int allowNum)
{ 
//實作監聽用Socket，
//AddressFamily.InterNetwork表示利用IP4協議
//SocketType.Stream 因為我們要使用TCP協議，需使用流式的Socket
//ProtocolType.Tcp 選用TCP協議
socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//指定伺服器IP，這邊利用IPAddress.Any方法得到本機的ＩＰ，讓這方法可以運用更靈活
IPAddress ip = IPAddress.Any;
//設置應用程序的端口
int port = myPort;
//利用IPEndPoint這個類，將伺服器端的IP還有端口帶入給socketListener
IPEndPoint point = new IPEndPoint(ip, port);
//將point綁定給socketListener
socketListener.Bind(point);

ShowMsg("Listening...");
//設定幾個程序可以連至本伺服器
socketListener.Listen(allowNum);
}

//傳送訊息
private static void ShowMsg(string s)
{
    Console.WriteLine(s);
}

//建立Server中用來傳送資料給客戶端的Socket
public static void ServerSender(Socket listener)
{ 
//利用Accept方法接收監聽用Socket資料
Socket socketSender = listener.Accept();
//如果連線成功利用socketSender.RemoteEndPoint取得所連線到的Socket的IP和Port
ShowMsg(("Client IP = " + socketSender.RemoteEndPoint.ToString()) + " Connect Succese!");
}

public static void Main(string[] args)
{
//將端口設置為6000，設定三個應用程序可以連線到伺服器
Server(60000,3);
//取得客戶端資料
ServerSender(socketListener);
}