using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace TCPIP
{
    public partial class Main : Form
    {
        IPLibrary pLibrary = new IPLibrary();
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                string strRAW = "";
                pLibrary.LocalIP = txtIP.Text;
                pLibrary.LocalPort = txtPort.Text;
                pLibrary.ConnectionType = IPLibrary.SocketType.Client;
               // pLibrary.ConnectionType = IPLibrary.SocketType.Server;
                pLibrary.Connection();
                strRAW = SECS.GetDataLenHead("00018101000000000001");
                strRAW = strRAW + "00018101000000000001";
                pLibrary.ClientWriteData(strRAW);
              //  pLibrary.ClientReadData();
                pLibrary.DisConnection();
                // MessageBox.Show(pLibrary.LocalIP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
             //  MessageBox.Show(ex.Message + ",Rollback:" + pLibrary.LocalIP);
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string StrTime = SecsSessionType.SeparateRequest;
            string strRAW = SECS.StrSystemByte;//  "";
            string strTEMP = "";

            //strRAW =  SECS.GetSxFy( "S2F1");
            //0000000A00018101000000000001
            //strRAW =  SECS.GetDataLenHead("00018101000000000001");
            //strRAW = strRAW + "00018101000000000001";
            //if (strRAW != "")
            //{

            //}
           
           
            strRAW += SECS.DataItemOut(SECS.DataType.LIST, "",3);
            strRAW += SECS.DataItemOut(SECS.DataType.ASCII, "");
            strRAW += SECS.DataItemOut(SECS.DataType.ASCII, "");
            strRAW += SECS.DataItemOut(SECS.DataType.LIST, "",2);
            strTEMP = CharClass.StringToAscString("asde");
            strRAW += SECS.DataItemOut(SECS.DataType.ASCII, strTEMP);
            strRAW += SECS.DataItemOut(SECS.DataType.LIST, "", 2);
            strTEMP = CharClass.StringToAscString("11");
            strRAW += SECS.DataItemOut(SECS.DataType.ASCII, strTEMP);
            strRAW += SECS.DataItemOut(SECS.DataType.LIST, "", 1);
            strRAW += SECS.DataItemOut(SECS.DataType.LIST, "", 1);
            strTEMP = CharClass.StringToAscString("2123123123");
            strRAW += SECS.DataItemOut(SECS.DataType.ASCII, strTEMP);
            Console.WriteLine(strRAW);
        }



    }
}
