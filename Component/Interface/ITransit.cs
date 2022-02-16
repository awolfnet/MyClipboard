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
    public interface ITransit
    {
        event EventHandler DataArrived;

        void Join(string MulticastAddress, int SrcPort, int DstPort);

        void Join();
        int Send(byte[] Buffer);

    }

    public class DataArrivedEventArgs:EventArgs
    {
        public int Length;
        public byte[] Data;
    }

}