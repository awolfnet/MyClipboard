using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Component.Interface
{
    public interface IDataLink
    {
        event EventHandler<DataArrivedEventArgs> DataArrived;
        void JoinMulticastGroup(string LocalAddress, int SrcPort, string MulticastAddress, int DstPort);
        void DropMulticastGroup();

        int Send(byte[] Buffer);

    }

    public class DataArrivedEventArgs:EventArgs
    {
        public int Length;
        public byte[] Data;
    }

}