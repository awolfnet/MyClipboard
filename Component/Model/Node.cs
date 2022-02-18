using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Component.Model
{
    public class Device
    {
        public string Name { set; get; }
        public string Platform { set; get; }
        public string IPAddress { set; get; }
        public int Port { set; get; }
        public Guid Guid { set; get; }

        public override string ToString()
        {
            return $"{Name}/{Platform}/{IPAddress}/{Port}/{Guid}";
        }

    }
}