using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace TCPIP
{
    public partial class Main : Form
    {
        IPLibrary pLibrary = new IPLibrary();
        Thread threadTtimeOut;
        bool isTtimeOut;
        DateTime[] TimeHSMS_T = new DateTime[9];


        List<SecsTransaction> ListSendTrans = new List<SecsTransaction>();
        List<string[]> ListSendSecsSession = new List<string[]>();


        string StrReceive = "";
        List<string> ListStrReceive = new List<string>();
        DateTime TimeReceive;

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
                string str;
                if (radbtnActive.Checked)
                {
                    SendData(SecsSessionType.SelectRequest);
                    //str = SecsSessionType.SelectRequest;
                    //str = SECS.GetDataLenHead(str) + str;
                    //pLibrary.Send(str);

                    Thread.Sleep(100);

                    SendData(SecsSessionType.LinktestResponse);
                    //str = SecsSessionType.LinktestResponse;
                    //str = SECS.GetDataLenHead(str) + str;
                    //pLibrary.Send(str);
                }
                else
                {
                    Thread.Sleep(1000);
                    SendData(SecsSessionType.SelectResponse);
                    //str = SecsSessionType.SelectResponse;
                    //str = SECS.GetDataLenHead(str) + str;
                    //pLibrary.Send(str);

                    Thread.Sleep(500);
                    SendData(SecsSessionType.LinktestRequest);
                    //str = SecsSessionType.LinktestRequest;
                    //str = SECS.GetDataLenHead(str) + str;
                    //pLibrary.Send(str);
                }

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
            pLibrary.RemoteIP  = txtRemoteIP.Text;
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
                if (radbtnActive.Checked)
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
        {
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
        {
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
            TimeReceive = DateTime.Now;
            ParserReceiveMsg(ListStrReceive);
            //throw new NotImplementedException();
        }


        private void listBox1ADD(string _string)
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.BeginInvoke(new Action(() =>
                {
                    listBox1ADD(_string);
                }));
                return;
            } 

            if (listBox1.Items.Count > 100)
            {
                listBox1.Items.Clear();
            }
            listBox1.Items.Add(_string);
        }

        private void ParserReceiveMsg(List<string> _listStr)
        {
            string TmpStr = "";
            string TmpStrData = "";
            string TmpStrNext = "";
            string StrCount = "";
            int Count = 0;
            for (int i = 0; i < _listStr.Count; i++)
            {
                TmpStr = _listStr[i];
                do
                {
                    TmpStrNext = "";
                    if (TmpStr.Length > 12)
                    {
                        StrCount = TmpStr.Substring(0, 8);
                        Count = Convert.ToInt16(StrCount, 16);
                        if (Count * 2 <= TmpStr.Length - 8) //判斷要收資料是不是大於以收到的資料
                        {
                            TmpStrData = TmpStr.Substring(8, Count * 2);
                            listBox1ADD(StrCount+ TmpStrData);
                            if (TmpStr.Length - 8 > Count * 2)
                            {
                                TmpStrNext = TmpStr.Substring(8 + Count * 2, TmpStr.Length - 8 - Count * 2);
                                TmpStr = TmpStrNext;
                            }
                            else
                            {

                            }

                        }
                        else
                        {
                             //要判斷是不是已經收完資料
                        }

                    }
                } while (TmpStrNext != "");
                

                _listStr.RemoveAt(0);
                i = 0;
            }
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

        private void SendData(string _str)
        {
            
            string _strSend = "";
            string strSystem = "";
            if (_str.Substring(0,10) == "FFFF000000")
            {
                string[] sStr = new string[3];
                strSystem = SECS.StrSystemByte;
                _strSend = SECS.GetDataLenHead(_str + strSystem) + _str + strSystem;
                sStr[0] = _strSend;
                sStr[1] = _str;
                sStr[2] = strSystem;
                pLibrary.Send(_strSend);
            }
        }
      



    }
}
