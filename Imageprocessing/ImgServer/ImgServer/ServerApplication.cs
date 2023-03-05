using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Net;

namespace ImgServer
{
    public partial class ServerApplication : Form
    {
        public ServerApplication()
        {
            InitializeComponent();
        }

        private string getIPAddress()
        {
            string hostName = Dns.GetHostName();
            Console.WriteLine(hostName);

            // Get the IP from GetHostByName method of dns class.
            string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return IP;
        }


        TcpServerChannel AdminServer = new TcpServerChannel(9090);
        private void btnStart_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Server Status: RUNNING";
            btnStop.Enabled = true;
            btnStart.BackColor = Color.Green;
            btnStop.BackColor = Color.CornflowerBlue;
            btnStart.Enabled = false;
            ChannelServices.RegisterChannel(AdminServer);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ImgCL.ImageProcess), "RemotingServer", WellKnownObjectMode.SingleCall);
            lblIp.Text = getIPAddress();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Server Status: STOPPED";
            btnStart.BackColor = Color.CornflowerBlue;
            btnStop.BackColor = Color.Red;
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            ChannelServices.UnregisterChannel(AdminServer);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ServerApplication_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
