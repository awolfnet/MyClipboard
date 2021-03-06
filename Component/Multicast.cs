using Component.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static Component.ProtocolDataUnit;

namespace Component
{
    public class Multicast : IDataLink
    {
        private IPAddress _localAddress = null;
        private IPEndPoint _localEndPoint = null;
        private IPAddress _remoteAddress = null;
        private IPEndPoint _remoteEndPoint = null;

        private UdpClient _udp = null;

        private ThreadStart _recvThreadStart;
        private Thread _recvThread;

        public event EventHandler<DataArrivedEventArgs> DataArrived;

        public Multicast()
        {

        }

        public Multicast(string LocalAddress, int SrcPort, string MulticastAddress, int DstPort)
        {
            Setup(LocalAddress, SrcPort, MulticastAddress, DstPort);
        }

        private void Setup(string LocalAddress, int SrcPort, string MulticastAddress, int DstPort)
        {
            _remoteAddress = IPAddress.Parse(MulticastAddress);
            _remoteEndPoint = new IPEndPoint(_remoteAddress, DstPort);

            if (!string.IsNullOrWhiteSpace(LocalAddress))
            {
                _localAddress = IPAddress.Parse(LocalAddress);
            }

            if (_localAddress != null)
            {
                _localEndPoint = new IPEndPoint(_localAddress, SrcPort);
            }
            else
            {
                _localEndPoint = new IPEndPoint(IPAddress.Any, SrcPort);
            }

            _udp = new UdpClient(_localEndPoint);

            _udp.MulticastLoopback = false;
        }

        public void Join()
        {
            _udp.JoinMulticastGroup(_remoteAddress);

            _recvThreadStart = new ThreadStart(RecvThread);
            _recvThread = new Thread(_recvThreadStart);
            _recvThread.Start();
        }


        public void JoinMulticastGroup(string LocalAddress, int SrcPort, string MulticastAddress, int DstPort)
        {
            Setup(LocalAddress, SrcPort, MulticastAddress, DstPort);
            Join();
        }

        public void DropMulticastGroup()
        {
            _udp.DropMulticastGroup(_remoteAddress);
        }

        private void RecvThread()
        {
            while (true)
            {
                IPEndPoint recvEndPoint = null;

                //byte[] buffer = _udp.Receive(ref recvEndPoint);

                UdpClient receivingUdpClient = new UdpClient(31109);
                receivingUdpClient.JoinMulticastGroup(_remoteAddress);
                Byte[] buffer = receivingUdpClient.Receive(ref recvEndPoint);
                DataArrivedEventArgs args = new DataArrivedEventArgs()
                {
                    Length = buffer.Length,
                    Data = buffer
                };

                OnDataArrived(args);
            }


            //UdpClient client = new UdpClient(7788);
            //client.JoinMulticastGroup(IPAddress.Parse("234.5.6.7"));
            //IPEndPoint multicast = new IPEndPoint(IPAddress.Parse("234.5.6.7"), 5566);
            //while (true)
            //{
            //    byte[] buf = client.Receive(ref multicast);
            //    string msg = Encoding.Default.GetString(buf);
            //    Console.WriteLine(msg);
            //}
        }

        public int Send(byte[] Buffer)
        {
            return _udp.Send(Buffer, Buffer.Length, _remoteEndPoint);
        }

        private void OnDataArrived(DataArrivedEventArgs e)
        {
            DataArrived?.Invoke(this, e);
        }

    }


}
