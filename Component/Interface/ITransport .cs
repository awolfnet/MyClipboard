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
    public interface ITransport
    {
        /// <summary>
        /// Join a group
        /// </summary>
        /// <param name="LocalAddress"></param>
        /// <param name="SrcPort"></param>
        /// <param name="MulticastAddress"></param>
        /// <param name="DstPort"></param>
        void Join(string LocalAddress, int SrcPort, string MulticastAddress, int DstPort);
        
        /// <summary>
        /// Join a group
        /// </summary>
        /// <param name="MulticastAddress"></param>
        /// <param name="DstPort"></param>
        void Join(string MulticastAddress, int DstPort);
        
        /// <summary>
        /// Leave a group
        /// </summary>
        void Leave();

        /// <summary>
        /// Broadcast a message to the group
        /// </summary>
        /// <param name="Message"></param>
        void BroadcastMessage(string Message);

        /// <summary>
        /// Search group
        /// </summary>
        void Search();

        void Announce(Device device);

    }
}