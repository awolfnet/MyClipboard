using Component;
using Component.Interface;
using Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Component.ProtocolDataUnit;

namespace WindowsDesktop.Controller
{
    public class Group
    {
        private volatile static Group _instance = null;
        public static Group Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Group();
                }
                return _instance;
            }
        }

        private ITransport transport = null;
        private Device _device = null;

        public event EventHandler<DiscoveredEventArgs> Discovered;

        private Group()
        {
            transport = new Transport(Transport.TransportType.Multicat);
        }
        public void Join(string LocalAddress, int SrcPort, string RemoteAddress, int DstPort)
        {
            transport.Join(LocalAddress, SrcPort, RemoteAddress, DstPort);
        }

        public void Announce(string Name, string Platform, string IPAddress, int Port, Guid Guid)
        {
            _device = new Device()
            {
                Name = Name,
                Platform = Platform,
                IPAddress = IPAddress,
                Port = Port,
                Guid = Guid
            };

            transport.Announce(_device);
        }

        public void Search()
        {
            transport.Search();
        }



        //public int Send(HEADER header, string Message)
        //{
        //    int sentBytes = 0;

        //    byte[] bufferMessage = Encoding.UTF8.GetBytes(Message);
        //    //DamienG.Security.Cryptography.CRC32 crc32 = new DamienG.Security.Cryptography.CRC32();
        //    //byte[] checksum = crc32.ComputeHash(bufferMessage);

        //    header.Tag = 0x11;
        //    header.Syn = _syn;
        //    header.Ack = 0;
        //    header.DataLength = bufferMessage.Length;
        //    header.DataChecksum = 888;
        //    header.HeaderChecksum = 0;

        //    byte[] bufferHeader = header.ToBytes();

        //    sentBytes = _udp.Send(bufferHeader, bufferHeader.Length, _endPoint);
        //    sentBytes += _udp.Send(bufferMessage, bufferMessage.Length, _endPoint);

        //    //ProtocolDataUnit.HEADER headerReceive=new ProtocolDataUnit.HEADER();
        //    //headerReceive.Parse(buffer);


        //    return sentBytes;
        //}
    }

    public class DiscoveredEventArgs : EventArgs
    {
        public Device Node { set; get; }
        public DateTime Datetime { set; get; }
    }
}
