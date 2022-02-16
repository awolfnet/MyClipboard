using Component;
using Component.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Component.ProtocolDataUnit;

namespace WindowsDesktop.Controller
{
    public class Group : IGroup
    {
        private static volatile Group _instance = null;
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

        private ITransit transit = null;

        public event EventHandler DeviceDiscovered;

        private Group()
        {

        }
        public void Join(string LocalAddress, int SrcPort, string RemoteAddress, int DstPort)
        {
            transit = new Multicast();
            transit.Join(LocalAddress, SrcPort, RemoteAddress, DstPort);
        }
        public void Join(string Address, int Port)
        {
            transit = new Multicast();
            transit.Join(Address, Port);
        }

        public void Search()
        {
            HEADER header;

            header.Tag = ProtocolDataUnit.TAG;
            header.Syn = 1;
            header.Ack = 0;
            header.Cmd = ProtocolDataUnit.Cmd.Search;
            header.DataLength = 0;
            header.DataChecksum = 0;
            header.HeaderChecksum = 0;

            transit.Send(header.ToBytes());
        }

        public void Sync()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Announce()
        {
            throw new NotImplementedException();
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
}
