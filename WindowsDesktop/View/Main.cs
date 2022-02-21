using Component;
using Component.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsDesktop.Controller;

namespace WindowsDesktop.View
{
    public partial class Main : Form
    {
        Group group = Group.Instance;
        Controller.Clipboard clipboard = Controller.Clipboard.Instance;
        System.Net.Sockets.UdpClient Client;

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress mcastAddress = System.Net.IPAddress.Parse("224.0.0.251");
            int mcastPort = 5353;

            System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);
            System.Net.IPAddress localAddress = System.Net.IPAddress.Parse("192.168.52.1");
            System.Net.IPEndPoint localEndPoint = new System.Net.IPEndPoint(localAddress, mcastPort);
            System.Net.Sockets.MulticastOption multicastOption = new System.Net.Sockets.MulticastOption(mcastAddress, localAddress);
            socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ReuseAddress, true);
            socket.Bind(localEndPoint);
            socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.IP, System.Net.Sockets.SocketOptionName.AddMembership, multicastOption);

            // multicast UDP-based mDNS-packet for discovering IP addresses


            //System.Net.IPEndPoint endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("224.0.0.251"), 5353);

            

            //System.Net.IPEndPoint localpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("192.168.52.1"), 5353);

            
            //socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ReuseAddress, true);
            
            //socket.Bind(localpoint);

            byte[] buffer = new byte[1024];
            int r = socket.Receive(buffer);
            System.Diagnostics.Debug.WriteLine(r);


            //List<byte> bytes = new List<byte>();

            //bytes.AddRange(new byte[] { 0x0, 0x0 });  // transaction id (ignored)
            //bytes.AddRange(new byte[] { 0x1, 0x0 });  // standard query
            //bytes.AddRange(new byte[] { 0x0, 0x1 });  // questions
            //bytes.AddRange(new byte[] { 0x0, 0x0 });  // answer RRs
            //bytes.AddRange(new byte[] { 0x0, 0x0 });  // authority RRs
            //bytes.AddRange(new byte[] { 0x0, 0x0 });  // additional RRs
            //bytes.AddRange(new byte[] { 0x05, 0x5f, 0x68, 0x74, 0x74, 0x70, 0x04, 0x5f, 0x74, 0x63, 0x70, 0x05, 0x6c, 0x6f, 0x63, 0x61, 0x6c, 0x00, 0x00, 0x0c, 0x00, 0x01 });  // _http._tcp.local: type PTR, class IN, "QM" question

            //socket.SendTo(bytes.ToArray(), endpoint);

            //group.Join("192.168.52.1", 0, "224.0.0.251", 5353);
            //group.Announce("WolfDesktop", "Windows", "192.168.52.1", 31109, Guid.NewGuid());
            //group.Search();
            //group.Discovered += Group_Discovered;
        }

        //CallBack
        private void recv(IAsyncResult res)
        {
            System.Net.IPEndPoint RemoteIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 8000);
            byte[] received = Client.EndReceive(res, ref RemoteIpEndPoint);

            //Process codes

            MessageBox.Show(Encoding.UTF8.GetString(received));
            Client.BeginReceive(new AsyncCallback(recv), null);
        }


        private void Group_Discovered(object sender, DiscoveredEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            group.Search();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            clipboard.Changed += Clipboard_Changed;
        }

        private void Clipboard_Changed(object sender, Controller.Clipboard.ClipboardChangedEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                textBox1.Text = (string)e.DataObject.GetData(DataFormats.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            group.Announce("WolfDesktop", "Windows", "192.168.52.1", 31109, Guid.NewGuid());
        }
    }
}
