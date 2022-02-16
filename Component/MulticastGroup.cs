using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
    
namespace Component
{
    public class MulticastGroup
    {
        private IPAddress _address = null;
        private IPEndPoint _endPoint = null;
        private int _srcport = 0;
        private int _dstport = 0;

        private UdpClient _udpClient = null;

        private uint _syn = 1;

        public MulticastGroup(string MulticastAddress, int DstPort)
        {
            Setup(MulticastAddress, null, DstPort);
        }

        public MulticastGroup(string MulticastAddress, int SrcPort, int DstPort)
        {
            Setup(MulticastAddress, SrcPort, DstPort);
        }

        private void Setup(string MulticastAddress, int? SrcPort, int DstPort)
        {
            _address = IPAddress.Parse(MulticastAddress);
            if (SrcPort != null)
            {
                _srcport = (int)SrcPort;
                _udpClient = new UdpClient(_srcport);
            }
            else
            {
                _udpClient = new UdpClient(_srcport);
            }
            _dstport = DstPort;
            _endPoint = new IPEndPoint(_address, _dstport);
        }

        public void Join(string MulticastAddress, int SrcPort, int DstPort)
        {
            Setup(MulticastAddress, SrcPort, DstPort);
            Join();
        }

        public void Join()
        {
            _udpClient.JoinMulticastGroup(_address);
        }

        //public int Send(string Message)
        //{
        //    byte[] buffer = Encoding.Default.GetBytes(Message);
        //    return _udpClient.Send(buffer, buffer.Length, _endPoint);
        //}

        public int Send(string Message)
        {
            int sentBytes = 0;

            byte[] bufferMessage = Encoding.UTF8.GetBytes(Message);
            //DamienG.Security.Cryptography.CRC32 crc32 = new DamienG.Security.Cryptography.CRC32();
            //byte[] checksum = crc32.ComputeHash(bufferMessage);

            ProtocolDataUnit.HEADER header;
            header.Tag = 0x11;
            header.Syn = _syn;
            header.Ack = 0;
            header.DataLength = bufferMessage.Length;
            header.DataChecksum = 888;
            header.HeaderChecksum = 0;

            byte[] bufferHeader = header.ToBytes();

            sentBytes = _udpClient.Send(bufferHeader, bufferHeader.Length, _endPoint);
            sentBytes += _udpClient.Send(bufferMessage, bufferMessage.Length, _endPoint);

            //ProtocolDataUnit.HEADER headerReceive=new ProtocolDataUnit.HEADER();
            //headerReceive.Parse(buffer);


            return sentBytes;
        }

        public void SendThread()
        {

        }

        public void RecvThread()
        {
            UdpClient client = new UdpClient(7788);
            client.JoinMulticastGroup(IPAddress.Parse("234.5.6.7"));
            IPEndPoint multicast = new IPEndPoint(IPAddress.Parse("234.5.6.7"), 5566);
            while (true)
            {
                byte[] buf = client.Receive(ref multicast);
                string msg = Encoding.Default.GetString(buf);
                Console.WriteLine(msg);
            }
        }



    }


}
