using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static WindowsFormsApp1.SECS;

namespace TCPIP
{
    public partial class Main : Form
    {
        IPLibrary pLibrary = new IPLibrary();
        Thread threadTtimeOut;
        bool isTtimeOut;
        DateTime[] TimeHSMS_T = new DateTime[9];


        List<SecsTransaction> ListSendTrans = new List<SecsTransaction>();
        List<string[]> ListStrSendSecsSession = new List<string[]>();


        string StrReceive = "";
        List<string> ListStrReceive = new List<string>();
        DateTime TimeReceive;

        private static object Objectlock = new object();


        Queue QueReceiveData = new Queue();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            pLibrary.ConnectOK += PLibrary_ConnectOK;
            pLibrary.DisConnect += PLibrary_DisConnect;
            pLibrary.ReceiveMsg += PLibrary_ReceiveMsg;
            pLibrary.SendMsg += PLibrary_SendMsg;

            for (int i = 0; i < TimeHSMS_T.Length; i++)
            {
                TimeHSMS_T[i] = new DateTime();
            }
            isTtimeOut = true;
            threadTtimeOut = new Thread(HSMStime);
            threadTtimeOut.Start();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainFormDisConnect();
            isTtimeOut = false;
        }

        private void HSMStime()
        {
            while (isTtimeOut)
            {

            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //DataType tt = new DataType();
                //int i  = SECS.GetNLB((byte)DataType.INT_8 + 2, ref tt);

                //CheckStreamFunReceive("0000000C0000060C0000000080022100");

                //S1F3
                //"000000420000810300000042F3C60109B104000186A1B104000007D2B104000007D3B10400000003B104000007D1B104000007D4B10400000027B10400000028B10400000029";
                CheckStreamFunReceive("000000420000810300000042F3C60109B104000186A1B104000007D2B104000007D3B10400000003B104000007D1B104000007D4B10400000027B10400000028B10400000029");
                //byte tbyte = 0x8F;

                //if ((tbyte & 0x80) == 0x80)
                //{
                //    Console.WriteLine("OK");
                //}
                //else
                //{
                //    Console.WriteLine("NG");
                //}

                //int Itemp = (int)tbyte - (((int)tbyte / 16) * 16);

                //string sStr = Convert.ToString((int)tbyte, 2);
                //sStr = sStr.Substring(sStr.Length - 4, 4);
                //Itemp = Convert.ToInt32(sStr, 2);
                //Console.WriteLine(Itemp);

                //ListStrReceive.Clear();
                //ListStrReceive.Add("0000000C0000060C00000000");
                //ListStrReceive.Add("80022100");
                //ParserReceiveMsg(ListStrReceive); //將字串分析後
                //string str = "FF";
                //int tmpI = 0;
                ////tmpI = Convert.ToInt32(str, 16);
                // int.TryParse(str, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out tmpI);
                //int.TryParse(str, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out tmpI);
                //tmpI = tmpI;
                //string str;
                //if (radbtnActive.Checked)
                //{
                //    SendData(SecsSessionType.SelectRequest);
                //    //str = SecsSessionType.SelectRequest;
                //    //str = SECS.GetDataLenHead(str) + str;
                //    //pLibrary.Send(str);

                //    Thread.Sleep(100);

                //    SendData(SecsSessionType.LinktestResponse);
                //    //str = SecsSessionType.LinktestResponse;
                //    //str = SECS.GetDataLenHead(str) + str;
                //    //pLibrary.Send(str);
                //}
                //else
                //{
                //    Thread.Sleep(1000);
                //    SendData(SecsSessionType.SelectResponse);
                //    //str = SecsSessionType.SelectResponse;
                //    //str = SECS.GetDataLenHead(str) + str;
                //    //pLibrary.Send(str);

                //    Thread.Sleep(500);
                //    SendData(SecsSessionType.LinktestRequest);
                //    //str = SecsSessionType.LinktestRequest;
                //    //str = SECS.GetDataLenHead(str) + str;
                //    //pLibrary.Send(str);
                //}

                //string strRAW = "";
                //pLibrary.LocalIP = txtRemoteIP.Text;
                //pLibrary.LocalPort = txtRemotePort.Text;
                //pLibrary.ConnectionType = IPLibrary.SocketType.Client;
                //// pLibrary.ConnectionType = IPLibrary.SocketType.Server;
                //pLibrary.Connection();
                //strRAW = SECS.GetDataLenHead("00018101000000000001");
                //strRAW = strRAW + "00018101000000000001";
                //pLibrary.ClientWriteData(strRAW);
                ////  pLibrary.ClientReadData();
                //pLibrary.DisConnection();
                //// MessageBox.Show(pLibrary.LocalIP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //  MessageBox.Show(ex.Message + ",Rollback:" + pLibrary.LocalIP);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SecsTransaction S6F11 = new SecsTransaction(_strName: "S6F11", _intStreams: 6, _intFunctions: 11);


            //SECSItem A1 = new SECSItem(SECS.DataType.ASCII, 2, "A1");
            S6F11.Add(new SECSItem(SECS.DataType.LIST, 1, ""));
            S6F11.Add(new SECSItem(SECS.DataType.LIST, 3, ""));
            S6F11.Add(new SECSItem(SECS.DataType.UINT_4, 1, "0"));
            S6F11.Add(new SECSItem(SECS.DataType.UINT_4, 1, "1004"));
            S6F11.Add(new SECSItem(SECS.DataType.UINT_4, 1, "1004"));

            //S6F11.Add(new SECSItem(SECS.DataType.LIST, 1, ""));
            //S6F11.Add(new SECSItem(SECS.DataType.LIST, 2, ""));
            //S6F11.Add(new SECSItem(SECS.DataType.UINT_4, 1, "8"));
            //S6F11.Add(new SECSItem(SECS.DataType.LIST, 3, ""));
            //S6F11.Add(new SECSItem(SECS.DataType.UINT_1, 1, "0"));
            //S6F11.Add(new SECSItem(SECS.DataType.ASCII, 12, "AAAAAAAAAAAA"));
            //S6F11.Add(new SECSItem(SECS.DataType.UINT_4, 1, "0"));
            //S6F11.Add(new SECSItem(SECS.DataType.ASCII, 4, "END1"));


            //S6F11.Add(new SECSItem(SECS.DataType.UINT_2, 1, "22"));
            //S6F11.Add(new SECSItem(SECS.DataType.UINT_1, 1, "111"));
            //S6F11.Add(new SECSItem(SECS.DataType.BINARY, 1, "35"));
            //S6F11.Add(new SECSItem(SECS.DataType.ASCII, 0, ""));
            //S6F11.Add(new SECSItem(SECS.DataType.INT_8, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.INT_4, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.INT_2, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.INT_1, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.FT_8, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.FT_4, 1, "11"));
            //S6F11.Add(new SECSItem(SECS.DataType.UINT_8, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.UINT_4, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.ASCII, 4, "ABCD"));
            //S6F11.Add(new SECSItem(SECS.DataType.LIST, 0, ""));
            //S6F11.Add(new SECSItem(SECS.DataType.BINARY, 3, "303030"));
            //S6F11.Add(new SECSItem(SECS.DataType.BOOLEAN, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.ASCII, 1, "1"));
            //S6F11.Add(new SECSItem(SECS.DataType.JIS, 3, "111"));
            //S6F11.Add(new SECSItem(SECS.DataType.INT_8, 1, "1"));




            string tt = S6F11.GetSendString();
            tt = SECS.GetDataLenHead(tt) + tt;

            pLibrary.Send(tt);
            //     Console.WriteLine(tt);
            //  S6F11.ListSecSItems.Add();
            //string StrTime = SecsSessionType.SeparateRequest;
            //string strRAW = SECS.StrSystemByte;//  "";
            //string strTEMP = "";
            //SECS.StrSystemByte = "001";
            ////strRAW =  SECS.GetSxFy( "S2F1");
            ////0000000A00018101000000000001
            ////strRAW =  SECS.GetDataLenHead("00018101000000000001");
            ////strRAW = strRAW + "00018101000000000001";
            ////if (strRAW != "")
            ////{

            ////}

            //string strRAW;
            //strRAW += SECS.DataItemOut(SECS.DataType.LIST, "",3);
            //strRAW += SECS.DataItemOut(SECS.DataType.ASCII, "");
            //strRAW += SECS.DataItemOut(SECS.DataType.ASCII, "");
            //strRAW += SECS.DataItemOut(SECS.DataType.LIST, "",2);
            //strTEMP = CharClass.StringToAscString("asde");
            //strRAW += SECS.DataItemOut(SECS.DataType.ASCII, strTEMP);
            //strRAW += SECS.DataItemOut(SECS.DataType.LIST, "", 2);
            //strTEMP = CharClass.StringToAscString("11");
            //strRAW += SECS.DataItemOut(SECS.DataType.ASCII, strTEMP);
            //strRAW += SECS.DataItemOut(SECS.DataType.LIST, "", 1);
            //strRAW += SECS.DataItemOut(SECS.DataType.LIST, "", 1);
            //strTEMP = CharClass.StringToAscString("2123123123");
            //strRAW += SECS.DataItemOut(SECS.DataType.ASCII, strTEMP);
            //Console.WriteLine(strRAW);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            MainFormConnect();
            //  btnTest_Click(sender, e);
        }
        private void MainFormConnect()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    MainFormConnect();
                }));
                return;
            }

            palEthernetSetting.Enabled = false;
            btnConnect.Enabled = false;

            pLibrary.LocalIP = txtLocalIP.Text;
            pLibrary.LocalPort = txtLocalPort.Text;
            pLibrary.RemoteIP = txtRemoteIP.Text;
            pLibrary.RemotePort = txtRemotePort.Text;

            if (radbtnPassive.Checked)
            {
                pLibrary.ConnectionType = IPLibrary.SocketType.Server;
            }
            else
            {
                pLibrary.ConnectionType = IPLibrary.SocketType.Client;
            }
            int inRC = pLibrary.Connection();
            if (inRC >= 0)
            {
                palEthernetSetting.Enabled = false;
                btnConnect.Enabled = false;
                if (radbtnActive.Checked) //主動連線，要發送 SelectRequest請求
                {
                    SendData(SecsSessionType.SelectRequest);
                }
            }
            else
            {
                MainFormDisConnect();
                palEthernetSetting.Enabled = true;
                btnConnect.Enabled = true;
            }

        }

        private void PLibrary_SendMsg(object sender, EventArgs e)
        {//顯示送出的字串
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    PLibrary_SendMsg(sender, e);
                }));
                return;
            }
            txtSendMsg.Text = pLibrary.StrSendMsg;
            // throw new NotImplementedException();
        }

        private void PLibrary_ReceiveMsg(object sender, EventArgs e)
        {  //收到的字串都記錄到 ListStrReceive
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    PLibrary_ReceiveMsg(sender, e);
                }));
                return;
            }
            txtReceiveMsg.Text = pLibrary.StrReceiveMsg;
            ListStrReceive.Add(pLibrary.StrReceiveMsg);
            TimeReceive = DateTime.Now;       //紀錄收到的時間，作為後續判斷
            ParserReceiveMsg(ListStrReceive); //將字串分析後
            //throw new NotImplementedException();
        }

        private void LogAdd(string _str)
        {
            if (listBoxLog.InvokeRequired)
            {
                listBoxLog.BeginInvoke(new Action(() =>
                {
                    LogAdd(_str);
                }));
                return;
            }

            if (listBoxLog.Items.Count > 100)
            {
                listBoxLog.Items.Clear();
            }
            listBoxLog.Items.Add(_str);
        }

        private void listReceiveADD(string _str)
        {
            if (listReceive.InvokeRequired)
            {
                listReceive.BeginInvoke(new Action(() =>
                {
                    listReceiveADD(_str);
                }));
                return;
            }

            if (listReceive.Items.Count > 100)
            {
                listReceive.Items.Clear();
            }
            listReceive.Items.Add(_str);
        }

        private void listSnedADD(string _str)
        {
            if (listSend.InvokeRequired)
            {
                listSend.BeginInvoke(new Action(() =>
                {
                    listSnedADD(_str);
                }));
                return;
            }

            if (listSend.Items.Count > 100)
            {
                listSend.Items.Clear();
            }
            listSend.Items.Add(_str);
        }


        private void ParserReceiveMsg(List<string> _listStr) // 解析收到字串
        {
            string TmpStr = "";
            string TmpStrData = "";
            string TmpStrNext = "";
            string StrCount = "";
            int Count = 0;
            int Ilength = 0;
            try
            {
                lock (Objectlock)
                {
                    for (int i = 0; i < _listStr.Count; i++)
                    {
                        TmpStr = _listStr[i];
                        if (TmpStr != null)
                        {
                            #region 單行字串解析
                            do
                            {
                                TmpStrNext = ""; //字串下一個DATA
                                TmpStrData = ""; //本次取得的DATA
                                if (TmpStr.Length > 8)
                                {
                                    StrCount = TmpStr.Substring(0, 8);  // 取出有多少的個數

                                    if (int.TryParse(StrCount, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out Count))
                                    {
                                        if (Count * 2 <= TmpStr.Length - 8) //判斷要收資料是不是大於以收到的資料
                                        {
                                            TmpStrData = TmpStr.Substring(8, Count * 2);
                                            if (TmpStr.Length - 8 > Count * 2)
                                            {
                                                TmpStrNext = TmpStr.Substring(8 + Count * 2, TmpStr.Length - 8 - Count * 2);
                                                TmpStr = TmpStrNext;
                                            }
                                            else if (TmpStr.Length - 8 == Count * 2)
                                            {
                                                TmpStr = "";//代表解析完成
                                            }
                                            else
                                            {
                                                //保留
                                            }

                                        }
                                        else
                                        {
                                            //要判斷是不是已經收完資料
                                        }
                                    }
                                    else
                                    {
                                        break; //收到的字串無法轉換成int
                                    }

                                }
                                if (TmpStrData != "")
                                {
                                    listReceiveADD(StrCount + TmpStrData);
                                    QueReceiveData.Enqueue(StrCount + TmpStrData); //整理過的DATA 紀錄到Queue，準備之後回復用
                                }
                            } while (TmpStrNext != "");
                            #endregion

                        }
                        #region 判斷後續有沒有值
                        if (TmpStr == "")  //代表這行已經收完
                        {
                            _listStr[i] = null;
                        }
                        else
                        { //沒有收完把後面的往前面合併

                            if (_listStr.Count >= i + 1) //先確認有收到後面的值
                            {
                                _listStr[i + 1] = TmpStr + _listStr[i + 1]; //把後面的往前面合併
                                _listStr[i] = null;
                            }
                        }
                        #endregion

                    }

                    #region 將空值清除
                    Ilength = _listStr.Count;
                    for (int i = 0; i < Ilength; i++)
                    {
                        if (_listStr[Ilength - 1 - i] == null)
                        {
                            _listStr.RemoveAt(Ilength - 1 - i);
                        }
                    }
                    #endregion


                    while (QueReceiveData.Count > 0)
                    {
                        CheckReceiveStr((string)QueReceiveData.Dequeue());
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }





        }

        private void CheckReceiveStr(string _str)
        {

            string StrSystem = "";
            string StrCount = _str.Substring(0, 8);  // 取出有多少的個數
            string StrData = _str.Substring(8, _str.Length - 8);

            if (StrData.Substring(0, 4) == "FFFF") //這是 T1~T9之類的
            {
                CheckSecsSession(_str);
            }
            else
            {
                CheckStreamFunReceive(_str);
            }


        }

        private void CheckSecsSession(string _str)
        {
            string StrCount = _str.Substring(0, 8);  // 取出有多少的個數
            string StrNoCount = _str.Substring(8, _str.Length - 8);
            string StrSystem = "";

            string StrData = StrNoCount.Substring(0, 12);
            LogAdd("StrData:" + StrData);
            StrSystem = StrNoCount.Substring(12, StrNoCount.Length - 12);
            LogAdd("StrSystem:" + StrSystem);
            switch (StrData)
            {
                case SecsSessionType.SelectRequest:
                    SendData(SecsSessionType.SelectResponse, StrSystem);
                    break;
                case SecsSessionType.SelectResponse:
                    break;
                case SecsSessionType.DeselectRequest:
                    SendData(SecsSessionType.DeselectResponse, StrSystem);
                    break;
                case SecsSessionType.DeselectResponse:
                    break;
                case SecsSessionType.LinktestRequest:
                    SendData(SecsSessionType.LinktestResponse, StrSystem);
                    break;
                case SecsSessionType.LinktestResponse:
                    break;
                case SecsSessionType.RejectRequest:
                    break;
                case SecsSessionType.SeparateRequest:
                    break;

            }
        }

        private void CheckStreamFunReceive(string _str)
        {
            string StrCount = _str.Substring(0, 8);  // 取出有多少的個數
            int intCount = 0;
            string StrNoCount = _str.Substring(8, _str.Length - 8);
            // string StrSystem = "";


            int.TryParse(StrCount, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out intCount);
            byte[] mybytes = CharClass.ConvertHexStrToByteArray(StrNoCount); //轉成byte 
            if (intCount != mybytes.Length ) //判斷資料個數是否正確
            {  //有異常
                return;
            }
            
            if (mybytes.Length < 10)
            {  //有異常
                return;
            }
            string strRole;
            if (mybytes[0] == 0x81)
            { //由Equipment 發出
                strRole = "Equipment";
            }
            else if (mybytes[0] == 0x01)
            {//HOST 發出
                strRole = "HOST";
            }
            else
            {
                strRole = "";
            }

            string DeviceID;
            DeviceID = mybytes[1].ToString("X2"); //X2 16進制字串


            bool isNeedReply = false;
            int intQuotient = ((int)mybytes[2] / 16);
            //-------------- 商數等於8 是0x80意思
            if (intQuotient == 8)
            {
                //0x80 必須回復
                isNeedReply = true;
            }
            //--------------
            ////--------------另外一種 
            //if ((mybytes[2] & 0x80) == 0x80) //用& 比較不是有0x80   (0x75 & 0x70) >> 0x70
            //{
            //    //0x80 必須回復
            //    isNeedReply = true;
            //}
            ////-------------- 也可以用2進制取得 128的位置換算是不是等於 1xxx xxxx 
            string strStream;
            int intStream;

            intStream = (int)mybytes[2] - (intQuotient * 16); // 取除16的餘數，利用取商乘 16 用乘法運算比 %取餘還快
            ////----------------------- 另外一種轉法 轉2進制 超過4位數的不要 即可取0~15數值
            //strStream = Convert.ToString((int)mybytes[2], 2);
            //strStream = strStream.Substring(strStream.Length - 4, 4);
            //intStream = Convert.ToInt32(strStream, 2);
            ////-----------------------
            strStream = intStream.ToString();

            string strFunctions;
            int intFunctions;

            intFunctions = (int)mybytes[3]; //強制轉型
            strFunctions = intFunctions.ToString();


            //最後一個區塊
            string strLastBlock;
            strLastBlock = mybytes[4].ToString("X2");
            //第一個區塊
            string strFristBlock;
            strFristBlock = mybytes[5].ToString("X2");

           
            //SystemByte 
            string strSystemByte;
            strSystemByte = mybytes[6].ToString("X2") + mybytes[7].ToString("X2") +
                            mybytes[8].ToString("X2") + mybytes[9].ToString("X2");

            string StrData = "";
            //for (int i = 9; i < mybytes.Length; i++)
            //{
            //    StrData += mybytes[i].ToString("X2");
            //}
            StrData = StrNoCount.Substring(10 * 2, StrNoCount.Length  - 10 * 2);

            SECS.DataInItem(StrData);

         }


        private void PLibrary_DisConnect(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    PLibrary_DisConnect(sender, e);
                }));
                return;
            }
            picEthernetConnect.Image = picRed.Image;
            btnDisConnected.Enabled = false;
            palEthernetSetting.Enabled = true;
            btnConnect.Enabled = true;
            btnDisConnected.Enabled = true;
            //  throw new NotImplementedException();
        }

        private void PLibrary_ConnectOK(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    PLibrary_ConnectOK(sender, e);
                }));
                return;
            }
            picEthernetConnect.Image = picGreen.Image;
            // throw new NotImplementedException();
        }

        private void MainFormDisConnect()
        {
            string str = SecsSessionType.SeparateRequest;
            str = SECS.GetDataLenHead(str) + str;
            pLibrary.Send(str);
            Thread.Sleep(20);
            pLibrary.DisConnection();

        }
        private void btnDisConnected_Click(object sender, EventArgs e)
        {
            MainFormDisConnect();
        }

        private void SendData(string _str, string _strSystem = "")
        {

            string _strSend = "";
            string strSystem = _strSystem;
            if (_str.Substring(0, 10) == "FFFF000000")
            {
                string[] sStr = new string[3];
                if (strSystem == "")
                {
                    strSystem = SECS.StrSystemByte;
                }
                _strSend = SECS.GetDataLenHead(_str + strSystem) + _str + strSystem;
                sStr[0] = _strSend;
                sStr[1] = _str;
                sStr[2] = strSystem;
                pLibrary.Send(_strSend);
                listSnedADD(_strSend);
            }
        }



    }
}
