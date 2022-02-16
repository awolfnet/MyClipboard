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
    public class Multicast: ITransit
    {
        private IPAddress _address = null;
        private IPEndPoint _endPoint = null;
        private int _srcport = 0;
        private int _dstport = 0;

        private UdpClient _udp = null;

        private uint _syn = 1;

        private ThreadStart _recvThreadStart;
        private Thread _recvThread;

        public event EventHandler DataArrived;

        public Multicast(string MulticastAddress, int DstPort)
        {
            Setup(MulticastAddress, null, DstPort);
        }

        public Multicast(string MulticastAddress, int SrcPort, int DstPort)
        {
            Setup(MulticastAddress, SrcPort, DstPort);
        }

        private void Setup(string MulticastAddress, int? SrcPort, int DstPort)
        {
            _address = IPAddress.Parse(MulticastAddress);
            _dstport = DstPort;
            _endPoint = new IPEndPoint(_address, _dstport);

            if (SrcPort != null)
            {
                _srcport = (int)SrcPort;
                _udp = new UdpClient(_srcport);
            }
            else
            {
                _udp = new UdpClient(_dstport);
            }

            _udp.MulticastLoopback = false;
        }

        public void Join(string MulticastAddress, int SrcPort, int DstPort)
        {
            Setup(MulticastAddress, SrcPort, DstPort);
            Join();
        }

        public void Join()
        {
            _udp.JoinMulticastGroup(_address);

            _recvThreadStart = new ThreadStart(RecvThread);
            _recvThread = new Thread(_recvThreadStart);
            _recvThread.Start();
        }

        private void RecvThread()
        {
            IPEndPoint recvEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] buffer = _udp.Receive(ref recvEndPoint);

            DataArrivedEventArgs args = new DataArrivedEventArgs()
            {
                Length = buffer.Length,
                Data = buffer
            };

            OnDataArrived(args);

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
            return _udp.Send(Buffer, Buffer.Length, _endPoint);
        }

        private void DataArrive()
        {

        }
        protected virtual void OnDataArrived(DataArrivedEventArgs e)
        {
            DataArrived?.Invoke(this, e);
        }
    }


}
