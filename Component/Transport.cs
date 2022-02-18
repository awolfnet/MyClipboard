using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Component.Interface;
using Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Component
{
    public class Transport : ITransport
    {
        public enum TransportType
        {
            Unicast,
            Multicat,
            Broadcast,
        }

        private IDataLink dataLink = null;
        private uint _syn = 1;

        public Transport(TransportType transportType)
        {
            switch (transportType)
            {
                case TransportType.Unicast:
                    {
                        break;
                    }
                case TransportType.Multicat:
                    {
                        dataLink = new Multicast();
                        break;
                    }
                case TransportType.Broadcast:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            Initiate();

        }
        public void Join(string LocalAddress, int SrcPort, string MulticastAddress, int DstPort)
        {
            dataLink.JoinMulticastGroup(LocalAddress, SrcPort, MulticastAddress, DstPort);
        }

        public void Join(string MulticastAddress, int DstPort)
        {
            throw new NotImplementedException();
        }

        public void Leave()
        {
            dataLink.DropMulticastGroup();
        }

        public void BroadcastMessage(string Message)
        {
            byte[] payload = Encoding.UTF8.GetBytes(Message);

            ProtocolDataUnit.HEADER header;
            header.Tag = ProtocolDataUnit.TAG;
            header.Syn = _syn++;
            header.Opt = ProtocolDataUnit.Opt.Message;
            header.DataLength = payload.Length;
            header.DataChecksum = 0;
            header.HeaderChecksum = 0;

            int sentLength = 0;
            sentLength = dataLink.Send(header.ToBytes());
            sentLength += dataLink.Send(payload);
        }

        public void Search()
        {
            ProtocolDataUnit.HEADER header;
            header.Tag = ProtocolDataUnit.TAG;
            header.Syn = _syn++;
            header.Opt = ProtocolDataUnit.Opt.Search;
            header.DataLength = 0;
            header.DataChecksum = 0;
            header.HeaderChecksum = 0;

            int sentLength = 0;
            sentLength = dataLink.Send(header.ToBytes());
        }

        public void Announce(Device device)
        {
            byte[] payload = Encoding.UTF8.GetBytes(device.ToString());

            ProtocolDataUnit.HEADER header;
            header.Tag = ProtocolDataUnit.TAG;
            header.Syn = _syn++;
            header.Opt = ProtocolDataUnit.Opt.Announce;
            header.DataLength = 0;
            header.DataChecksum = 0;
            header.HeaderChecksum = 0;

            int sentLength = 0;
            sentLength = dataLink.Send(header.ToBytes());
            sentLength += dataLink.Send(payload);
        }

        private void Initiate()
        {
            _syn = 1;
            dataLink.DataArrived += DataLink_DataArrived;
        }

        private void DataLink_DataArrived(object sender, DataArrivedEventArgs e)
        {
            byte[] buffer = e.Data;


            //    //ProtocolDataUnit.HEADER headerReceive=new ProtocolDataUnit.HEADER();
            //    //headerReceive.Parse(buffer);
        }
    }
}