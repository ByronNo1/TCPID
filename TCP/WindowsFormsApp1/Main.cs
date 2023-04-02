using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                pLibrary.LocalIP = txtIP.Text;
                pLibrary.LocalPort = txtPort.Text;
                pLibrary.ConnectionType = IPLibrary.SocketType.Client;
                pLibrary.ConnectionType = IPLibrary.SocketType.Server;
                pLibrary.Connection();
                //pLibrary.ClientWriteData("123ADEW");
                //pLibrary.ClientReadData();
               // pLibrary.DisConnection();
                // MessageBox.Show(pLibrary.LocalIP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
             //  MessageBox.Show(ex.Message + ",Rollback:" + pLibrary.LocalIP);
            }
          
        }
    }
}
