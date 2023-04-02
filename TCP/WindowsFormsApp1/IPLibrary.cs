using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCPIP
{
    internal class IPLibrary
    {
        public IPAddress mLocalIP = IPAddress.Parse("127.0.0.1");
        public string LocalIP
        {
             
            get
            {
                return mLocalIP.ToString();
            }
            set 
            {
                
                if (IPAddress.TryParse(value,out mLocalIP)) //判斷能不能轉換成IP
                {
                    string[] AA = value.Split('.');
                    int[] IpInt = new int[AA.Length];   

                    //判斷轉成IP 是否都是正確避免輸入 "1.1.1" 只有三碼也會過
                    if (AA.Length != 3 &&  int.TryParse(AA[0],out IpInt[0])
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


    }
}
