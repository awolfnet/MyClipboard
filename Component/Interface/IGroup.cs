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
    public interface IGroup
    {
        event EventHandler DeviceDiscovered;

        void Join(string LocalAddress, int SrcPort, string RemoteAddress, int DstPort);

        void Join(string Address, int Port);

        void Announce();

        void Search();

        void Sync();

        void Exit();
    }


    public class DeviceDiscoveredEventArgs : EventArgs
    {
        public Node Node { set; get; }
        public DateTime Datetime { set; get; }
    }
}